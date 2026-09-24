namespace AvaloniaToolboxShare.Behaviors;

public class ScrollIntoViewBehavior : Behavior<ListBox>
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


        //if (sender is ListBox listBox && listBox.SelectedItem is not null)
        //{
        //    listBox.Dispatcher.BeginInvoke(() =>
        //    {
        //        listBox.UpdateLayout();
        //        listBox.ScrollIntoView(listBox.SelectedItem);
        //    });
        //}
    }
}
