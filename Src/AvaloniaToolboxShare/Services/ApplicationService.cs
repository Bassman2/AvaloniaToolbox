//using static System.Net.Mime.MediaTypeNames;

namespace AvaloniaToolbox.Services;

public class ApplicationService : IApplicationService
{
    private ISettingsService settingsService;
    private Application application = Application.Current ?? throw new InvalidOperationException("Application.Current is null.");

    public ApplicationService(ISettingsService settingsService)
    {
        this.settingsService = settingsService;
        actualThemeVariant = application.ActualThemeVariant ?? ThemeVariant.Light;
        ThemeMode = settingsService.Current.ThemeMode;
    }

   
    private ThemeVariant actualThemeVariant = ThemeVariant.Light;
    private ThemeMode currentThemeMode = ThemeMode.System;

    public ThemeMode ThemeMode
    {
        get => currentThemeMode;
        set
        {
            currentThemeMode = value;
            application.RequestedThemeVariant = currentThemeMode switch
            {
                ThemeMode.System => ThemeVariant.Default,
                ThemeMode.Light => ThemeVariant.Light,
                ThemeMode.Dark => ThemeVariant.Dark,
                _ => throw new ArgumentOutOfRangeException(nameof(currentThemeMode), $"Unknown theme mode: {currentThemeMode}")
            };

            settingsService.Current.ThemeMode = value;
            settingsService.Save();
        }
    }

    public void SetThemeVariant(ThemeMode themeVariant)
    {
        
    }

    public void ExitApplication()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }

    public void OpenUrl(string url)
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
        }
        catch
        { }
    }
}