Imports ASTM.Delimiters.AstmDelimiters
Namespace Records
     'Common ASTM records structure
    Public Class Header
        'Record Template with chars < 240: 
        '[STX] [F#] [Text] [ETX] [CHK1] [CHK2] [CR] [LF]


        'Record Template with chars > 240:
        '[STX] [F#] [Text] [ETB] [CHK1] [CHK2] [CR] [LF]
        '[STX] [F#] [Text] [ETB] [CHK1] [CHK2] [CR] [LF]
        '……
        '[STX] [F#] [Text] [ETX] [CHK1] [CHK2] [CR] [LF]


        'Max characters in ASTM record cannot exceed 240 including the overhead of
        'control characters.
        Const MaxCharSPerRecord As Integer = 240
        Const MaxRetriesSendingRecord As Integer = 06  'max number of retries after consecutive NAKs from recever.

        'Variables used for Testing
        Public Shared TestingTimestamp As String

        'Todo: Remove this Enum from he
      Enum TimeOuts
                    'Timeouts expresseed in milliseconds
                    'Establishment phase
                  ReplyWindowAfterENQ = 15000        'reply with ACK, NAK or EOT
                  MinimumENQWaitAfterNAK = 10000     'Have to wait for min 10 sec before another ENQ
                  ENQWaitAfterENQClash = 20000       'Server have to wait min 20 before sending ENQ after an ENQ Clash

      End Enum
      Enum ASTM_Versions
                LIS2_A2
                E1394_97

      End Enum

        'Header Record Example: H|\^&|||||||||||E139→4-97|20100822100525<CR> 
        ' +-----+--------------+---------------------------------+-------------------+
        ' |  #  | ASTM Field # | ASTM Name                       | VB alias          |
        ' +=====+==============+=================================+===================+
        ' |   1 |        7.1.1 |             ASTM Record Type ID |              type |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |   2 |        7.1.2 |            Delimiter Definition |     Delimiter Def |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |   3 |        7.1.3 |              Message Control ID |        message_id |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |   4 |        7.1.4 |                 Access Password |          password |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |   5 |        7.1.5 |               Sender Name or ID |            sender |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |   6 |        7.1.6 |           Sender Street Address |           address |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |   7 |        7.1.7 |                  Reserved Field |          reserved |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |   8 |        7.1.8 |         Sender Telephone Number |             phone |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |   9 |        7.1.9 |       Characteristics of Sender |              caps |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |  10 |       7.1.10 |                     Receiver ID |          receiver |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |  11 |       7.1.11 |                        Comments |          comments |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |  12 |       7.1.12 |                   Processing ID |     processing_id |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |  13 |       7.1.13 |                  Version Number |           version |
        ' +-----+--------------+---------------------------------+-------------------+
        ' |  14 |       7.1.14 |            Date/Time of Message |         timestamp |
        ' +-----+--------------+---------------------------------+-------------------+

        Function GenerateHeader(ByVal sender As String,
        ByVal Is_timestamp_Required As Boolean,
        ByVal Optional astmVersion As ASTM_Versions = ASTM_Versions.LIS2_A2,
        ByVal Optional repeatDelimiter As Integer = RepeatDelimiter, 
        ByVal Optional message_id As String = Nothing, 
        ByVal Optional password As String = Nothing, 
        ByVal Optional address As String = Nothing,
        ByVal Optional reserved As String = Nothing,
        ByVal Optional phone As String = Nothing, 
        ByVal Optional caps As String = Nothing, 
        ByVal Optional receiver As String = Nothing ,
        ByVal Optional comments As String = Nothing,
        ByVal Optional processing_id As String = Nothing)                

            'Usage Status in Order Record Header.
            'Generate Delimiter Definition.
            Const type As String = "H"
            Dim DelimiterDef As String = ChrW(FieldDelimiter) & ChrW(repeatDelimiter) & ChrW(ComponentDelimiter) & ChrW(EscapeCharacter)

            'Setting the Default Protocol as LIS2-A2. No Need to read from disk(Settings file.) That makes the code slower.
            Dim VersionNumber As String = "LIS2-A2"
            'If the Version is specified as E1394-97 then the following line gets executed.
            If astmVersion = ASTM_Versions.E1394_97 then VersionNumber = My.Settings.E1394_97

            'Setting timestamp if required. Date and time of message format is fixed with “YYYYMMDDHHMMSS”
            Dim timestamp As String = ""
            If Is_timestamp_Required = True Then timestamp = DateTime.Now().ToString("yyyyMMddHHmmss")
            TestingTimestamp = timestamp.ToString
            Return String.Format("{0}{1}{2}{3}{2}{4}{2}{5}{2}{6}{2}{7}{2}{8}{2}{9}{2}{10}{2}{11}{2}{12}{2}{13}{2}{14}", type, DelimiterDef,ChrW(FieldDelimiter), message_id, password, sender, address, reserved, phone, caps, receiver, comments, processing_id, VersionNumber,timestamp)
      End Function
    
      Function InterpretHeader(HeaderRecord As String)
            'STEP 1: Read the first character and ensure that the Passed record is a HeaderRecord. Starts with "H"

            'STEP 2: Read the delimiter definition
                    '1. Field Delimiter     (|)
                    '2. Repeat Delimiter    (\) or (¥)
                    '3. Component Delimiter (^)
                    '4. Escape Character    (&)

            'STEP 3: Store the delimiters in My.Settings for interpreting the message correctly.
            'STEP 4: Split up the rest of the message with Field Delimiter and identify instrument with which the communication is being initialised with.
            'STEP 5: Take necessary actions and notify successful read.

            Return 0
        End Function




    End Class

End Namespace

   