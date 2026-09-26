namespace AvaloniaToolbox.Services;

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNameCaseInsensitive = true, UseStringEnumConverter = true)]
[JsonSerializable(typeof(AppSettingsBase))]
internal partial class AppSettingsBaseJsonContext : JsonSerializerContext
{ }

public class AppSettingsBase
{
    public ThemeMode ThemeMode { get; set; } = ThemeMode.System;
    public double WindowWidth { get; set; } = 900;
    public double WindowHeight { get; set; } = 600;
    public int WindowX { get; set; } = -1;
    public int WindowY { get; set; } = -1;
    public WindowState LastWindowState { get; set; } = WindowState.Normal;
}
