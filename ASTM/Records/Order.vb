Namespace Records

    Public Class Order

        Function GenerateOrder()
            'Order Record Example: O|1|123456^05^1234567890123456789012^B^O||^^^^CHM\^^^^UF||20040807101000|||||N||||||| |||||||Q<CR>
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  #  | ASTM Field # | ASTM Name                      | VB  alias          |
            ' +=====+==============+================================+====================+
            ' |   1 |        9.4.1 |                 Record Type ID |               type |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |   2 |        9.4.2 |                Sequence Number |                seq |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |   3 |        9.4.3 |                    Specimen ID |          sample_id |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |   4 |        9.4.4 |         Instrument Specimen ID |         instrument |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |   5 |        9.4.5 |              Universal Test ID |               test |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |   6 |        9.4.6 |                       Priority |           priority |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |   7 |        9.4.7 |    Requested/Ordered Date/Time |         created_at |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |   8 |        9.4.8 |  Specimen Collection Date/Time |         sampled_at |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |   9 |        9.4.9 |            Collection End Time |       collected_at |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  10 |       9.4.10 |              Collection Volume |             volume |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  11 |       9.4.11 |                   Collector ID |          collector |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  12 |       9.4.12 |                    Action Code |        action_code |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  13 |       9.4.13 |                    Danger Code |        danger_code |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  14 |       9.4.14 |           Relevant Information |      clinical_info |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  15 |       9.4.15 |    Date/Time Specimen Received |       delivered_at |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  16 |       9.4.16 |            Specimen Descriptor |        biomaterial |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  17 |       9.4.17 |             Ordering Physician |          physician |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  18 |       9.4.18 |        Physician’s Telephone # |    physician_phone |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  19 |       9.4.19 |               User Field No. 1 |       user_field_1 |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  20 |       9.4.20 |               User Field No. 2 |       user_field_2 |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  21 |       9.4.21 |         Laboratory Field No. 1 | laboratory_field_1 |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  22 |       9.4.22 |         Laboratory Field No. 2 | laboratory_field_2 |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  23 |       9.4.23 |             Date/Time Reported |        modified_at |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  24 |       9.4.24 |              Instrument Charge |  instrument_charge |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  25 |       9.4.25 |          Instrument Section ID | instrument_section |
            ' +-----+--------------+--------------------------------+--------------------+
            ' |  26 |       9.4.26 |                    Report Type |        report_type |
            ' +-----+--------------+--------------------------------+--------------------+
            Const type As String = "O"
            'The sequence number starts with 1 in a maximum of 4 digits,
            'and indicates the sequence position in which the record appeared in the message.
            'This number is reset to 1 when a higher-level record appears in the message.
            Dim seq As Integer = Nothing

            'sample_id: Sub-fields of Rack No., tube position., sample No., and sample No. attribute are placed by separating with a delimiter “^”.
            'Rack No.^
            'Tube Position No.^
            'Sample ID number^
            'Sample No. attribute^
            'If only sample_id is specified the field should look like ^^sample_id^^  with a max of 22 characters
            Dim sample_id As String = Nothing
            Dim instrument As String = Nothing

            '^^^^Parameter
            'When the host computer replies order, please set the Analyzer which it is necessary to measure.
            'Example: “^^^^CHM\^^^^UF\^^^^UD”
            Dim test As Integer = Nothing

            Dim priority As String = Nothing      'R: Routine  A: Urgent   S: Emergency
            Dim created_at As DateTime = Nothing  ''YYYYMMDDHHMMSS
            Dim sampled_at As DateTime = Nothing
            Dim collected_at As DateTime = Nothing
            Dim volume As Integer = Nothing
            Dim collector As Integer = Nothing

            ' [Host computer → U-WAM]

            'Code indicating the type of order information to be sent.
            'C:          Cancellation of a parameter
            'A:          Addition of a parameter to an existing order
            'N: New order
            Const action_code As String = "N" '

            Dim danger_code As String = Nothing
            Dim clinical_info As String = Nothing
            Dim delivered_at As DateTime = Nothing
            Dim biomaterial As Integer = Nothing   'UrineSampleType: Urine, Urine-EarlyMorning, Urine-Pooled, Urine-Postprandial, Urine-Catheter, Blank
            'Physician Code ^
            'Physician Last Name ^
            'Physician First Name ^^^
            'Physician Title
            'Therefore: If no physician info is provided it should look like ^ ^ ^^^ or ^^^^^
            Dim physician As String = Nothing
            Dim physician_phone As Integer = Nothing
            Dim user_field_1 As String = Nothing
            Dim user_field_2 As String = Nothing
            Dim laboratory_field_1 As String = Nothing
            Dim laboratory_field_2 As String = Nothing
            Dim modified_at As DateTime = Nothing
            Dim instrument_charge As String = Nothing
            Dim instrument_section As String = Nothing

            'F: Final result (Fixed: U-WAMwill always output the final results.)
            'Y: No test order exists. (Use this when no order exists for the inquiry.)
            'Q: Response to the inquiry (Use this when an order exists for the inquiry.)
            Dim report_type As String = Nothing

            Return 0
        End Function

    End Class

End Namespace