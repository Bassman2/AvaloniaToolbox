namespace AvaloniaToolbox.Services;

public interface IApplicationService
{
    ThemeMode ThemeMode { get; set; }

    void SetThemeVariant(ThemeVariant themeVariant);

    void ExitApplication();
    
    void OpenUrl(string url);
}
