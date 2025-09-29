using System;
using System.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace pWordLib.Services
{
    public class ConfigurationDetectionService
    {
        private readonly AppSettingsReader _configReader;
        private readonly Dictionary<string, object> _configCache;

        public ConfigurationDetectionService()
        {
            _configReader = new AppSettingsReader();
            _configCache = new Dictionary<string, object>();
        }

        /// <summary>
        /// Automatically detects and retrieves configuration for any object
        /// </summary>
        public T GetConfigValue<T>(string key, T defaultValue = default(T))
        {
            try
            {
                if (_configCache.ContainsKey(key))
                {
                    return (T)_configCache[key];
                }

                var value = (T)_configReader.GetValue(key, typeof(T));
                _configCache[key] = value;
                return value;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets all configuration keys that match a pattern
        /// </summary>
        public Dictionary<string, object> GetConfigurationByPattern(string pattern)
        {
            var result = new Dictionary<string, object>();
            var config = ConfigurationManager.AppSettings;
            
            foreach (string key in config.AllKeys)
            {
                if (key.Contains(pattern))
                {
                    try
                    {
                        // Try to determine the type from the value
                        var value = config[key];
                        var typedValue = ConvertToAppropriateType(value);
                        result[key] = typedValue;
                    }
                    catch (Exception ex)
                    {
                        // Log the error but continue processing
                        System.Diagnostics.Debug.WriteLine($"Error reading config key {key}: {ex.Message}");
                    }
                }
            }
            
            return result;
        }

        private object ConvertToAppropriateType(string value)
        {
            // Try bool first
            if (bool.TryParse(value, out bool boolResult))
                return boolResult;
            
            // Try int
            if (int.TryParse(value, out int intResult))
                return intResult;
            
            // Try double
            if (double.TryParse(value, out double doubleResult))
                return doubleResult;
            
            // Default to string
            return value;
        }
    }
}