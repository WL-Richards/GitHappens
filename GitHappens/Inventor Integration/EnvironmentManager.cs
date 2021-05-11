using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHappens.Inventor_Integration.Item_Panels;
using System.Windows.Forms;
using GitHappens.Inventor_Integration.UI_Management;

namespace GitHappens.Inventor_Integration
{
    /// <summary>
    /// Created for the purpose of managing information related to the current inventor environment from a single easy to reference point
    /// </summary>
    class EnvironmentManager
    {
        private static string CurrentEnvironment = "";

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

                // Convert the Environment name to its API name
                if (getCurrentEnvironment().Equals("Inventor")) {setCurrentEnvironment("ZeroDoc");}
                if (getCurrentEnvironment().Equals("2D Sketch")) {setCurrentEnvironment("Part");}

                // Create / Restore the environments interface
                createUserInterface(AddInSetup.getUIManager());
                
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
        public static void createUserInterface(UserInterfaceManager uiManger)
        {
            // Create or restore the UI for the environment
            UIManager.createUI(uiManger);
        }

        /// <summary>
        /// Handles cleanup of non-null objects
        /// </summary>
        public static void cleanUpUI()
        {
           UIManager.Close();
        }

        /// <summary>
        /// Getter for the current document
        /// </summary>
        /// <returns>Current Document's name</returns>
        public static string getCurrentDocument()
        {
            string documentName = "";

            // Verify the ActiveDocument actually exists
            if (ApplicationManager.getInventorApplication().ActiveDocument != null)
            {
                documentName = ApplicationManager.getInventorApplication().ActiveDocument.FullDocumentName;
            }
           
            return documentName;

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
        /// Sets the current environment name to track the value globally
        /// </summary>
        /// <param name="currentEnvironment">New environment name</param>
        public static void setCurrentEnvironment(string currentEnvironment)
        {
            CurrentEnvironment = currentEnvironment;
        }

    }
}
