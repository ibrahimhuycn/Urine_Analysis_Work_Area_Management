Imports System.Reflection
Imports System.Text
Imports ASTM.astmConstants
Imports ASTM.Delimiters.AstmDelimiters
Imports ASTM.ErrorCodes.Errors

Public Class MiscAstmOperations

    'Initializing log4net logger for this class and getting class name from reflection
    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)

    Dim expectNextBlock As Boolean = False

    Enum FrameType
        H 'Header       0
        P 'Patient      1
        O 'Order        2
        Q 'Query        3
        R 'Result       4
        C 'Comment      5
        S 'Scientific   6
        L 'Terminator   7
        M 'Manufacturer 8
    End Enum

    ''' <summary>
    ''' Determines the type of frame by reading the
    ''' </summary>
    ''' <param name="AstmFrame">frame of ASTM data to evaluate</param>
    ''' <returns>Returns frame type as Enum FrameType.</returns>
    Public Shared Function DetermineFrameType(AstmFrame As String) As FrameType
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info(String.Format("Method: {0} Frame: {1}", myName, AstmFrame))

        Dim splitFrame() As String = AstmFrame.Split(ChrW(fieldDelimiter))
        Dim frameLetter As String = Right(splitFrame(0), 1)
        Dim returnFrameType = invalidFrameType

        Select Case frameLetter
            Case "H"
                returnFrameType = FrameType.H
            Case "P"
                returnFrameType = FrameType.P
            Case "O"
                returnFrameType = FrameType.O
            Case "Q"
                returnFrameType = FrameType.Q
            Case "R"
                returnFrameType = FrameType.R
            Case "C"
                returnFrameType = FrameType.C
            Case "L"
                returnFrameType = FrameType.L
        End Select

        log.Info(myName & " returned " & returnFrameType)
        Return returnFrameType
    End Function

    ''' <summary>
    ''' Calculates checksum of an ASTM frame, calculates characters after STX,
    ''' up to and including the ETX or ETB. Method assumes the frame contains an ETX or ETB.
    ''' </summary>
    ''' <param name="Frame">frame of ASTM data to evaluate</param>
    ''' <returns>string containing checksum</returns>
    Public Function GetCheckSumValue(ByVal Frame As String) As String
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info("Method: " & myName & " Frame: " & Frame)

        Frame = ReplaceControlCharacters(Frame)
        Dim checksum As String = "00"
        Dim byteVal As Integer = 0
        Dim sumOfChars As Integer = 0
        Dim complete As Boolean = False
        For idx As Integer = 0 To Frame.Length - 1
            byteVal = Convert.ToInt32(Frame(idx))
            Select Case byteVal
                Case STX
                    sumOfChars = 0
                Case ETX, ETB
                    sumOfChars += byteVal
                    complete = True
                Case Else
                    sumOfChars += byteVal
            End Select

            If complete Then Exit For
        Next

        If sumOfChars > 0 Then
            checksum = Convert.ToString(sumOfChars Mod 256, 16).ToUpper()
        End If

        log.Info(myName & " returned " & If(checksum.Length = 1, "0" & checksum, checksum))
        Return If(checksum.Length = 1, "0" & checksum, checksum)

    End Function

    ''' <summary>
    ''' Returns True/False after checking the validity of the ASTM frame by validating the checksum.
    ''' IMPORTANT: Method assumes that frame ends with [CR][LF]
    ''' </summary>
    ''' <param name="frame">frame of ASTM data to evaluate with actual ASTM control characters. Not the placeholders.</param>
    ''' <returns>Returns True for valid checksums and False otherwise</returns>
    Function IsAstmFrameValid(frame As String) As Boolean
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info("Method: " & myName & " Frame: " & frame)

        'Calculate ASTM checksum for the provided ASTM frame.
        'Compare checksum in the frame with the calculated checksum.
        'ASTM frame ending pattern: [CHK1][CHK2][CR][LF]
        'MsgBox(Strings.Mid(frame, frame.Length - 3, 2))
        Dim isValid As Boolean = False
        If GetCheckSumValue(frame) = Mid(frame, frame.Length - 3, 2) Then isValid = True
        log.Info(myName & " returned " & isValid)
        Return isValid
    End Function

    ''' <summary>
    ''' Checks whether astm record is complete, checks whether to expect another block of the record.
    ''' This is achieved by checking the start of the frame for [STX] and the end of the frame for either [CR][ETX] or [ETB]
    ''' </summary>
    ''' <param name="validatedFrame">frame of ASTM data to evaluate with actual ASTM control characters. Not the placeholders.</param>
    ''' <returns>Returns True for complete records and False otherwise</returns>
    Function IsAstmRecordComplete(validatedFrame As String) As Boolean
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info("Method: " & myName & " Frame: " & validatedFrame)

        Dim byteVal As Integer
        Dim isComplete As Boolean = False
        expectNextBlock = False
        Dim GotETX As Boolean = False

        'Look for ASCII [STX] at the beginning of the frame.
        If Left(validatedFrame, 1) = ChrW(2) Then
            'Starting at the end of the frame to look for pattern [CR][ETX] Or [ETB]
            For idx As Integer = validatedFrame.Length - 1 To 0 Step -1

                byteVal = Convert.ToInt32(validatedFrame(idx))
                ' MsgBox (byteVal)
                Select Case byteVal
                    Case CR   'the [CR][LF] should not be considered as a [CR] for end of frame.
                        If GotETX = True Then isComplete = True
                    Case ETX
                        GotETX = True
                    Case ETB
                        isComplete = True
                        expectNextBlock = True
                End Select
                If isComplete Then Exit For

            Next

            log.Info(myName & " returned " & isComplete)
            Return isComplete
        Else

            log.Info(myName & " returned " & isComplete)
            'A frame which does not start with ASCII [STX] is invalid.
            Return isComplete
        End If
    End Function

    ''' <summary>
    ''' Replaces display ASTM control characters (i.e., STX, ETX etc) with their ASCII values
    ''' for calculation of checksum.
    ''' </summary>
    ''' <param name="AstmFrame">frame of ASTM data to evaluate</param>
    ''' <returns>ASTM frame with actual ASCII control characters</returns>
    Public Function ReplaceControlCharacters(AstmFrame As String) As String
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info("Method: " & myName & " Frame: " & AstmFrame)

        Dim replacedAstmFrame As StringBuilder = New StringBuilder(AstmFrame)

        replacedAstmFrame.Replace("[STX]", ChrW(STX))
        replacedAstmFrame.Replace("[ETX]", ChrW(ETX))
        replacedAstmFrame.Replace("[EOT]", ChrW(EOT))
        replacedAstmFrame.Replace("[ENQ]", ChrW(ENQ))
        replacedAstmFrame.Replace("[ACK]", ChrW(ACK))
        replacedAstmFrame.Replace("[LF]", ChrW(LF))
        replacedAstmFrame.Replace("[CR]", ChrW(CR))
        replacedAstmFrame.Replace("[NAK]", ChrW(NAK))
        replacedAstmFrame.Replace("[ETB]", ChrW(ETB))

        log.Info(myName & " returned " & replacedAstmFrame.ToString)
        Return replacedAstmFrame.ToString
    End Function

End Class