namespace AvaloniaToolbox.Services;

public interface IApplicationService
{
    ThemeMode ThemeMode { get; set; }

    void ExitApplication();
    
    void OpenUrl(string url);
}
