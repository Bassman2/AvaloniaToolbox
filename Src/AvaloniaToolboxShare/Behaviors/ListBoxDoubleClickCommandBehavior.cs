using Avalonia;

namespace AvaloniaToolbox.Behaviors;


public class ListBoxItemDoubleClickBehavior : Behavior<ListBox>
{
    public static readonly AvaloniaProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<ListBoxItemDoubleClickBehavior, ICommand?>(nameof(Command));

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject?.DoubleTapped += OnDoubleTapped;
    }

    protected override void OnDetaching()
    {
        AssociatedObject?.DoubleTapped -= OnDoubleTapped;
        base.OnDetaching();
    }

    private void OnDoubleTapped(object? sender, RoutedEventArgs e)
    {
        if (Command is { } command && AssociatedObject?.SelectedItem is { } selectedItem && command.CanExecute(selectedItem))
        {
            command.Execute(selectedItem);
        }
    }
}
