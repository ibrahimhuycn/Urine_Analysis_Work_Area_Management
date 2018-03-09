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
        Dim TestAstmFrame As String = String.Format("2[Text]{0}{1}A6{2}{3}", ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))
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
        Dim ActualValue As Boolean = IsFrameComplete.IsAstmFrameComplete(TestAstmFrame)
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

    <TestMethod()> Public Sub DetermineFrameType_HeaderFrame_r0r1r2r3r4r5()
        'ARRANGE
        'Note: Checksums of the following frames may not be correct. It's not required for this purpose.
        Dim TestAstmFrame0 As String = String.Format("{0}1H|¥^&|||U-WAM^00-08_Build008^11001^^^^AU501736||||||||LIS2-A2|20170307144247{1}{2}B8{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))
        Dim TestAstmFrame1 As String = String.Format("{0}O|1|123456^05^1234567890123456789012^B^O||^^^^CHM\^^^^UF||20040807101000|||||N||||||| |||||||Q{1}{2}B8{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))
        Dim TestAstmFrame2 As String = String.Format("{0}2P|1||P00001||Sample^Ichiro^^¥Sample^Ichiro||19701130|M||||||ANEG||||KIDNEY1||||||IP{1}{2}B8{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))
        Dim TestAstmFrame3 As String = String.Format("{0}6R|2|^^^C-PH^A^1^S^0002^01|7.5^MAINFORMAT|||N||||^^admin^administrator||20151110105603|UC_3500{1}{2}B8{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))
        Dim TestAstmFrame4 As String = String.Format("{0}2Q|1|20150730001||||20170307144247||||||C{1}{2}B8{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))
        Dim TestAstmFrame5 As String = String.Format("{0}6C|1||S^Sample Comment{1}{2}B8{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))
        Dim TestAstmFrame6 As String = String.Format("{0}3L|1|N{1}{2}B8{3}{4}", ChrW(STX), ChrW(CR), ChrW(ETX), ChrW(CR), ChrW(LF))

        '
        Dim ExpectedValue As Integer = 0
        Dim ExpectedValue1 As Integer = 2
        Dim ExpectedValue2 As Integer = 1
        Dim ExpectedValue3 As Integer = 4
        Dim ExpectedValue4 As Integer = 3
        Dim ExpectedValue5 As Integer = 5
        Dim ExpectedValue6 As Integer = 6
        Dim GetFrameType As New ASTM.MiscAstmOperations
        'ACT
        Dim ActualValue As Integer = GetFrameType.DetermineFrameType(TestAstmFrame0)
        Dim ActualValue1 As Integer = GetFrameType.DetermineFrameType(TestAstmFrame1)
        Dim ActualValue2 As Integer = GetFrameType.DetermineFrameType(TestAstmFrame2)
        Dim ActualValue3 As Integer = GetFrameType.DetermineFrameType(TestAstmFrame3)
        Dim ActualValue4 As Integer = GetFrameType.DetermineFrameType(TestAstmFrame4)
        Dim ActualValue5 As Integer = GetFrameType.DetermineFrameType(TestAstmFrame5)
        Dim ActualValue6 As Integer = GetFrameType.DetermineFrameType(TestAstmFrame6)
        'MsgBox(ActualValue)
        'ASSERT
        Assert.AreEqual(ActualValue, ExpectedValue)
        Assert.AreEqual(ActualValue1, ExpectedValue1)
        Assert.AreEqual(ActualValue2, ExpectedValue2)
        Assert.AreEqual(ActualValue3, ExpectedValue3)
        Assert.AreEqual(ActualValue4, ExpectedValue4)
        Assert.AreEqual(ActualValue5, ExpectedValue5)
        Assert.AreEqual(ActualValue6, ExpectedValue6)
    End Sub

End Class