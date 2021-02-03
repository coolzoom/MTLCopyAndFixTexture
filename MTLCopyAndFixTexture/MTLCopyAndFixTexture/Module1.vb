
Imports System.Deployment
Imports System.IO

Module Module1

    Sub Main()
        Dim sPath As String = AppDomain.CurrentDomain.BaseDirectory
        Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory
        Dim di As DirectoryInfo = New DirectoryInfo(sPath)

        'Dim fi As FileInfo = New FileInfo("..\..\..\..\..\dungeons\textures\walls\mm_strmwnd_wall_01.png")

        Dim fList As FileInfo() = di.GetFiles("*.mtl")

        Dim newfilecontent As String = ""
        For Each f In fList
            Dim line As String
            Dim sr As StreamReader = New StreamReader(f.FullName, System.Text.Encoding.UTF8)
            Do While sr.Peek() > 0
                line = sr.ReadLine()
                If line.StartsWith("map_Kd ") Then
                    Dim strTexturePath As String = Mid(line, 8)
                    Dim stFI As FileInfo = New FileInfo(strTexturePath)
                    If Not File.Exists(stFI.Name) Then
                        File.Copy(strTexturePath, stFI.Name)
                    End If
                    newfilecontent = newfilecontent & "map_Kd " & stFI.Name & vbCrLf
                Else
                    newfilecontent = newfilecontent & line & vbCrLf
                End If
            Loop
            sr.Close()
            sr = Nothing
            File.Move(f.FullName, f.FullName & ".bak")
            File.WriteAllText(f.FullName, newfilecontent)
        Next

    End Sub

    Private Sub subTXTWrite()
        Dim strFilePath As String = "D:\test.txt"
        Dim temp
        Dim sw As StreamWriter = New StreamWriter(strFilePath, True, System.Text.Encoding.UTF8) 'true is append method
        For i = 0 To 10
            temp = i.ToString
            sw.WriteLine(temp)
            sw.Flush()
        Next
        sw.Close()
        sw = Nothing
    End Sub

    Private Sub subTXTRead()
        Dim line As String
        Dim sr As StreamReader = New StreamReader("D:\test.txt", System.Text.Encoding.UTF8)
        Do While sr.Peek() > 0
            line = sr.ReadLine()
        Loop
        sr.Close()
        sr = Nothing
    End Sub

End Module
