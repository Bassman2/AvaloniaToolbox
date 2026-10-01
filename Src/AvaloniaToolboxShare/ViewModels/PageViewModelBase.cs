using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaToolbox.ViewModels;

public abstract partial class PageViewModelBase : ObservableObject // ObservableValidator //ObservableRecipient
{
    //public IMessenger Messenger { get; protected set; } = null!;

    //protected PageViewModelBase()
    //{
    //    // Verhindert Reflection-Warnungen bezüglich der Messenger-Auflösung
    //    Messenger = WeakReferenceMessenger.Default;
    //}

    public abstract string Title { get; }

    public abstract string IconKey { get; } // String für ein Icon-Symbol
}
