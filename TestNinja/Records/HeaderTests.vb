Imports ASTM.Delimiters.AstmDelimiters
Imports ASTM.astmConstants
Imports ASTM.Records.Header
Imports ASTM.ErrorCodes.Errors

Namespace Records

    <TestClass()> Public Class HeaderTests

        <TestMethod()> Public Sub GenerateHeader_RepeatDelimiterBackslashNoTimeStampLIS2A2_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header
            Const ExpectedHeader As String = "[STX][F#]H|\^&|||TestNinja||||||||LIS2-A2|[ETX][CHK1][CHK2][CR][LF]"
            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja", False,, repeatDelimiter)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub

        <TestMethod()> Public Sub GenerateHeader_RepeatDelimiterYenwoTimeStampLIS2A2_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header
            Const ExpectedHeader As String = "[STX][F#]H|¥^&|||TestNinja||||||||LIS2-A2|[ETX][CHK1][CHK2][CR][LF]"
            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja", False,, repeatDelimiterYen)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub

        <TestMethod()> Public Sub GenerateHeader_RepeatDelimiterBackslashNoTimeStampE1394_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header
            Const ExpectedHeader As String = "[STX][F#]H|\^&|||TestNinja||||||||E1394-97|[ETX][CHK1][CHK2][CR][LF]"
            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja", False, AstmVersions.E1394_97, repeatDelimiter)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub

        <TestMethod()> Public Sub GenerateHeader_RepeatDelimiterBackslashTimeStampedLIS2A2_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header

            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja", True,, repeatDelimiter)
            Dim ExpectedHeader As String = String.Format("[STX][F#]H|\^&|||TestNinja||||||||LIS2-A2|{0}[ETX][CHK1][CHK2][CR][LF]", testingTimestamp)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub

        <TestMethod()> Public Sub GenerateHeader_AllFieldsLIS2A2_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header

            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja", True,, repeatDelimiter, "Msg", "Pass", "Addr", "Reserved", "7657111", "c.a.ps", "Receiver", "Comment", "PId")
            Dim ExpectedHeader As String = String.Format("[STX][F#]H|\^&|Msg|Pass|TestNinja|Addr|Reserved|7657111|c.a.ps|Receiver|Comment|PId|LIS2-A2|{0}[ETX][CHK1][CHK2][CR][LF]", testingTimestamp)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub

        <TestMethod()> Public Sub InterpretHeader_PassAnotherRecordType_ReturnOne()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header

            'ACT
            Dim ActualHeader As String = GenHeaderRecord.InterpretHeader(String.Format("{0}3L|1|N{1}{2}B8{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF)))
            Dim ExpectedHeader As String = invalidFrameType

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub

    End Class

End Namespace