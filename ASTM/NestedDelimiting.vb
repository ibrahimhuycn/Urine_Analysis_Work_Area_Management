Imports System.Reflection
Imports ASTM.Delimiters.AstmDelimiters

Public Class NestedDelimiting

    'Initializing log4net logger for this class and getting class name from reflection
    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)

    ''' <summary>
    ''' Gets individual components from a component delimited field INCLUDING NULL VALUES
    ''' </summary>
    ''' <param name="ComponentDelimitedField">Component delimited field to separate out components</param>
    ''' <returns>An array of components</returns>
    Public Shared Function GetIndividualComponents(ByVal ComponentDelimitedField As String) As String()
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info(String.Format("Method: {0} Frame: {1}", myName, ComponentDelimitedField))

        Dim GetComponents As New NestedDelimiting
        Return GetComponents.DeserializeField(ComponentDelimitedField, componentDelimiter)
    End Function

    ''' <summary>
    ''' Gets individual values for the field from repeat delimited field
    ''' </summary>
    ''' <param name="RepeatDelimitedField">ASTM Header Record to be splitted.</param>
    ''' <returns>An array of all the values for the field.</returns>
    Public Shared Function GetFieldRepeats(ByVal RepeatDelimitedField As String) As String()
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info(String.Format("Method: {0} Frame: {1}", myName, RepeatDelimitedField))

        Dim GetRepeats As New NestedDelimiting
        Return GetRepeats.DeserializeField(RepeatDelimitedField, repeatDelimiter)   'Todo: Determine whether to use Yen or Backslash
    End Function

    ''' <summary>
    ''' Does the actual separation of fields from the nested delimited fields with the string delimiter being passed to it.
    ''' </summary>
    ''' <param name="field">ASTM Header Record to be splitted.</param>
    ''' <param name="delimiter">The delimiter to be used. Eg: Component delimiter / field delimiter</param>
    ''' <returns>An array of all the values for the field.</returns>
    Function DeserializeField(ByVal field As String, ByVal delimiter As Integer) As String()
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info(String.Format("Method: {0} Frame: {1}", myName, field))

        Return field.Split(ChrW(delimiter))
    End Function

End Class