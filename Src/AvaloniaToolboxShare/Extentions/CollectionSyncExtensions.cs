using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AvaloniaToolbox.Extentions;

public static class CollectionSyncExtensions
{
    // Die Factory-Methode für den Primary Constructor
    public static ObservableCollection<TViewModel> CreateAndSync<TViewModel, TModel>(
        IEnumerable<TViewModel> initialItems,
        IList<TModel> modelList,
        Func<TViewModel, TModel> getModelSelector)
    {
        // 1. Erstelle die Collection aus den initialen Daten
        var collection = new ObservableCollection<TViewModel>(initialItems);

        // 2. Registriere die Synchronisations-Logik (Hinzufügen, Entfernen, Move)
        collection.SynchronizeWithModel(modelList, getModelSelector);

        // 3. Gib die fertig konfigurierte Collection zurück
        return collection;
    }

    // Deine bestehende Synchronisations-Methode (wird von oben aufgerufen)
    public static void SynchronizeWithModel<TViewModel, TModel>(
        this ObservableCollection<TViewModel> uiCollection,
        IList<TModel> modelList,
        Func<TViewModel, TModel> getModelSelector)
    {
        uiCollection.CollectionChanged += (sender, e) =>
        {
            switch (e.Action)
            {
            case NotifyCollectionChangedAction.Add when e.NewItems != null:
                int index = e.NewStartingIndex;
                foreach (TViewModel vm in e.NewItems)
                {
                    modelList.Insert(index++, getModelSelector(vm));
                }
                break;

            case NotifyCollectionChangedAction.Remove when e.OldItems != null:
                foreach (TViewModel vm in e.OldItems)
                {
                    modelList.Remove(getModelSelector(vm));
                }
                break;

            case NotifyCollectionChangedAction.Move:
                var modelToMove = modelList[e.OldStartingIndex];
                modelList.RemoveAt(e.OldStartingIndex);
                modelList.Insert(e.NewStartingIndex, modelToMove);
                break;

            case NotifyCollectionChangedAction.Replace when e.NewItems != null:
                int replaceIndex = e.NewStartingIndex;
                foreach (TViewModel vm in e.NewItems)
                {
                    modelList[replaceIndex++] = getModelSelector(vm);
                }
                break;
            case NotifyCollectionChangedAction.Reset:
                modelList.Clear();
                foreach (TViewModel vm in uiCollection)
                {
                    modelList.Add(getModelSelector(vm));
                }
                break;
            }
        };
    }
}