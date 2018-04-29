'Watch the app.config for changes in log4Net configuration. So that logging behaviour up-to-date
Imports ASTM

<Assembly: log4net.Config.XmlConfigurator(Watch:=True)>

Public Class frmUWAM

    'Initializing log4net logger for this class and getting class name from reflection
    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    Private Sub frmUWAM_Load(sender As Object, e As EventArgs) Handles Me.Load
        log.Info("Start Up")

        Dim calcCheckSum As New ASTM.MiscAstmOperations
        calcCheckSum.GetCheckSumValue("[STX]5R|2|^^^1.0000+950+1.0|15|||^5^||V||34001637|20080516153540|20080516153602|34001637[CR][ETX]")
        Dim str = "[STX]5R|2|^^^1.0000+950+1.0|15|||^5^||V||34001637|20080516153540|20080516153602|34001637[CR][ETX]"
        Dim ar As String() = str.Split("|")
        MsgBox(ar.ToDelimitedString("/"))
    End Sub

End Class