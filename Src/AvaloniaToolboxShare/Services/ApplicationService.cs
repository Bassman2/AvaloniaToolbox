using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;

namespace AvaloniaToolbox.Services;

public class ApplicationService : IApplicationService
{
    private ISettingsService settingsService;
    private Application application = Application.Current ?? throw new InvalidOperationException("Application.Current is null.");

    public ApplicationService()
    {
        settingsService = Ioc.Default.GetRequiredService<ISettingsService>();
        
        actualThemeVariant = application.ActualThemeVariant ?? ThemeVariant.Light;
        ThemeMode = settingsService.CurrentBase.ThemeMode;
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

            settingsService.CurrentBase.ThemeMode = value;
            settingsService.Save();
        }
    }

    public void SetThemeVariant(ThemeVariant themeVariant)
    {
        if (Avalonia.Application.Current is { } app)
        {
            app.RequestedThemeVariant = themeVariant;
        }
    }

    public void SetThemeVariant(ThemeMode themeVariant)
    {
        if (Avalonia.Application.Current is { } app)
        {
            app.RequestedThemeVariant = themeVariant switch
            {
                ThemeMode.Light => ThemeVariant.Light,
                ThemeMode.Dark => ThemeVariant.Dark,
                _ => throw new InvalidOperationException()
            };
        }
    }

    //public void SetThemeVariant(ThemeVariant themeVariant)
    //{
    //    if (Avalonia.Application.Current is { } app)
    //    {
    //        app.RequestedThemeVariant = themeVariant;
    //    }
    //}

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

    public async Task CopyToClipboardAsync(string textToCopy)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var clipboard = desktop.MainWindow?.Clipboard;

            if (clipboard is not null)
            {
                await clipboard.SetTextAsync(textToCopy);
            }
        }
    }
}


public static class ApplicationServiceExtensions
{
    
    public static IServiceCollection AddSettings<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(this IServiceCollection services, JsonSerializerContext contect)
        where T : AppSettingsBase, new()
    {
        services.AddSingleton<JsonSerializerContext>(sp => contect);
        services.AddSingleton<SettingsService<T>>();
        services.AddSingleton<ISettingsService>(sp => sp.GetRequiredService<SettingsService<T>>());
        services.AddSingleton<ISettingsService<T>>(sp => sp.GetRequiredService<SettingsService<T>>());

        return services;
    }
}