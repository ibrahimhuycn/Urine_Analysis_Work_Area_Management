Namespace Records
    Public Class Termination
        Function Termination()
            'Termination Record Example: L|1|N<CR>
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  #  | ASTM Field # | ASTM Name                       | VB  alias         |
            ' +=====+==============+=================================+===================+
            ' |   1 |       13.1.1 |                  Record Type ID |              type |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   2 |       13.1.2 |                 Sequence Number |               seq |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   3 |       13.1.3 |                Termination code |              code |
            ' +-----+--------------+---------------------------------+-------------------+

            Const type As String = "L"
            Dim seq As Integer = 1          'Always 1 in the case of U-WAM
            Dim code As String = "N"        '“N” is usually used as a character string. Normal Termination



            Return 0
        End Function
    End Class
End Namespace


