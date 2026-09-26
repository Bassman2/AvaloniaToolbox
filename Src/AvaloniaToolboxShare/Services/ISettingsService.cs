namespace AvaloniaToolbox.Services;

public interface ISettingsService
{
    AppSettingsBase CurrentBase { get; }

    void Save();
}

public interface ISettingsService<T> : ISettingsService where T : AppSettingsBase
{
    T Current { get; }
}
