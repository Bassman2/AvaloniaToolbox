namespace AvaloniaToolbox.Services;

[JsonSerializable(typeof(AppSettings))]
internal partial class AppSettingsJsonContext : JsonSerializerContext
{
}

public class SettingsService : ISettingsService
{
    private const string AppFolderName = "MasterGroupManager";
    private const string FileName = "settings.json";

    private readonly string filePath;

   
    public AppSettings Current { get; }

    public SettingsService()
    {
        string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        filePath = Path.Combine(baseFolder, AppFolderName, FileName);

        Current = Load();
    }

    private AppSettings Load()
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
   
                var loadedSettings = JsonSerializer.Deserialize(json, AppSettingsJsonContext.Default.AppSettings);

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
        return new AppSettings();
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

            string json = JsonSerializer.Serialize(Current, AppSettingsJsonContext.Default.AppSettings);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Fehler beim Speichern der Einstellungen: {ex.Message}");
        }
    }
}
