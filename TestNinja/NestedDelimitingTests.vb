Imports ASTM.Delimiters.AstmDelimiters
Imports ASTM.NestedDelimiting

<TestClass()> Public Class NestedDelimitingTests

    Enum NestedDelimitedTypes
        componentDelimited
        repeatDelimited
        bothComponentAndRepeatDelimited
        notDelimited
    End Enum

    <TestMethod()> Public Sub GetIndividualComponents_PassComponentDelimitedString_ArrayOfComponents()

        'ARRANGE
        Const nestedComponent As String = "aaaa^bbbb^cccc"
        Dim expectedComponents() As String = nestedComponent.Split(ChrW(componentDelimiter))
        'ACT
        Dim components() As String = GetIndividualComponents(nestedComponent)
        'ASSERT)
        CollectionAssert.AreEqual(expectedComponents, components)

    End Sub

    <TestMethod()> Public Sub GetFieldRepeats_PassRepeatDelimitedString_ArrayOfRepeats()

        'ARRANGE
        Const nestedRepeateDelimitedField As String = "aaaa\bbbb\cccc"
        Dim expectedRepeats() As String = nestedRepeateDelimitedField.Split(ChrW(repeatDelimiter))
        'ACT
        Dim repeats() As String = GetFieldRepeats(nestedRepeateDelimitedField)
        'ASSERT)
        CollectionAssert.AreEqual(expectedRepeats, repeats)

    End Sub

    <TestMethod()> Public Sub IsNestedDelimited_BothComponentAndRepeatDelimited_EnumIndicatingBothDelimited()

        'ARRANGE
        Const componentAndRepeatDelimitedString As String = "aaaa^bbbb\cccc^dddd"
        Dim expected As String = NestedDelimitedTypes.bothComponentAndRepeatDelimited
        'ACT
        Dim delimitedTypeindicator As String = IsNestedDelimited(componentAndRepeatDelimitedString)
        'ASSERT)
        Assert.AreEqual(expected, delimitedTypeindicator)

    End Sub

    <TestMethod()> Public Sub IsNestedDelimited_ComponentDelimited_EnumIndicatingComponentDelimited()

        'ARRANGE
        Const componentDelimitedString As String = "aaaa^bbbb"
        Dim expected As String = NestedDelimitedTypes.componentDelimited
        'ACT
        Dim delimitedTypeIndicator As String = IsNestedDelimited(componentDelimitedString)
        'ASSERT)
        Assert.AreEqual(expected, delimitedTypeIndicator)

    End Sub

    <TestMethod()> Public Sub IsNestedDelimited_RepeatDelimited_EnumIndicatingRepeatDelimited()

        'ARRANGE
        Const repeatDelimitedString As String = "bbbb\cccc"
        Dim expected As String = NestedDelimitedTypes.repeatDelimited
        'ACT
        Dim delimitedTypeindicator As String = IsNestedDelimited(repeatDelimitedString)
        'ASSERT)
        Assert.AreEqual(expected, delimitedTypeindicator)

    End Sub

    <TestMethod()> Public Sub IsNestedDelimited_NotDelimited_EnumIndicatingNotDelimited()

        'ARRANGE
        Const notDelimitedString As String = "aaaabbbbccccdddd"
        Dim expected As String = NestedDelimitedTypes.notDelimited
        'ACT
        Dim delimitedTypeindicator As String = IsNestedDelimited(notDelimitedString)
        'ASSERT)
        Assert.AreEqual(expected, delimitedTypeindicator)

    End Sub

End Class