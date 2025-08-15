using System;
using System.Collections.Generic;
using System.Configuration;

namespace Test_WindowsForms
{
    public static class TestConfigurationService
    {
        private static readonly Dictionary<string, object> _defaultValues = new Dictionary<string, object>
        {
            ["txtValue.AcceptsReturn"] = true,
            ["txtValue.AcceptsTab"] = true,
            ["txtValue.AllowDrop"] = true,
            ["txtValue.Enabled"] = true,
            ["txtValue.Multiline"] = true,
            ["txtValue.ScrollBars"] = "Both",
            ["txtValue.WordWrap"] = true,
            ["toolBarButton1.Enabled"] = true,
            ["toolBarButton1.ImageIndex"] = 0,
            ["toolBarButton1.Pushed"] = false,
            ["treeView1.Scrollable"] = true,
            ["pWord.Enabled"] = true,
            ["pWord.TopMost"] = false
        };

        public static T GetConfigValue<T>(string key, T defaultValue = default(T))
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];
                if (value != null)
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            catch
            {
                // Fall through to return default value
            }

            if (_defaultValues.ContainsKey(key))
            {
                return (T)_defaultValues[key];
            }

            return defaultValue;
        }
    }
}