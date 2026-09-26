namespace AvaloniaToolbox.Services;

public interface ISettingsService
{
    AppSettings Current { get; }

    void Save();
}
