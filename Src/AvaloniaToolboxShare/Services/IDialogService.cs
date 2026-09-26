namespace AvaloniaToolbox.Services;

public interface IDialogService
{
    Task<bool> ShowDialogAsync<TViewModel>(TViewModel viewModel) where TViewModel : DialogViewModelBase;
}
