Imports System.Data.SqlClient
Imports System.Text

Module Module1

    Dim tmpp As String

    Public con As SqlConnection
    Public constr As String

    Public consetting As SqlConnection
    Public constrsetting As String

    Public dbmyservernamesetting As String
    Public dbmydbnamesetting As String
    Public dbmypwdsetting As String
    Public dbuseridsetting As String
    Public dbreportpathsetting As String


    Public dbmyservername As String
    Public dbmydbname As String
    Public dbmypwd As String
    Public dbuserid As String
    Public dbreportpath As String

    Public Sub dbMAIN()
        dbmyservername = System.Configuration.ConfigurationManager.AppSettings("myservername")
        dbmydbname = System.Configuration.ConfigurationManager.AppSettings("mydbname")
        dbmypwd = decodefile(System.Configuration.ConfigurationManager.AppSettings("mypwd"))
        dbuserid = System.Configuration.ConfigurationManager.AppSettings("userid")
        dbreportpath = System.Configuration.ConfigurationManager.AppSettings("reportpath")


        constr = "Password=" & Trim(dbmypwd) & ";Persist Security Info=True;User ID=" & Trim(dbuserid) & ";Initial Catalog=" & Trim(dbmydbname) & ";Data Source=" & Trim(dbmyservername) & ""
        con = New SqlConnection(constr)





    End Sub

    Public Function encodefile(ByVal srcfile As String) As String

        Dim bytesToEncode As Byte()
        bytesToEncode = Encoding.UTF8.GetBytes(srcfile)

        Dim encodedText As String
        encodedText = Convert.ToBase64String(bytesToEncode)
        encodefile = Encript(encodedText)
    End Function

    Public Function Decode(ByVal Password As String) As String
        'Dim I As Integer
        Dim TMP As Long
        tmpp = ""
        For i = 1 To Len(Password)
            TMP = Asc(Mid(Password, i, 1))
            TMP = TMP - i
            tmpp = Trim(tmpp) & Chr(TMP)
            'Decode = Decode & Chr(TMP)
        Next i
        Decode = tmpp
        Return Decode
    End Function

    Public Function Encript(ByVal Password As String) As String
        ' Dim I As Integer
        'Dim tmpp As String

        Dim TMP As Long
        tmpp = ""
        For i = 1 To Len(Password)
            TMP = Asc(Mid(Password, i, 1))
            TMP = TMP + i
            tmpp = Trim(tmpp) + Chr(TMP)

            'Encript = Encript & Chr(TMP)
        Next i
        Encript = tmpp
        Return Encript
    End Function

    Public Function decodefile(ByVal srcfile As String) As String

        Dim decodedBytes As Byte()
        decodedBytes = Convert.FromBase64String(Decode(srcfile))

        Dim decodedText As String
        decodedText = Encoding.UTF8.GetString(decodedBytes)
        decodefile = decodedText
    End Function
End Module