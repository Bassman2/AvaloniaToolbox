namespace AvaloniaToolbox.Misc;

public class AppSettings
{
    public string LastUsedUser { get; set; } = string.Empty;
    public bool EnabledOnly { get; set; } = false;
    public string Theme { get; set; } = "Dark";
}

// 2. Der statische Manager, der die Logik kapselt
public static class SettingsManager
{
    private static readonly string AppDataFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "MasterGroupManager"
    );

    private static readonly string UserSettingsPath = Path.Combine(AppDataFolder, "user-settings.json");
    private static readonly string FallbackSettingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    // Das statische Datenfeld, das die aktuellen Werte im Speicher hält
    public static AppSettings Current { get; }

    // Der statische Konstruktor lädt die Daten automatisch beim ersten App-Zugriff
    static SettingsManager()
    {
        Current = LoadSettings();
    }

    private static AppSettings LoadSettings()
    {
        try
        {
            // Pfad 1: Benutzerspezifische Einstellungen aus AppData laden
            if (File.Exists(UserSettingsPath))
            {
                string json = File.ReadAllText(UserSettingsPath);
                var root = JsonSerializer.Deserialize<SettingsEnvelope>(json);
                if (root?.AppSettings != null) return root.AppSettings;
            }

            //// Pfad 2: Fallback auf Standardwerte aus dem Installationsverzeichnis
            //if (File.Exists(FallbackSettingsPath))
            //{
            //    string json = File.ReadAllText(FallbackSettingsPath);
            //    var root = JsonSerializer.Deserialize<SettingsEnvelope>(json);
            //    if (root?.AppSettings != null) return root.AppSettings;
            //}
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Fehler beim Laden der Settings: {ex.Message}");
        }

        // Pfad 3: Absoluter Notfall-Fallback, falls keine Datei existiert
        return new AppSettings();
    }

    /// <summary>
    /// Speichert die aktuellen Werte aus 'SettingsManager.Current' im AppData-Ordner.
    /// </summary>
    public static void Save()
    {
        try
        {
            Directory.CreateDirectory(AppDataFolder);

            var envelope = new SettingsEnvelope { AppSettings = Current };
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(envelope, options);

            File.WriteAllText(UserSettingsPath, jsonString);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Fehler beim Speichern der Settings: {ex.Message}");
        }
    }

    // Hilfsstruktur, um das JSON-Format kompatibel zur Standard-appsettings.json zu halten
    private sealed class SettingsEnvelope
    {
        public AppSettings? AppSettings { get; set; }
    }
}
