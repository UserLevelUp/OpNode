using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pWordLib.Services
{
    public class ToolBarConfigurationService
    {
        private readonly ConfigurationDetectionService _configService;

        public ToolBarConfigurationService()
        {
            _configService = new ConfigurationDetectionService();
        }

        /// <summary>
        /// Automatically configures a toolbar button from app.config
        /// </summary>
        public void ConfigureToolBarButton(ToolBarButton button, string buttonName)
        {
            try
            {
                var enabled = _configService.GetConfigValue($"{buttonName}.Enabled", true);
                var imageIndex = _configService.GetConfigValue($"{buttonName}.ImageIndex", -1);
                var pushed = _configService.GetConfigValue($"{buttonName}.Pushed", false);
                var toolTipText = _configService.GetConfigValue($"{buttonName}.ToolTipText", "");

                button.Enabled = enabled;
                if (imageIndex >= 0)
                    button.ImageIndex = imageIndex;
                button.Pushed = pushed;
                if (!string.IsNullOrEmpty(toolTipText))
                    button.ToolTipText = toolTipText;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configuring toolbar button {buttonName}: {ex.Message}");
            }
        }

        /// <summary>
        /// Configures an entire toolbar from configuration
        /// </summary>
        public void ConfigureToolBar(ToolBar toolBar, string toolBarName)
        {
            try
            {
                var configs = _configService.GetConfigurationByPattern(toolBarName);
                
                foreach (var config in configs)
                {
                    ApplyToolBarConfiguration(toolBar, config.Key, config.Value);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configuring toolbar {toolBarName}: {ex.Message}");
            }
        }

        private void ApplyToolBarConfiguration(ToolBar toolBar, string key, object value)
        {
            var parts = key.Split('.');
            if (parts.Length < 2) return;

            var propertyName = parts[parts.Length - 1];
            
            try
            {
                var property = toolBar.GetType().GetProperty(propertyName);
                if (property != null && property.CanWrite)
                {
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(toolBar, convertedValue);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying toolbar config {key}: {ex.Message}");
            }
        }
    }
}