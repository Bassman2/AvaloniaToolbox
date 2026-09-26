using AvaloniaToolbox.Services;

namespace AvaloniaToolbox.Views;

public partial class AppView : Window
{
    private readonly ISettingsService settingsService;

    private NativeMenu? docMenu;

    public AppView()
    {
        this.settingsService = Ioc.Default.GetRequiredService<ISettingsService>();
    }

    protected override void OnOpened(System.EventArgs e)
    {
        base.OnOpened(e);

        // Geometrie aus den Einstellungen laden
        var settings = settingsService.Current;

        Width = settings.WindowWidth;
        Height = settings.WindowHeight;
        WindowState = settings.LastWindowState;

        if (settings.WindowX != -1 && settings.WindowY != -1)
        {
            Position = new PixelPoint(settings.WindowX, settings.WindowY);
        }

        ////////////////////////////////////////////

        //if (DataContext is MainViewModel vm && vm.Documentations != null)
        //{
        //    var docMenuItem = NativeMenu.GetMenu(this)?.Items.OfType<NativeMenuItem>().FirstOrDefault(item => item.Header?.ToString() == "_Documentation");
        //    if (docMenuItem != null)
        //    {
        //        docMenu = docMenuItem.Menu ??= new();

        //        RebuildNativeMenu();
        //    }
        //    vm.PropertyChanged += OnViewModelPropertyChanged;
        //}
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        var settings = settingsService.Current;

        // Zustand und Größe sichern
        settings.LastWindowState = WindowState;

        if (WindowState == WindowState.Normal)
        {
            settings.WindowWidth = Width;
            settings.WindowHeight = Height;
            settings.WindowX = Position.X;
            settings.WindowY = Position.Y;
        }

        // Einstellungen speichern
        settingsService.Save();

        base.OnClosing(e);
    }
}
