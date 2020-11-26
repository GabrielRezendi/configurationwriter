using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;

namespace configurationwriter
{
    public class ConfigurationWriter
    {
        public string SettingsPath { get; set; }
        public ConfigurationWriter()
        {
            this.SettingsPath = "appsettings.json";
        }
        public ConfigurationWriter(string settingsPath)
        {
            this.SettingsPath = settingsPath;
        }
        public void AddOrUpdateAppSetting<T>(string section, string key, T value)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, SettingsPath);
                dynamic jsonObj = JsonConvert.DeserializeObject(File.ReadAllText(filePath));

                jsonObj[section][key] = value;

                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException($"Failed to write the {SettingsPath}", ex);
            }
        }
        public void AddOrUpdateAppSetting<T>(string key, T value)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                dynamic jsonObj = JsonConvert.DeserializeObject(File.ReadAllText(filePath));

                jsonObj[key] = value;
                
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException($"Failed to write the {SettingsPath}", ex);
            }
        }

    }
}
