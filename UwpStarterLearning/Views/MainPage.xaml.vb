' 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください
Imports Windows.UI.Popups

''' <summary>
''' それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Private Sub ButtonDbRead_Click(sender As Object, e As RoutedEventArgs) Handles ButtonDbRead.Click

        Dim cn As New System.Data.SqlClient.SqlConnection()
        Dim sb As New Data.SqlClient.SqlConnectionStringBuilder()
        Dim dr As Data.SqlClient.SqlDataReader
        Dim data As New Data.DataTable()
        Dim cmd As New Data.SqlClient.SqlCommand()

        sb.DataSource = "(local)\SQLEXPRESS"
        sb.InitialCatalog = "HibernateSample"
        sb.IntegratedSecurity = False 'Windows認証での接続は別途設定しなければならない様だ。確認中。
        sb.UserID = "sa"
        sb.Password = "sapassword"
        sb.ConnectTimeout = 3
        cn.ConnectionString = sb.ConnectionString
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select * from ShohinDataDesk"
        dr = cmd.ExecuteReader()

        data.Load(dr, System.Data.LoadOption.PreserveChanges)
        GridView1.ItemsSource = data.DefaultView
        'dr.Read()
        'Button1.Content = dr("ShohinName")
        'dr.Close()
        cn.Close()
        ContentDialogShow()

    End Sub

    Private Sub ButtonTimer_Click(sender As Object, e As RoutedEventArgs) Handles ButtonTimer.Click

        Frame.Navigate(GetType(WindowTimer))

    End Sub

    Private Async Sub MessageDialogShow()
        'MessageDialogクラスでのダイアログボックス表示
        Dim d As New MessageDialog("コンテンツ", "タイトル")
        Await d.ShowAsync()

    End Sub

    Private Async Sub ContentDialogShow()
        'ContentDialogクラスでのダイアログボックス表示
        Dim d As New ContentDialog()
        d.Content = "本文"
        d.Title = "タイトル"
        d.PrimaryButtonText = "OK"
        d.SecondaryButtonText = "キャンセル"
        Dim res = Await d.ShowAsync() 'モーダルダイアログ
        If res = ContentDialogResult.Primary Then
            'Button1.Content = "OKが押されました"
        End If

    End Sub

    Private Sub MainPage1_Loaded(sender As Object, e As RoutedEventArgs) Handles MainPage1.Loaded

        Dim result As Boolean = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TryResizeView(New Size() With {.Width = 800, .Height = 600})

    End Sub
End Class
