namespace AvaloniaToolbox.Behaviors;

public class ListBoxScrollIntoViewBehavior : Behavior<ListBox>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject?.SelectionChanged += OnSelectionChanged;
    }

    protected override void OnDetaching()
    {
        AssociatedObject?.SelectionChanged -= OnSelectionChanged;
        base.OnDetaching();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (AssociatedObject?.SelectedItem != null)
        {
            AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem);
        }
    }
}
