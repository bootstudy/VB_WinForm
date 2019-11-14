Imports System.Environment
Imports System.Data.SqlClient
Imports System.Data.OracleClient
Imports System.Configuration


Public Class Frm_DataGrid

    Dim Con As New SqlConnection

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click


        Using (Con)

            Dim settings As ConnectionStringSettings

            ' 接続文字列をapp.configファイルから取得
            settings = ConfigurationManager.ConnectionStrings("PZDBEntities1")

            Con.ConnectionString = settings.ConnectionString

            Con.Open()

            Dim cmd As SqlCommand = New SqlCommand()
            cmd.CommandText = "Select * from Notice"
            cmd.Connection = Con

            Dim drd As SqlDataReader = cmd.ExecuteReader()

            'Console.WriteLine("-----------CategoryName--------------")
            'While drd.Read()
            '    Console.WriteLine(drd("CategoryName"))
            'End While

            Dim m_schemaTable As DataTable

            Dim m_dataTable = New DataTable

            While drd.Read()

                If m_schemaTable Is Nothing Then

                    m_schemaTable = drd.GetSchemaTable()

                    For Each s_dr As DataRow In m_schemaTable.Rows
                        m_dataTable.Columns.Add(s_dr("ColumnName"))
                    Next

                End If

                Dim dr = m_dataTable.NewRow

                For i As Integer = 0 To drd.FieldCount - 1

                    dr.Item(i) = drd.GetValue(i)

                Next

                m_dataTable.Rows.Add(dr)

            End While

            dgv_data.DataSource = m_dataTable

            dgv_Schema.DataSource = m_schemaTable

        End Using



    End Sub
End Class