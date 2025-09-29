using System;
using System.Windows.Forms;

namespace pWordLib.Services
{
    public class pWordConfigurationService
    {
        private readonly ConfigurationDetectionService _configService;
        private readonly ToolBarConfigurationService _toolBarService;
        private readonly TreeViewConfigurationService _treeViewService;

        public pWordConfigurationService()
        {
            _configService = new ConfigurationDetectionService();
            _toolBarService = new ToolBarConfigurationService();
            _treeViewService = new TreeViewConfigurationService();
        }

        /// <summary>
        /// Configures the entire pWord form from app.config
        /// </summary>
        public void ConfigurepWordForm(Form form, string formName = "pWord")
        {
            try
            {
                // Configure form properties
                var enabled = _configService.GetConfigValue($"{formName}.Enabled", true);
                var topMost = _configService.GetConfigValue($"{formName}.TopMost", false);
                var minimumWidth = _configService.GetConfigValue($"{formName}.MinimumSize.Width", 300);
                var minimumHeight = _configService.GetConfigValue($"{formName}.MinimumSize.Height", 200);

                form.Enabled = enabled;
                form.TopMost = topMost;
                form.MinimumSize = new System.Drawing.Size(minimumWidth, minimumHeight);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configuring pWord form: {ex.Message}");
            }
        }

        /// <summary>
        /// Auto-discovers and configures all controls in a form
        /// </summary>
        public void AutoConfigureControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                try
                {
                    switch (control)
                    {
                        case ToolBar toolBar:
                            _toolBarService.ConfigureToolBar(toolBar, control.Name);
                            break;
                        case TreeView treeView:
                            _treeViewService.ConfigureTreeView(treeView, control.Name);
                            break;
                    }

                    // Recursively configure child controls
                    if (control.HasChildren)
                    {
                        AutoConfigureControls(control.Controls);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error auto-configuring control {control.Name}: {ex.Message}");
                }
            }
        }
    }
}