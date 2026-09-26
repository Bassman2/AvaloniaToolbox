namespace AvaloniaToolbox.ViewModels;

public partial class AppViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string StatusText { get; set; } = "Ready";

    [RelayCommand]
    public void OnExit()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }

    //=> applicationService.ExitApplication();

    //public ThemeMode CurrentTheme => applicationService.ThemeMode;


    //[RelayCommand]
    //public void OnSetMode(ThemeMode themeMode) => applicationService.ThemeMode = themeMode;


    //[RelayCommand]
    //public void OnHelp() => applicationService.OpenUrl(applicationHelpUrl);
}
