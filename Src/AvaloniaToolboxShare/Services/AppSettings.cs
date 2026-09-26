namespace AvaloniaToolbox.Services;

public class AppSettings
{
    public ThemeMode ThemeMode { get; set; } = ThemeMode.System;

    public double WindowWidth { get; set; } = 900;
    public double WindowHeight { get; set; } = 600;
    public int WindowX { get; set; } = -1;
    public int WindowY { get; set; } = -1;

    // Speichert, ob das Fenster Maximiert oder Normal war
    public WindowState LastWindowState { get; set; } = WindowState.Normal;
}
