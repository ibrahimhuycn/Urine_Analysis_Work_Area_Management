Imports System.Text
Public Class MiscAstmOperations

    Dim ExpectNextBlock As Boolean = False
    ''' <summary>
    ''' Reads checksum of an ASTM frame, calculates characters after STX,
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
                Case astmConstants.STX
                    sumOfChars = 0
                Case astmConstants.ETX, astmConstants.ETB
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
    ''' Replaces display ASTM control characters (i.e., STX, ETX etc) with their ASCII values 
    ''' for calculation of checksum.
    ''' </summary>
    ''' <param name="astmFrame">frame of ASTM data to evaluate</param>
    ''' <returns>ASTM frame with actual ASCII control characters</returns>
    Public Function ReplaceControlCharacters(astmFrame As string) As String
        Dim ReplcedAstmFrame As StringBuilder = New StringBuilder(astmFrame)

        ReplcedAstmFrame.Replace("[STX]", ChrW(astmConstants.STX))
        ReplcedAstmFrame.Replace("[ETX]", ChrW(astmConstants.ETX))
        ReplcedAstmFrame.Replace("[EOT]", ChrW(astmConstants.EOT))
        ReplcedAstmFrame.Replace("[ENQ]", ChrW(astmConstants.ENQ))
        ReplcedAstmFrame.Replace("[ACK]", ChrW(astmConstants.ACK))
        ReplcedAstmFrame.Replace("[LF]", ChrW(astmConstants.LF))
        ReplcedAstmFrame.Replace("[CR]", ChrW(astmConstants.CR))
        ReplcedAstmFrame.Replace("[NAK]", ChrW(astmConstants.NAK))
        ReplcedAstmFrame.Replace("[ETB]", ChrW(astmConstants.ETB))

        Return ReplcedAstmFrame.ToString
    End Function
    ''' <summary>
    ''' Checks whether astm frame is complete, checks whether to expect another block of the frame.
    ''' This is achieved by checking the start of the frame for [STX] and the end of the frame for either [CR] or [ETB]
    ''' </summary>
    ''' <param name="frame">frame of ASTM data to evaluate with actual ASTM control characters. Not the placeholders.</param>
    ''' <returns>Returns True for complete frames and False otherwise</returns>
    Function IsAstmFrameComplete(frame As String) As Boolean

        Dim byteVal As Integer
        Dim IsComplete As Boolean = False
        ExpectNextBlock = False
        Dim GotETX As Boolean = False

        'Look for ASCII [STX] at the beginning of the frame.
        If Left(frame, 1) = ChrW(2) Then

            For idx As Integer = frame.Length -1 To 0 Step -1

                byteVal = Convert.ToInt32(frame(idx))
               ' MsgBox (byteVal)
                Select Case byteVal
                    Case astmConstants.CR   'the [CR][LF] should not be considered as a [CR] for end of frame.
                        If GotETX = True then IsComplete = True
                    Case astmConstants.ETX
                        GotETX = True
                    Case astmConstants.ETB
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
End Class
