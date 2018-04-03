Imports System.Runtime.CompilerServices
Imports System.Text

Module SwatIncMod

    ''' <summary>
    ''' Returns a delimited string from an Array.
    ''' </summary>
    ''' <param name="stringArray"></param>
    ''' <param name="delimiter"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    <Extension()> Public Function ToDelimitedString(ByVal stringArray As String(), ByVal delimiter As String) As String
        Dim sb As New StringBuilder

        For Each buf As String In stringArray
            sb.Append(buf)
            sb.Append(delimiter)
        Next

        ' The final delimiter is trimmed off since there is no record after that item
        Return sb.ToString.Trim(delimiter)
    End Function

End Module