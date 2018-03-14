﻿Imports System.Text
Imports ASTM.astmConstants
Imports ASTM.Delimiters.AstmDelimiters

Public Class MiscAstmOperations

    Dim ExpectNextBlock As Boolean = False

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
    ''' <param name="astmFrame">frame of ASTM data to evaluate</param>
    ''' <returns>Returns frame type as Enum FrameType.</returns>
    Public Shared Function DetermineFrameType(astmFrame As String) As FrameType
        Dim SplitFrame() As String = astmFrame.Split(ChrW(FieldDelimiter))
        Dim FrameLetter As String = Right(SplitFrame(0), 1)
        Dim ReturnValue As FrameType = 404

        Select Case FrameLetter
            Case "H"
                ReturnValue = FrameType.H
            Case "P"
                ReturnValue = FrameType.P
            Case "O"
                ReturnValue = FrameType.O
            Case "Q"
                ReturnValue = FrameType.Q
            Case "R"
                ReturnValue = FrameType.R
            Case "C"
                ReturnValue = FrameType.C
            Case "L"
                ReturnValue = FrameType.L
        End Select

        Return ReturnValue
    End Function

    ''' <summary>
    ''' Calculates checksum of an ASTM frame, calculates characters after STX,
    ''' up to and including the ETX or ETB. Method assumes the frame contains an ETX or ETB.
    ''' </summary>
    ''' <param name="frame">frame of ASTM data to evaluate</param>
    ''' <returns>string containing checksum</returns>
    Public Function GetCheckSumValue(ByVal frame As String) As String
        frame = ReplaceControlCharacters(frame)
        Dim checksum As String = "00"
        Dim byteVal As Integer = 0
        Dim sumOfChars As Integer = 0
        Dim complete As Boolean = False
        For idx As Integer = 0 To frame.Length - 1
            byteVal = Convert.ToInt32(frame(idx))
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

        Return If(checksum.Length = 1, "0" & checksum, checksum)
    End Function

    ''' <summary>
    ''' Checks whether astm record is complete, checks whether to expect another block of the record.
    ''' This is achieved by checking the start of the frame for [STX] and the end of the frame for either [CR][ETX] or [ETB]
    ''' </summary>
    ''' <param name="ValidatedFrame">frame of ASTM data to evaluate with actual ASTM control characters. Not the placeholders.</param>
    ''' <returns>Returns True for complete records and False otherwise</returns>
    Function IsAstmRecordComplete(ValidatedFrame As String) As Boolean

        Dim byteVal As Integer
        Dim IsComplete As Boolean = False
        ExpectNextBlock = False
        Dim GotETX As Boolean = False

        'Look for ASCII [STX] at the beginning of the frame.
        If Left(ValidatedFrame, 1) = ChrW(2) Then
            'Starting at the end of the frame to look for pattern [CR][ETX] Or [ETB]
            For idx As Integer = ValidatedFrame.Length - 1 To 0 Step -1

                byteVal = Convert.ToInt32(ValidatedFrame(idx))
                ' MsgBox (byteVal)
                Select Case byteVal
                    Case CR   'the [CR][LF] should not be considered as a [CR] for end of frame.
                        If GotETX = True Then IsComplete = True
                    Case ETX
                        GotETX = True
                    Case ETB
                        IsComplete = True
                        ExpectNextBlock = True
                End Select
                If IsComplete Then Exit For

            Next
            Return IsComplete
        Else
            'A frame which does not start with ASCII [STX] is invalid.
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns True/False after checking the validity of the ASTM frame by validating the checksum.
    ''' IMPORTANT: Method assumes that frame ends with [CR][LF]
    ''' </summary>
    ''' <param name="frame">frame of ASTM data to evaluate with actual ASTM control characters. Not the placeholders.</param>
    ''' <returns>Returns True for valid checksums and False otherwise</returns>
    Function IsAstmFrameValid(frame As String) As Boolean
        'Calculate ASTM checksum for the provided ASTM frame.
        'Compare checksum in the frame with the calculated checksum.
        'ASTM frame ending pattern: [CHK1][CHK2][CR][LF]
        'MsgBox(Strings.Mid(frame, frame.Length - 3, 2))
        Dim IsValid As Boolean = False
        If GetCheckSumValue(frame) = Mid(frame, frame.Length - 3, 2) Then IsValid = True
        Return IsValid
    End Function

    ''' <summary>
    ''' Replaces display ASTM control characters (i.e., STX, ETX etc) with their ASCII values
    ''' for calculation of checksum.
    ''' </summary>
    ''' <param name="astmFrame">frame of ASTM data to evaluate</param>
    ''' <returns>ASTM frame with actual ASCII control characters</returns>
    Public Function ReplaceControlCharacters(astmFrame As String) As String
        Dim ReplcedAstmFrame As StringBuilder = New StringBuilder(astmFrame)

        ReplcedAstmFrame.Replace("[STX]", ChrW(STX))
        ReplcedAstmFrame.Replace("[ETX]", ChrW(ETX))
        ReplcedAstmFrame.Replace("[EOT]", ChrW(EOT))
        ReplcedAstmFrame.Replace("[ENQ]", ChrW(ENQ))
        ReplcedAstmFrame.Replace("[ACK]", ChrW(ACK))
        ReplcedAstmFrame.Replace("[LF]", ChrW(LF))
        ReplcedAstmFrame.Replace("[CR]", ChrW(CR))
        ReplcedAstmFrame.Replace("[NAK]", ChrW(NAK))
        ReplcedAstmFrame.Replace("[ETB]", ChrW(ETB))

        Return ReplcedAstmFrame.ToString
    End Function

End Class