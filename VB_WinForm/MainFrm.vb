
Imports System.Environment
Imports System.Linq
Imports Entity


Public Class MainFrm
    Private Sub btnSure_Click(sender As Object, e As EventArgs) Handles btnSure.Click
        If txt_URL.Text.Trim() IsNot Nothing Then
            WebBrowser1.Url = New Uri(txt_URL.Text)
        End If


    End Sub

    Private Sub btn_Exec_Click(sender As Object, e As EventArgs) Handles btn_Exec.Click
        Dim str_b = $"現在は:{DateTime.Now.ToString("yyyyMMdd")}"
        Dim str_a = Procedure(str_b)

        Dim module_1 = New [Module]()
        Dim entity = New Entity.PZDBEntities()

        Dim moduleList As List(Of [Module])
        moduleList = (From m In entity.Modules
                      Where m.ModuleCode IsNot Nothing
                      Select m).ToList()

        moduleList = entity.Modules.Where(Function(m) m.ModuleCode IsNot Nothing).ToList()

        For Each mo As [Module] In moduleList
            Dim str_Line = ""
            For Each prop As System.Reflection.PropertyInfo In mo.GetType().GetProperties()
                Dim obj_value As String
                obj_value = If(prop.GetValue(mo) Is Nothing, " ", prop.GetValue(mo).ToString())
                str_Line += $"{obj_value},"
            Next
            Procedure(str_Line.Substring(0, str_Line.Length - 1))
        Next

    End Sub




    Function Procedure(ByVal strLine As String) As Boolean
        Dim opBool = True
        Try
            txtDisplay.Text = $"{strLine}{NewLine}{txtDisplay.Text}"
        Catch ex As Exception
            txtDisplay.Text = $"{ex.Message}{NewLine}{txtDisplay.Text}"
            opBool = False
        End Try
        Return opBool
    End Function

End Class
