using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHappens.AddIn_Assistant.Item_Panels
{
    class SettingsPanel
    {
        // Settings
        private static ButtonDefinition btn_GitSettings;
        private static RibbonPanel settingsPanel;

        public static void createSettingsPanel(RibbonTab versionControlTab)
        {
            settingsPanel = versionControlTab.RibbonPanels.Add("Git Settings", "Autodesk:VCS:Settings", Guid.NewGuid().ToString());

            btn_GitSettings = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Settings", 
                "Autodesk:VCS:SettingsButton", 
                CommandTypesEnum.kNonShapeEditCmdType, 
                Guid.NewGuid().ToString(), "", "", 
                IconManager.smallSettingsPicture, 
                IconManager.largeSettingsPicture);

            btn_GitSettings.OnExecute += onOpenGitSettings;

            settingsPanel.CommandControls.AddButton(btn_GitSettings, true);
        }

        private static void onOpenGitSettings(NameValueMap Context)
        {
            new Settings.GitSettings().Show();
        }

        /// <summary>
        /// Cleanup and remove all bindings for closure
        /// </summary>
        public static void Close()
        {
            if(settingsPanel != null)
            {
                settingsPanel.Delete();

                btn_GitSettings.Delete();
                btn_GitSettings.OnExecute -= onOpenGitSettings;
            }
        }

    }
}
