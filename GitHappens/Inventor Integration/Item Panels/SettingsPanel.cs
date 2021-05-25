using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHappens.Inventor_Integration.Item_Panels
{
    class SettingsPanel
    {
        // Settings
        private ButtonDefinition btn_GitSettings;
        private ButtonDefinition btn_CreateRepo;
        private RibbonPanel settingsPanel;

        public void createSettingsPanel(RibbonTab versionControlTab, string envName)
        {
            settingsPanel = versionControlTab.RibbonPanels.Add("Advanced Options", "Autodesk:VCS:Settings", Guid.NewGuid().ToString());

            btn_CreateRepo = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Create New\nCAD Repo",
                string.Format("Autodesk:VCS:CreateRepoButton:{0}", envName),
                CommandTypesEnum.kNonShapeEditCmdType,
                Guid.NewGuid().ToString(), "", "",
                IconManager.smallCreateRepoPicture,
                IconManager.largeCreateRepoPicture);

            btn_CreateRepo.OnExecute += onCreateRepo;

            btn_GitSettings = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Settings",
                string.Format("Autodesk:VCS:SettingsButton:{0}", envName), 
                CommandTypesEnum.kNonShapeEditCmdType, 
                Guid.NewGuid().ToString(), "", "", 
                IconManager.smallSettingsPicture, 
                IconManager.largeSettingsPicture);



            btn_GitSettings.OnExecute += onOpenGitSettings;

            settingsPanel.CommandControls.AddButton(btn_CreateRepo, true);
            settingsPanel.CommandControls.AddButton(btn_GitSettings, true);
        }

        private void onCreateRepo(NameValueMap Context)
        {
            new Git_Settings.RepoWizard().ShowDialog();
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

                btn_CreateRepo.Delete();
                btn_CreateRepo.OnExecute -= onCreateRepo;
            }
        }

    }
}
