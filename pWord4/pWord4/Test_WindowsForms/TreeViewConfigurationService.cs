using System;
using System.Windows.Forms;

namespace pWordLib.Services
{
    public class TreeViewConfigurationService
    {
        private readonly ConfigurationDetectionService _configService;

        public TreeViewConfigurationService()
        {
            _configService = new ConfigurationDetectionService();
        }

        /// <summary>
        /// Automatically configures a TreeView from app.config
        /// </summary>
        public void ConfigureTreeView(TreeView treeView, string treeViewName)
        {
            try
            {
                var scrollable = _configService.GetConfigValue($"{treeViewName}.Scrollable", true);
                var showLines = _configService.GetConfigValue($"{treeViewName}.ShowLines", true);
                var showPlusMinus = _configService.GetConfigValue($"{treeViewName}.ShowPlusMinus", true);
                var showRootLines = _configService.GetConfigValue($"{treeViewName}.ShowRootLines", true);
                var fullRowSelect = _configService.GetConfigValue($"{treeViewName}.FullRowSelect", false);
                var hotTracking = _configService.GetConfigValue($"{treeViewName}.HotTracking", false);
                var hideSelection = _configService.GetConfigValue($"{treeViewName}.HideSelection", true);

                treeView.Scrollable = scrollable;
                treeView.ShowLines = showLines;
                treeView.ShowPlusMinus = showPlusMinus;
                treeView.ShowRootLines = showRootLines;
                treeView.FullRowSelect = fullRowSelect;
                treeView.HotTracking = hotTracking;
                treeView.HideSelection = hideSelection;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configuring TreeView {treeViewName}: {ex.Message}");
            }
        }
    }
}