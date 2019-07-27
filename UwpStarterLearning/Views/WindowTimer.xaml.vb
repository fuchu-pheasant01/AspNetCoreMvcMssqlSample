' 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

Imports Windows.UI.Popups
''' <summary>
''' それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
''' </summary>
Public NotInheritable Class WindowTimer
    Inherits Page

    Private WithEvents DispaTmr As New Windows.UI.Xaml.DispatcherTimer()
    Private WithEvents TimersTmr As New System.Timers.Timer()
    Private ThreadingTmr As Threading.Timer
    Private TimerCnt As Integer
    Private DoEventsType As Boolean = True
    Private Sub WindowTimer1_Loaded(sender As Object, e As RoutedEventArgs) Handles WindowTimer1.Loaded

        Dim result As Boolean = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TryResizeView(New Size() With {.Width = 800, .Height = 600})

    End Sub

    Private Sub ButtonDispaTimer_Click(sender As Object, e As RoutedEventArgs) Handles ButtonDispaTimer.Click

        'DispatcherTimerセットアップ
        AddHandler DispaTmr.Tick, AddressOf DispatcherTimer_Tick
        DispaTmr.Interval = New TimeSpan(0, 0, 0, 1, 0)
        TimerCnt = 0
        DispaTmr.Start()

    End Sub

    Private Sub DispatcherTimer_Tick(sender As Object, e As RoutedEventArgs) Handles DispaTmr.Tick

        'DispatcherTimerのイベントハンドラー内はUIスレッドで動作するので時間のかかる処理を行うとその間は他のイベントハンドラーは受け付けない。

        TimerCnt += 1
        '現在の秒を表示するラベルを更新する
        TextBlockTime.Text = "現在時刻 ：" & String.Format(DateTime.Now, "HH:mm:ss")
        'TextBlockTime.Text = "秒カウント：" & TimerCnt

        'CommandManagerにRequerySuggestedイベントを発生させる
        'CommandManager.InvalidateRequerySuggested()

    End Sub

    Private Sub ButtonDispaStop_Click(sender As Object, e As RoutedEventArgs) Handles ButtonDispaStop.Click

        DispaTmr.Stop()

    End Sub

    Private Sub ButtonTimersTimer_Click(sender As Object, e As RoutedEventArgs) Handles ButtonTimersTimer.Click

        AddHandler TimersTmr.Elapsed, New Timers.ElapsedEventHandler(AddressOf TimersTimer_Tick)
        'TimersTmr.SynchronizingObject = Me.LabelTime
        TimersTmr.Interval = 1000
        'TimersTmr.AutoReset = True
        'TimersTmr.Enabled = True
        TimerCnt = 0
        TimersTmr.Start()

    End Sub

    Private Sub TimersTimer_Tick(sender As Object, e As Timers.ElapsedEventArgs) Handles TimersTmr.Elapsed

        'Timers.Timerのイベントハンドラーは別スレッドで実行されるので直接UIスレッドのコントロールにアクセスできない。(スレッドプールで実行される)
        'Invoke、BeginInvoke、EndInvoke
        Dim dummy = Me.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
        Sub()
            TimerCnt += 1
            TextBlockTime.Text = "現在時刻 ：" & String.Format(DateTime.Now, "HH:mm:ss")
            TextBlockCount.Text = "秒カウント：" & TimerCnt
        End Sub)

    End Sub

    Private Sub ButtonTimersStop_Click(sender As Object, e As RoutedEventArgs) Handles ButtonTimersStop.Click

        TimersTmr.Stop()

    End Sub

    Private Sub ButtonThreadingTimer_Click(sender As Object, e As RoutedEventArgs) Handles ButtonThreadingTimer.Click

        'Threading.Timerのイベントハンドラーは別スレッドで実行されるので直接UIスレッドのコントロールにアクセスできない。(スレッドプールで実行される)
        'Dim ThreadingCallback As New System.Threading.TimerCallback(AddressOf ThreadingTimer)
        Dim ThreadingCallback As System.Threading.TimerCallback = Sub(state)
                                                                      Dim o As Object
                                                                      ThreadingTimer(o)
                                                                  End Sub
        TimerCnt = 0
        ThreadingTmr = New Threading.Timer(ThreadingCallback, Nothing, 0, 1000)

    End Sub

    Private Sub ButtonThreadingStop_Click(sender As Object, e As RoutedEventArgs) Handles ButtonThreadingStop.Click

        ThreadingTmr.Change(Threading.Timeout.Infinite, Threading.Timeout.Infinite)

    End Sub

    Private Sub ThreadingTimer(sender As Object)

        Dim dummy = Me.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
        Sub()
            TimerCnt += 1
            TextBlockTime.Text = "現在時刻 ：" & String.Format(DateTime.Now, "HH:mm:ss")
            TextBlockCount.Text = "秒カウント：" & TimerCnt
        End Sub)

    End Sub

End Class
