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
        Const TestAstmFrame As String = "2[Text]A6"
        Dim ValidateASTM As New ASTM.MiscAstmOperations
        'ACT
        Dim ActualValue As Boolean = ValidateASTM.IsAstmFrameComplete(TestAstmFrame)
        'MsgBox(ActualValue)
        'ASSERT
        Assert.IsTrue(ActualValue)
    End Sub
    <TestMethod()> Public Sub IsAstmFrameComplete_PassCompleteAstmFrameWithCrEtx_ReturnTrueAsComplete()
        'ARRANGE
        Const TestAstmFrame As String = "2[Text]
A6
"
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
        Const TestAstmFrame As String = "1H|¥^&|||U-WAM^00-08_Build008^11001^^^^AU501736||||||||LIS2-A2|20170307144247
CF
"
        Dim IsFrameComplete As New ASTM.MiscAstmOperations
        'ACT
        Dim ActualValue As Boolean = IsFrameComplete.IsAstmFrameComplete(TestAstmFrame)
        'ASSERT
        Assert.IsFalse(ActualValue)
    End Sub
End Class