namespace AvaloniaToolbox.ViewModels;

public abstract partial class PageViewModelBase : ObservableRecipient
{
    public abstract string Title { get; }

    public abstract string IconKey { get; } // String für ein Icon-Symbol
}
