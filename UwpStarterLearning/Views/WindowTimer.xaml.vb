' 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

Imports Windows.UI.Popups
''' <summary>
''' それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
''' </summary>
Public NotInheritable Class WindowTimer
    Inherits Page

    Private Sub WindowTimer1_Loaded(sender As Object, e As RoutedEventArgs) Handles WindowTimer1.Loaded

        Dim result As Boolean = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TryResizeView(New Size() With {.Width = 800, .Height = 600})

    End Sub

    Private Sub ButtonDispaTimer_Click(sender As Object, e As RoutedEventArgs) Handles ButtonDispaTimer.Click

    End Sub
End Class
