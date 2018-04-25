Imports System.Reflection
Imports ASTM.Delimiters.AstmDelimiters

Public Class NestedDelimiting

    Enum NestedDelimitedTypes
        componentDelimited
        repeatDelimited
        bothComponentAndRepeatDelimited
        notDelimited
    End Enum

    'Initializing log4net logger for this class and getting class name from reflection
    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)

    ''' <summary>
    ''' Gets individual components from a component delimited field INCLUDING NULL VALUES
    ''' </summary>
    ''' <param name="componentDelimitedField">Component delimited field to separate out components</param>
    ''' <returns>An array of components</returns>
    Public Shared Function GetIndividualComponents(ByVal componentDelimitedField As String) As String()
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info(String.Format("Method: {0} Frame: {1}", myName, componentDelimitedField))

        Dim GetComponents As New NestedDelimiting
        Return GetComponents.DeserializeField(componentDelimitedField, componentDelimiter)
    End Function

    ''' <summary>
    ''' Gets individual values for the field from repeat delimited field
    ''' </summary>
    ''' <param name="repeatDelimitedField">Repeat delimited field to be deserialized.</param>
    ''' <returns>An array of all the values for the field.</returns>
    Public Shared Function GetFieldRepeats(ByVal repeatDelimitedField As String) As String()
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info(String.Format("Method: {0} Frame: {1}", myName, repeatDelimitedField))

        Dim GetRepeats As New NestedDelimiting
        Return GetRepeats.DeserializeField(repeatDelimitedField, repeatDelimiter)   'Todo: Determine whether to use Yen or Backslash
    End Function

    ''' <summary>
    ''' Does the actual separation of fields from the nested delimited fields with the string delimiter being passed to it.
    ''' </summary>
    ''' <param name="field">Deserialize with the provided delimited.</param>
    ''' <param name="delimiter">The delimiter to be used. Eg: Component delimiter / field delimiter</param>
    ''' <returns>An array of all the values for the field, including null values.</returns>
    Private Function DeserializeField(ByVal field As String, ByVal delimiter As Integer) As String()
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info(String.Format("Method: {0} Frame: {1}", myName, field))

        Return field.Split(ChrW(delimiter))
    End Function

    ''' <summary>
    ''' Checks whether the field is delimited by looking for the presence of a repeat delimiter or component delimiter or both.
    ''' </summary>
    ''' <param name="fields">The field to be checked.</param>
    ''' <returns>Returns an integer as an enum indicating type of delimiting. If not delimited, that is indicated.</returns>
    Public Shared Function IsNestedDelimited(ByVal fields As String) As NestedDelimitedTypes
        'Setting up method name for logging.
        Dim myName As String = MethodBase.GetCurrentMethod().Name
        log.Info(String.Format("Method: {0} Frame: {1}", myName, fields))

        Select Case True

            Case fields.Contains(ChrW(componentDelimiter)) And fields.Contains(ChrW(repeatDelimiter))   'Checking whether the field has both component/repeat delimiters
                Return NestedDelimitedTypes.bothComponentAndRepeatDelimited
                'TODO: What about Yen?

            Case fields.Contains(ChrW(componentDelimiter))  'Checking whether the field has a component delimiter.
                Return NestedDelimitedTypes.componentDelimited

            Case fields.Contains(ChrW(repeatDelimiter))   'Checking whether the field has a repeat delimiter.
                Return NestedDelimitedTypes.repeatDelimited

            Case Else   'Fields that are not delimited falls through to else statement.
                Return NestedDelimitedTypes.notDelimited
        End Select

    End Function

End Class