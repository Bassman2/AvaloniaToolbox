using AvaloniaToolbox.Services;

namespace AvaloniaToolbox.ViewModels;

public partial class AppViewModel : ObservableObject
{
    protected readonly IApplicationService applicationService;
    protected readonly IDialogService dialogService;

    public AppViewModel()
    {
        applicationService = Ioc.Default.GetRequiredService<IApplicationService>();
        dialogService = Ioc.Default.GetRequiredService<IDialogService>();
    }

    public static bool IsMacPlatform => OperatingSystem.IsMacOS();

    [ObservableProperty]
    public partial string StatusText { get; set; } = "Ready";

    [RelayCommand]
    public void OnExit() => applicationService.ExitApplication();

    [RelayCommand]
    public void OnSetThemeVariant(string name) => applicationService.SetThemeVariant(new ThemeVariant(name, null));
}
