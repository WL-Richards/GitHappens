using GitHappens.Inventor_Integration.Item_Panels;
using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHappens.Inventor_Integration.UI_Management
{
    /// <summary>
    /// Object to contain the RibbonTab itself and it's corresponding panels
    /// </summary>
    class RibbonTabContainer
    {

        public string environmentName;

        public RibbonTab versionControlTab;
        public GitPanel gitPanel;
        public SettingsPanel settingsPanel;
        public FileManagementPanel fileManagementPanel;

        /// <summary>
        /// Create the new container for ribbons and buttons
        /// </summary>
        /// <param name="environmentName"></param>
        /// <param name="uiManger"></param>
        public RibbonTabContainer(string environmentName, UserInterfaceManager uiManger)
        {
            // Keep a copy of the environment name
            this.environmentName = environmentName;

            // Create objects for each of the panels
            gitPanel = new GitPanel();
            settingsPanel = new SettingsPanel();
            fileManagementPanel = new FileManagementPanel();

            // Create new version control tab for this environment
            versionControlTab = uiManger.Ribbons[EnvironmentManager.getCurrentEnvironment()].RibbonTabs.Add("Version Control", "Autodesk:VCS", Guid.NewGuid().ToString());

            // Create all the panels
            // NOTE: FADE OUT THE inGitRepo param
            gitPanel.createBasicGitPanel(versionControlTab,  environmentName);
            fileManagementPanel.createFileManagementPanel(versionControlTab, environmentName);
            settingsPanel.createSettingsPanel(versionControlTab, environmentName);
        }

        /// <summary>
        /// Clean up all UI elements
        /// </summary>
        public void Close()
        {
            gitPanel.Close();
            settingsPanel.Close();
            fileManagementPanel.Close();
            versionControlTab.Delete();
        }

        public void disableButtons()
        {
            gitPanel.disableButtons();
            fileManagementPanel.disableButtons();
        }

        public void enableButtons()
        {
            gitPanel.enableButtons();
            fileManagementPanel.enableButtons();
        }
    }
}
