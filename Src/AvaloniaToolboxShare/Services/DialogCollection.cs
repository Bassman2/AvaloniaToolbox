using System.Diagnostics.CodeAnalysis;

namespace AvaloniaToolbox.Services;

public static class DialogCollection
{
    private static readonly Dictionary<Type, Func<Window>> collection = new();

    public static IServiceCollection AddDialog<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TView>(this IServiceCollection services) 
        where TView : Window
        where TViewModel : class
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        services.AddTransient<TViewModel>();
        services.AddTransient<TView>();
        collection[typeof(TViewModel)] = () => Ioc.Default.GetRequiredService<TView>();
        return services;
    }

    //public static void Register<TViewModel, TView>() where TView : Window
    //{
    //    // AOT-sicher: Die Factory löst den konkreten Typ zur Laufzeit über Ioc.Default auf
    //    collection[typeof(TViewModel)] = () => Ioc.Default.GetRequiredService<TView>();
    //}

    public static Window CreateViewForViewModel(object viewModel)
    {
        if (collection.TryGetValue(viewModel.GetType(), out var factory))
        {
            return factory();
        }
        throw new InvalidOperationException($"No view registered for {viewModel.GetType()}.");
    }
}
