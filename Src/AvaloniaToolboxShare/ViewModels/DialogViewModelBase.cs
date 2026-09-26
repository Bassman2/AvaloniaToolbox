namespace AvaloniaToolbox.ViewModels;

//public abstract partial class DialogViewModelBase<TResult> : ObservableObject
//{
//    public Action<TResult>? CloseAction { get; init; }

//    [ObservableProperty]
//    public partial string Title { get; set; } = string.Empty;


//    protected void Close(TResult result) => CloseAction?.Invoke(result);
   

//    [RelayCommand]
//    protected virtual void OnCancel() => Close(default!);
//}

public abstract partial class DialogViewModelBase : ObservableObject
{
    public Action<bool>? CloseAction { get; set; }

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;


    protected void Close(bool result) => CloseAction?.Invoke(result);


    [RelayCommand]
    protected virtual void OnCancel() => Close(default!);

    [RelayCommand]
    protected virtual void OnOK() => Close(true);
}
