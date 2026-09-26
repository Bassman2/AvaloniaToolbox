using System.Text.Json.Serialization.Metadata;

namespace AvaloniaToolbox.Services;


public class SettingsService<T> : ISettingsService<T>
    where T : AppSettingsBase, new()
{
    private const string AppFolderName = "MasterGroupManager";
    private const string FileName = "settings.json";

    private readonly string filePath;

    private readonly JsonSerializerContext context;


    public T Current { get; }

    public AppSettingsBase CurrentBase => (AppSettingsBase)Current;

    public SettingsService(JsonSerializerContext context)
    {
        this.context = context;
        string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        filePath = Path.Combine(baseFolder, AppFolderName, FileName);

        Current = Load();
    }

    private T Load()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                JsonTypeInfo<T> jsonTypeInfo = (JsonTypeInfo<T>)context.GetTypeInfo(typeof(T))!;
                var loadedSettings = JsonSerializer.Deserialize(json, jsonTypeInfo);

                if (loadedSettings != null)
                {
                    return loadedSettings;
                }
            }
        }
        catch (Exception ex)
        {
            // Optional: Hier Logging integrieren (z. B. Serilog)
            System.Diagnostics.Debug.WriteLine($"Fehler beim Laden der Einstellungen: {ex.Message}");
        }

        // Fallback: Wenn keine Datei existiert oder ein Fehler auftrat, neue Instanz erzeugen
        return new();
    }

    /// <summary>
    /// Speichert die aktuellen Einstellungen im Hintergrund ab.
    /// </summary>
    public void Save()
    {
        try
        {
            // Sicherstellen, dass das Verzeichnis existiert
            string? directory = Path.GetDirectoryName(filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            JsonTypeInfo<T> jsonTypeInfo = (JsonTypeInfo<T>)context.GetTypeInfo(typeof(T))!;
            string json = JsonSerializer.Serialize(Current, jsonTypeInfo);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Fehler beim Speichern der Einstellungen: {ex.Message}");
        }
    }
}
