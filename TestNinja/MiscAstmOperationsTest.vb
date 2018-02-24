Imports ASTM.astmConstants
<TestClass()> Public Class MiscAstmOperationsTest

    <TestMethod()> Public Sub ReplaceControlCharacters_CheckAllCharactersInUse_CovertedAsciiControlCharacters()
        'ARRANGE
        Dim ExpectedValue As String = ChrW(2) & ChrW(3) & ChrW(4) & ChrW(5) & ChrW(6) & ChrW(10) & ChrW(13) & ChrW(21) & ChrW(23)
        Dim ReplaceChars As New ASTM.MiscAstmOperations
        'ACT
        Dim ActualValue As String = ReplaceChars.ReplaceControlCharacters("[STX][ETX][EOT][ENQ][ACK][LF][CR][NAK][ETB]")
        'ASSERT
        Assert.AreEqual(ActualValue, ExpectedValue)
    End Sub

    <TestMethod()> Public Sub GetCheckSumValue_PassAstmFrame_GetValidChecksum()
        'ARRANGE
         Const ExpectedValue As String = "3D"
        Dim CalculateChecksum As New ASTM.MiscAstmOperations
        'ACT
        Dim ActualValue As String = CalculateChecksum.GetCheckSumValue("[STX]5R|2|^^^1.0000+950+1.0|15|||^5^||V||34001637|20080516153540|20080516153602|34001637[CR][ETX]")
        'ASSERT
        Assert.AreEqual(ActualValue, ExpectedValue)
    End Sub

    <TestMethod()> Public Sub IsAstmFrameComplete_PassCompleteAstmFrameWithEtb_ReturnTrueAsComplete()
        'ARRANGE
        '[stx]2[text][etb]A6[CR][LF]
        Dim TestAstmFrame As String = String.Format("{0}2[Text]{1}A6{2}{3}", ChrW(STX), ChrW(ETB), ChrW(CR), ChrW(LF))
        Dim ValidateASTM As New ASTM.MiscAstmOperations
        'ACT
        Dim ActualValue As Boolean = ValidateASTM.IsAstmFrameComplete(TestAstmFrame)
        'MsgBox(ActualValue)
        'ASSERT
        Assert.IsTrue(ActualValue)
    End Sub
    <TestMethod()> Public Sub IsAstmFrameComplete_PassCompleteAstmFrameWithCrEtx_ReturnTrueAsComplete()
        'ARRANGE
        Dim TestAstmFrame As String = String.Format("2[Text]{0}{1}A6{2}{3}", ChrW(CR), ChrW(ETX), chrw(CR), ChrW(LF))
        Dim IsFrameComplete As New ASTM.MiscAstmOperations
        'ACT
        Dim ActualValue As Boolean = IsFrameComplete.IsAstmFrameComplete(TestAstmFrame)
        'ASSERT
        Assert.IsTrue(ActualValue)
        End Sub
    <TestMethod()> Public Sub IsAstmFrameComplete_PassIncompleteAstmFrameNoStx_ReturnFalseAsIncomplete()
        'ARRANGE
        Const TestAstmFrame As String = "2[Text]A6"
        Dim IsFrameComplete As New ASTM.MiscAstmOperations
        'ACT
        Dim ActualValue As Boolean = isFrameComplete.IsAstmFrameComplete(TestAstmFrame)
        'ASSERT
        Assert.IsFalse(ActualValue)
    End Sub
    <TestMethod()> Public Sub IsAstmFrameComplete_PassIncompleteAstmFrameNoEtx_ReturnFalseAsIncomplete()
        'ARRANGE
        Dim TestAstmFrame As String = String.Format("1H|¥^&|||U-WAM^00-08_Build008^11001^^^^AU501736||||||||LIS2-A2|20170307144247{0}{1}", ChrW(CR), ChrW(LF))
        Dim IsFrameComplete As New ASTM.MiscAstmOperations
        'ACT
        Dim ActualValue As Boolean = IsFrameComplete.IsAstmFrameComplete(TestAstmFrame)
        'ASSERT
        Assert.IsFalse(ActualValue)
    End Sub
    <TestMethod()> Public Sub IsAstmFrameValid_ValidChecksum_True()
        'ARRANGE
        Dim TestAstmFrame0 As String = String.Format("{0}1H|¥^&|||U-WAM^00-08_Build008^11001^^^^AU501736||||||||LIS2-A2|20170307144247{1}{2}B8{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))
        Dim TestAstmFrame1 As String = String.Format("{0}1foo|1{1}{2}32{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))
        Dim IsFrameComplete As New ASTM.MiscAstmOperations
        'ACT
        Dim IsValid0 As Boolean = IsFrameComplete.IsAstmFrameValid(TestAstmFrame0)
        Dim IsValid1 As Boolean = IsFrameComplete.IsAstmFrameValid(TestAstmFrame1)
        'MsgBox(IsValid.ToString)
        'ASSERT
        Assert.IsTrue(IsValid0)
        Assert.IsTrue(IsValid1)
    End Sub
End Class