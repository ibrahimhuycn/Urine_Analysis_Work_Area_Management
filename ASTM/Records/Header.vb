Imports ASTM.Delimiters.AstmDelimiters
Imports ASTM.MiscAstmOperations

Namespace Records
    'Common ASTM records structure

    Public Class Header
        'Record Template with chars < 240:
        '[STX][F#] [Text] [CR][ETX][CHK1][CHK2][CR][LF]

        'Record Template with chars > 240:
        '[STX] [F#] [Text] [ETB] [CHK1] [CHK2] [CR] [LF]
        '[STX] [F#] [Text] [ETB] [CHK1] [CHK2] [CR] [LF]
        '……
        '[STX] [F#] [Text] [ETX] [CHK1] [CHK2] [CR] [LF]

        'Variables used for Testing
        Public Shared TestingTimestamp As String

        'Max characters in ASTM record cannot exceed 240 including the overhead of
        'control characters.
        Const MaxCharSPerRecord As Integer = 240

        Const MaxRetriesSendingRecord As Integer = 6  'max number of retries after consecutive NAKs from recever.

        Enum ASTM_Versions
            LIS2_A2
            E1394_97
        End Enum

        'Todo: Remove this Enum from he
        Enum TimeOuts

            'Timeouts expressed in milliseconds
            'Establishment phase
            ReplyWindowAfterENQ = 15000        'reply with ACK, NAK or EOT

            MinimumENQWaitAfterNAK = 10000     'Have to wait for min 10 sec before another ENQ
            ENQWaitAfterENQClash = 20000       'Server have to wait min 20 before sending ENQ after an ENQ Clash

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
        ''' <summary>
        ''' Parse ASTM Header record when required details are passed,
        ''' Required details will be determined from the specific instruments documentation.
        ''' </summary>
        ''' <param name="sender">Sender Name or ID.</param>
        ''' <param name="Is_timestamp_Required">Boolean determines whether timestamp should be included in the ASTM header record.</param>
        ''' <param name="astmVersion">Default: LIS2-A2.Optional parameter. Version Number for ASTM Specification.</param>
        ''' <param name="repeatDelimiter">Default: Backslash ASCII 92. Repeat Delimiter, included in delimiter definition. </param>
        ''' <param name="message_id">This is a unique number or other ID that uniquely identifies the transmission for use in network systems.</param>
        ''' <param name="password">This is a level security/access password as mutually agreed upon by the sender and receiver.</param>
        ''' <param name="address">This text value shall contain the street address of the sender</param>
        ''' <param name="reserved">This field is currently unused but reserved for future use.</param>
        ''' <param name="phone">This field identifies a telephone number for voice communication with the sender</param>
        ''' <param name="caps">This field contains any characteristics of the sender such as, parity, checksums, optional protocols, etc. necessary for establishing a communication link with the sender.</param>
        ''' <param name="receiver">The name or other ID of the receiver. Its purpose is verification that the transmission is indeed for the receiver.</param>
        ''' <param name="comments">This text field shall contain any comments or special instructions relating to the subsequent records to be transmitted.</param>
        ''' <param name="processing_id">Processing IDs: P, T, D, Q. Production, Training, Debugging and Quality Control respectively </param>
        ''' <returns>Header record with placeholders for control characters.</returns>
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
        ByVal Optional receiver As String = Nothing,
        ByVal Optional comments As String = Nothing,
        ByVal Optional processing_id As String = Nothing)

            'Usage Status in Order Record Header.
            'Generate Delimiter Definition.
            Const type As String = "H"
            Dim DelimiterDef As String = ChrW(FieldDelimiter) & ChrW(repeatDelimiter) & ChrW(ComponentDelimiter) & ChrW(EscapeCharacter)

            'Setting the Default Protocol as LIS2-A2. No Need to read from disk(Settings file.) That makes the code slower.
            Dim VersionNumber As String = "LIS2-A2"
            'If the Version is specified as E1394-97 then the following line gets executed.
            If astmVersion = ASTM_Versions.E1394_97 Then VersionNumber = My.Settings.E1394_97

            'Setting timestamp if required. Date and time of message format is fixed with “YYYYMMDDHHMMSS”
            Dim timestamp As String = ""
            If Is_timestamp_Required = True Then timestamp = DateTime.Now().ToString("yyyyMMddHHmmss")
            TestingTimestamp = timestamp.ToString

            Return String.Format("[STX][F#]{0}{1}{2}{3}{2}{4}{2}{5}{2}{6}{2}{7}{2}{8}{2}{9}{2}{10}{2}{11}{2}{12}{2}{13}{2}{14}[ETX][CHK1][CHK2][CR][LF]", type, DelimiterDef, ChrW(FieldDelimiter), message_id, password, sender, address, reserved, phone, caps, receiver, comments, processing_id, VersionNumber, timestamp)
        End Function

        ''' <summary>
        ''' Interprets validated ASTM header record.
        ''' Records are validated by checking for valid checksum.
        ''' </summary>
        ''' <param name="HeaderRecord">ASTM Header Record to be interpreted.</param>
        ''' <returns>I have no idea what this should return yet. Maybe an ACK equivalent for a successfully decoded message.</returns>
        Function InterpretHeader(HeaderRecord As String)
            '[STX]1H|\^&|||U-WAM^00-08_Build008^11001^^^^AU501736||||||||LIS2-A2|20170307144247[CR][ETX][CHK1][CHK2][CR][LF]
            'Record should have been validated by checking the checksum characters.

            'Check whether the frame passed is a header.
            If DetermineFrameType(HeaderRecord) = FrameType.H Then
            Else

            End If

            'STEP 1: Read the delimiter definition

            'Default Delimiters
            '1. Field Delimiter     (|)
            '2. Repeat Delimiter    (\) <---Default or (¥)
            '3. Component Delimiter (^)
            '4. Escape Character    (&)

            'STEP 2: Store the delimiters variables.
            'STEP 2.1: Identify message source, load up the config for communication with source or something similar
            'STEP 3: Split up the rest of the message with Field Delimiter and identify instrument with which the communication is being initialized with.
            'STEP 4: Take necessary actions and notify successful read.

            Return 0
        End Function

    End Class

End Namespace