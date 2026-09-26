namespace MasterGroupManager.Services;


public interface IDialogViewModel<TResult>
{
    // Die UI-Schicht injiziert diese Aktion, um den Dialog zu schließen
    Action<TResult>? CloseAction { get; set; }
}
