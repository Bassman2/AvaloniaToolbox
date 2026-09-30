namespace AvaloniaToolbox.ViewModels;

public abstract partial class DialogViewModelBase : ObservableObject
{
    public Action<bool>? CloseAction { get; set; }

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string OKText { get; set; } = "OK";


    protected void Close(bool result) => CloseAction?.Invoke(result);


    [RelayCommand]
    protected virtual void OnCancel() => Close(default!);

    [RelayCommand]
    protected virtual void OnOK() => Close(true);
}
