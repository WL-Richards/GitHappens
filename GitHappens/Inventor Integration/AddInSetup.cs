using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHappens.Inventor_Integration
{
    /// <summary>
    /// Handles mostly misc. functions that could easily be moved to their own file to improve readability.
    /// </summary>
    class AddInSetup
    {
        private static UserInterfaceManager UIManger;
        private static UserInterfaceEvents UIEventManger;
        private static ApplicationEvents ApplicationEventsManager;

        /// <summary>
        /// Runs the first time Git checkup and required setup when the application is loaded by Inventor
        /// </summary>
        public static void setupGit()
        {
            // Check if Git was not found in the default location
            if (Git.GitManager.setupGit().Equals("Not Found"))
            {
                // Prompted the user to set the correct Git path.
                if (MessageBox.Show("Git Path Not Set, Would you like to do so now?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    new Settings.GitSettings().ShowDialog();
                }
            }

            // If it was found then go ahead and install LFS to handle the large files
            else
            {
                Git.GitManager.installLFS();
            }

            // If the users email is not set take them to the settings to set their email
            if (Git.GitManager.getUserEmail().Trim().Length <= 0)
            {
                // Check if the Git binary is Not and Prompted the user to set it.
                if (MessageBox.Show("Git Email Not Set (Required to lock files), Would you like to do so now?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    new Settings.GitSettings().ShowDialog();
                }
            }
        }

        /// <summary>
        /// Preforms the first time initialization of all UI elements and mangers
        /// </summary>
        public static void setupUIManagers()
        {
            // Get a reference to the inventor application object
            Inventor.Application application = ApplicationManager.getInventorApplication();

            // Create the Event Manger for the Add-in
            UIEventManger = application.UserInterfaceManager.UserInterfaceEvents;

            // Create an event handler for when the environment changes (eg. Start Screen (ZeroDoc) -> Part File)
            UIEventManger.OnEnvironmentChange += EnvironmentManager.onEnvironmentChange;
            ApplicationEventsManager = application.ApplicationEvents;
            ApplicationEventsManager.OnOpenDocument += ApplicationManager.onDocumentOpen;
            ApplicationEventsManager.OnSaveDocument += ApplicationManager.onDocumentSave;
            ApplicationEventsManager.OnActivateDocument += ApplicationManager.onChangeDocument;
            
           

            // Get the applications UI manager
            UIManger = application.UserInterfaceManager;
        }


        public static UserInterfaceManager getUIManager()
        {
            return UIManger;
        }
    }
}
