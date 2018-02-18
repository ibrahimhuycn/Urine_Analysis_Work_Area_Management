Namespace Records
    Public Class Patient
                Function GeneratePatient()
            'Patient Record Example: 
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  #  | ASTM Field # | ASTM Name                       | VB  alias         |
            ' +=====+==============+=================================+===================+
            ' |   1 |        8.1.1 |                  Record Type ID |              type |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   2 |        8.1.2 |                 Sequence Number |               seq |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   3 |        8.1.3 |    Practice Assigned Patient ID |       practice_id |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   4 |        8.1.4 |  Laboratory Assigned Patient ID |     laboratory_id |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   5 |        8.1.5 |                      Patient ID |                id |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   6 |        8.1.6 |                    Patient Name |              name |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   7 |        8.1.7 |            Mother’s Maiden Name |       maiden_name |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   8 |        8.1.8 |                       Birthdate |         birthdate |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |   9 |        8.1.9 |                     Patient Sex |               sex |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  10 |       8.1.10 |      Patient Race-Ethnic Origin |              race |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  11 |       8.1.11 |                 Patient Address |           address |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  12 |       8.1.12 |                  Reserved Field |          reserved |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  13 |       8.1.13 |        Patient Telephone Number |             phone |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  14 |       8.1.14 |          Attending Physician ID |      physician_id |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  15 |       8.1.15 |                Special Field #1 |         special_1 |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  16 |       8.1.16 |                Special Field #2 |         special_2 |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  17 |       8.1.17 |                  Patient Height |            height |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  18 |       8.1.18 |                  Patient Weight |            weight |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  19 |       8.1.19 |       Patient’s Known Diagnosis |         diagnosis |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  20 |       8.1.20 |     Patient’s Active Medication |        medication |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  21 |       8.1.21 |                  Patient’s Diet |              diet |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  22 |       8.1.22 |            Practice Field No. 1 |  practice_field_1 |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  23 |       8.1.23 |            Practice Field No. 2 |  practice_field_2 |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  24 |       8.1.24 |       Admission/Discharge Dates |    admission_date |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  25 |       8.1.25 |                Admission Status |  admission_status |
            ' +-----+--------------+---------------------------------+-------------------+
            ' |  26 |       8.1.26 |                        Location |          location |
            ' +-----+--------------+---------------------------------+-------------------+
            Const type As String = "P"
            Dim seq As Integer = Nothing                        'seq
            Dim practice_id As Integer = Nothing                'Not Used
            Dim laboratory_id As Integer = Nothing              'max 16 characters long    
            Dim id As Integer = Nothing                         'Not Used

            'Names separated by caret (^)
            'LastName^MiddleName^FirstName^
            Dim name As String = Nothing
            Dim maiden_name As String = Nothing                 'Not Used
            Dim birthdate As Date = Nothing                     'B'DAY in AD    
            Dim sex As String = Nothing                         'Gender
            Dim race As String = Nothing                        'Not Used
            Dim address As String = Nothing                     'Not Used
            Dim reserved As String = Nothing                    'Not Used
            Dim phone As Integer = Nothing                      'Not Used
            Dim physician_id As Integer = Nothing               'Not Used
            Dim special_1 As String = Nothing                   'Blood Group & Rh 
            Dim special_2 As String = Nothing                   'Not Used
            Dim height As Integer = Nothing                     'Not Used
            Dim weight As Integer = Nothing                     'Not Used
            Dim diagnosis As String = Nothing                   'Disease Code       
            Dim medication As String = Nothing                  'Not Used
            Dim diet As String = Nothing                        'Not Used
            Dim practice_field_1 As String = Nothing            'Not Used
            Dim practice_field_2 As String = Nothing            'Not Used
            Dim admission_date As Date = Nothing                'Not Used
            Dim admission_status As String = Nothing            'OP: Out-patient  IP: In-patient  (Blank): Unknown
            Dim location As String = Nothing

            Return 0
        End Function
    End Class
End Namespace

