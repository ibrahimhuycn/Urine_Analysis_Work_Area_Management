Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ASTM.Records.Header
Imports ASTM.Delimiters
Namespace Records
    <TestClass()> Public Class HeaderTests

        <TestMethod()> Public Sub GenerateHeader_RepeatDelimiterBackslashNoTimeStampLIS2A2_ValidResponse()

            'ARRANGE
            Dim GenHeaderRecord As New ASTM.Records.Header
             Const ExpectedHeader As String = " H|\^&||||||||||LIS2-A2|"
            'ACT
            Dim ActualHeader As String = GenHeaderRecord.GenerateHeader("TestNinja",False,ASTM_Versions.LIS2_A2,AstmDelimiters.RepeatDelimiter)
            
            'ASSERT
            Assert.Equals(ActualHeader,ExpectedHeader)

        End Sub

    End Class
End Namespace
