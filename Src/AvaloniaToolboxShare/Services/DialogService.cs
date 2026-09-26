namespace AvaloniaToolbox.Services;

public class DialogService : IDialogService
{
    public async Task<bool> ShowDialogAsync<TViewModel>(TViewModel viewModel)
        where TViewModel : DialogViewModelBase
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop || desktop.MainWindow is not { } ownerWindow)
        {
            return default!;
        }

        // View aus Registry holen (wird intern über Ioc.Default aufgelöst)
        Window dialogWindow = DialogCollection.CreateViewForViewModel(viewModel);

        viewModel.CloseAction = result =>
        {
            dialogWindow.Close(result);
        };

        dialogWindow.DataContext = viewModel;
        bool dialogResult = await dialogWindow.ShowDialog<bool>(ownerWindow);

        return dialogResult;
    }
}
