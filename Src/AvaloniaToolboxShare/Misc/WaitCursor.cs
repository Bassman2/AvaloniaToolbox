namespace AvaloniaToolbox.Misc;

public sealed class WaitCursor : IDisposable
{
    private readonly Window? window;
    private readonly Cursor? previousCursor;
    private bool isDisposed;

    public WaitCursor()
    {
        // Holt das aktuelle Hauptfenster direkt aus der Avalonia-Infrastruktur
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow is Window mainWindow)
        {
            window = mainWindow;
            previousCursor = window.Cursor;

            // Setzt den Cursor auf den Standard-Ladekreis
            SetCursor(Cursor.Parse("Wait"));
        }
    }

    public void Dispose()
    {
        if (isDisposed || window == null) return;
        isDisposed = true;

        // Setzt den alten Cursor wieder zurück
        SetCursor(previousCursor);
    }

    private void SetCursor(Cursor? cursor)
    {
        if (Dispatcher.UIThread.CheckAccess())
        {
            window!.Cursor = cursor;
        }
        else
        {
            Dispatcher.UIThread.Post(() => window!.Cursor = cursor);
        }
    }
}