Imports ASTM.Records.Header
Imports ASTM.Delimiters
Namespace Records
    <TestClass()> Public Class HeaderTests

        <TestMethod()> Public Sub GenerateHeader_RepeatDelimiterBackslashNoTimeStampLIS2A2_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header
            Const ExpectedHeader As String = "[STX][F#]H|\^&|||TestNinja||||||||LIS2-A2|[ETX][CHK1][CHK2][CR][LF]"
            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja",False,,AstmDelimiters.RepeatDelimiter)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub
        <TestMethod()> Public Sub GenerateHeader_RepeatDelimiterYenwoTimeStampLIS2A2_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header
            Const ExpectedHeader As String = "[STX][F#]H|¥^&|||TestNinja||||||||LIS2-A2|[ETX][CHK1][CHK2][CR][LF]"
            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja", False,, AstmDelimiters.RepeatDelimiterYen)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub
        <TestMethod()> Public Sub GenerateHeader_RepeatDelimiterBackslashNoTimeStampE1394_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header
            Const ExpectedHeader As String = "[STX][F#]H|\^&|||TestNinja||||||||E1394-97|[ETX][CHK1][CHK2][CR][LF]"
            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja", False, ASTM_Versions.E1394_97, AstmDelimiters.RepeatDelimiter)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub
        <TestMethod()> Public Sub GenerateHeader_RepeatDelimiterBackslashTimeStampedLIS2A2_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header
            
            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja",True,, AstmDelimiters.RepeatDelimiter)
            Dim ExpectedHeader As String = String.Format("[STX][F#]H|\^&|||TestNinja||||||||LIS2-A2|{0}[ETX][CHK1][CHK2][CR][LF]", TestingTimestamp)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub
        <TestMethod()> Public Sub GenerateHeader_AllFieldsLIS2A2_ValidAstmHeader()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header

            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja", True,, AstmDelimiters.RepeatDelimiter,"Msg","Pass","Addr","Reserved","7657111","c.a.ps","Receiver","Comment","PId")
            Dim ExpectedHeader As String = String.Format("[STX][F#]H|\^&|Msg|Pass|TestNinja|Addr|Reserved|7657111|c.a.ps|Receiver|Comment|PId|LIS2-A2|{0}[ETX][CHK1][CHK2][CR][LF]", TestingTimestamp)

            'ASSERT
            'MsgBox(ActualHeader & vbCrLf & ExpectedHeader)
            Assert.AreEqual(ActualHeader, ExpectedHeader)

        End Sub

    End Class
End Namespace
