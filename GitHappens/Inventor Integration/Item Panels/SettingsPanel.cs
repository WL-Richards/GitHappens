using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHappens.Inventor_Integration.Item_Panels
{
    class SettingsPanel
    {
        // Settings
        private ButtonDefinition btn_GitSettings;
        private RibbonPanel settingsPanel;

        public void createSettingsPanel(RibbonTab versionControlTab, string envName)
        {
            settingsPanel = versionControlTab.RibbonPanels.Add("Git Settings", "Autodesk:VCS:Settings", Guid.NewGuid().ToString());

            btn_GitSettings = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Settings",
                string.Format("Autodesk:VCS:SettingsButton:{0}", envName), 
                CommandTypesEnum.kNonShapeEditCmdType, 
                Guid.NewGuid().ToString(), "", "", 
                IconManager.smallSettingsPicture, 
                IconManager.largeSettingsPicture);

            btn_GitSettings.OnExecute += onOpenGitSettings;

            settingsPanel.CommandControls.AddButton(btn_GitSettings, true);
        }

        private void onOpenGitSettings(NameValueMap Context)
        {
            new Settings.GitSettings().Show();
        }

        /// <summary>
        /// Cleanup and remove all bindings for closure
        /// </summary>
        public void Close()
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
