namespace AvaloniaToolbox.Behaviors;

public class ListBoxScrollIntoViewBehavior : Behavior<ListBox>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        if (AssociatedObject != null)
        {
            // Event abonnieren
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject != null)
        {
            // Event sauber wieder entfernen
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
        }
        base.OnDetaching();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (AssociatedObject?.SelectedItem != null)
        {
            // Scrollt das ausgewählte Element in den sichtbaren Bereich
            AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem);
        }
    }
}
