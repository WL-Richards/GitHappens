using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHappens.AddIn_Assistant.Item_Panels;
using System.Windows.Forms;

namespace GitHappens.AddIn_Assistant
{
    /// <summary>
    /// Created for the purpose of managing information related to the current inventor environment from a single easy to reference point
    /// </summary>
    class EnvironmentManager
    {
        // Points of reference for the currently open document and environment
        private static string CurrentDocument = "";
        private static string CurrentEnvironment = "";

        private static RibbonTab versionControlTab;

        /// <summary>
        /// Handles Changing of Environments in Inventor 
        /// </summary>
        /// <param name="Environment">New Environment</param>
        /// <param name="EnvironmentState">The Current State of the Environment (eg. has this been loaded prior)</param>
        /// <param name="BeforeOrAfter">Where in the environment transition process we are</param>
        /// <param name="Context">Context pertaining to the handler call</param>
        /// <param name="HandlingCode">Whether or not the handler was successfully handled</param>
        public static void onEnvironmentChange(Inventor.Environment Environment, EnvironmentStateEnum EnvironmentState, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            // Check if the environment state changing is creating a new environment or resuming a previously opened one while also making sure the following is only run once per environment init
            if ((EnvironmentState == EnvironmentStateEnum.kActivateEnvironmentState || EnvironmentState == EnvironmentStateEnum.kResumeEnvironmentState) && CurrentEnvironment != Environment.DisplayName)
            {

                // Set the CurrentEnvironment variable so this code is only run once per change

                setCurrentEnvironment(Environment.DisplayName);

                // Convert the Environment name "Inventor" to ZeroDoc as that is its internal name
                if (getCurrentEnvironment().Equals("Inventor"))
                {
                    setCurrentEnvironment("ZeroDoc");
                }

                if(getCurrentEnvironment().Equals("2D Sketch"))
                {
                    setCurrentEnvironment("Part");
                }

                EnvironmentManager.cleanUpUI();
                createUserInterface(AddInSetup.getUIManager(), false);
                

                // Finally set the handling state of the handler
                HandlingCode = HandlingCodeEnum.kEventHandled;
            }
            else
            {
                // If it was not the type of environment change we wanted and thus did not handle it set the HandlingCode accordingly 
                HandlingCode = HandlingCodeEnum.kEventNotHandled;
            }
        }


        /// <summary>
        /// Create the Version Control Tab and sub panels
        /// </summary>
        /// <param name="inGitRepo"></param>
        /// <param name="versionControlTab"></param>
        /// <param name="uiManger"></param>
        public static void createUserInterface(UserInterfaceManager uiManger, bool inGitRepo)
        {
            // Create the version control tab for the current environment
            versionControlTab = uiManger.Ribbons[getCurrentEnvironment()].RibbonTabs.Add("Version Control", "Autodesk:VCS", Guid.NewGuid().ToString());

            // Create the Git Panel 
            GitPanel.createBasicGitPanel(versionControlTab, inGitRepo);

            // Create the file management panel
            FileManagementPanel.createFileManagementPanel(versionControlTab, inGitRepo);

            // Create the git settings file
            SettingsPanel.createSettingsPanel(versionControlTab);
        }

        /// <summary>
        /// Handles cleanup of non-null objects
        /// </summary>
        public static void cleanUpUI()
        {
            GitPanel.Close();
            FileManagementPanel.Close();
            SettingsPanel.Close();

            if (versionControlTab != null)
                versionControlTab.Delete();
        }

        /// <summary>
        /// Getter for the current document
        /// </summary>
        /// <returns>Current Document's name</returns>
        public static string getCurrrentDocument()
        {
            return CurrentDocument;
        }

        /// <summary>
        /// Getter for the current environment
        /// </summary>
        /// <returns>The current environment's name</returns>
        public static string getCurrentEnvironment()
        {
            return CurrentEnvironment;
        }
        
        /// <summary>
        /// Sets the current document name to track the value globally
        /// </summary>
        /// <param name="currentDocument">New document name</param>
        public static void setCurrrentDocument(string currentDocument)
        {
            CurrentDocument = currentDocument;
        }

        /// <summary>
        /// Sets the current environment name to track the value globally
        /// </summary>
        /// <param name="currentEnvironment">New environment name</param>
        public static void setCurrentEnvironment(string currentEnvironment)
        {
            CurrentEnvironment = currentEnvironment;
        }

    }
}
