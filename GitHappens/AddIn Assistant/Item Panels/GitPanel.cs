using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHappens.AddIn_Assistant.Item_Panels
{
    class GitPanel
    {
        // Buttons 
        private static ButtonDefinition btn_Commit;
        private static ButtonDefinition btn_Checkout;
        private static ButtonDefinition btn_Stage;
        private static RibbonPanel basicGitPanel;

        /// <summary>
        /// Create the basic Git control panel
        /// </summary>
        /// <param name="versionControlTab">Version control tab</param>
        public static void createBasicGitPanel(RibbonTab versionControlTab, bool inGitRepo)
        {
            // Add the basic git functionality
            basicGitPanel = versionControlTab.RibbonPanels.Add("Push & Pull", "Autodesk:VCS:Commit_Push", Guid.NewGuid().ToString());

            // Create the commit button and add a method for functionality
            btn_Commit = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Commit\nFile", 
                "Autodesk:VCS:Commit", 
                CommandTypesEnum.kFileOperationsCmdType, 
                Guid.NewGuid().ToString(), "", "", 
                IconManager.smallCommitPicture, 
                IconManager.largeCommitPicture);

            btn_Commit.OnExecute += onCommit;


            // Create the checkout button and create a method for functionality.
            btn_Checkout = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Pull\nChanges",
                "Autodesk:VCS:Checkout",
                CommandTypesEnum.kFileOperationsCmdType,
                Guid.NewGuid().ToString(), "", "",
                IconManager.smallCheckoutPicture,
                IconManager.largeCheckoutPicture);

            btn_Checkout.OnExecute += onCheckout;

            btn_Stage = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Stage\nCurrent File",
                "Autodesk:VCS:Stage",
                CommandTypesEnum.kFileOperationsCmdType,
                Guid.NewGuid().ToString(), "", "",
                IconManager.smallStagePicture,
                IconManager.largeStagePicture);

            btn_Stage.OnExecute += onStageFile;

            // If we are not in a Git repo then disable all Git controls
            if (!inGitRepo)
            {
                btn_Commit.Enabled = false;
                btn_Checkout.Enabled = false;
                btn_Stage.Enabled = false;
            }


            // Add all buttons to the panel
            basicGitPanel.CommandControls.AddButton(btn_Commit, true);
            basicGitPanel.CommandControls.AddButton(btn_Checkout, true);
            basicGitPanel.CommandControls.AddButton(btn_Stage, true);

        }

        /// <summary>
        /// Called when ever the user wants to stage changes to a file
        /// </summary>
        /// <param name="Context"></param>
        private static void onStageFile(NameValueMap Context)
        {
            Properties.Settings.Default.stagedFiles.Add(EnvironmentManager.getCurrrentDocument());
        }

        /// <summary>
        /// When the checkout / pull button is pressed
        /// </summary>
        /// <param name="Context">Caller/Handler info</param>
        private static void onCheckout(NameValueMap Context)
        {
            if (Git.GitManager.inGitRepo(EnvironmentManager.getCurrrentDocument()))
                Git.GitManager.updateFiles();
        }

        /// <summary>
        /// When the commit button is pressed
        /// </summary>
        /// <param name="Context">Caller/Handler info</param>
        private static void onCommit(NameValueMap Context)
        {
            if (Properties.Settings.Default.stagedFiles.Count <= 0)
                Properties.Settings.Default.stagedFiles.Add(EnvironmentManager.getCurrrentDocument());
            Git.GitManager.openCommitDialog();
        }
    
        /// <summary>
        /// Cleanup and remove all bindings for closure
        /// </summary>
        public static void Close()
        {
            if(basicGitPanel != null)
            {
                basicGitPanel.Delete();

                btn_Commit.Delete();
                btn_Commit.OnExecute -= onCommit;

                btn_Checkout.Delete();
                btn_Checkout.OnExecute -= onCheckout;

                btn_Stage.Delete();
                btn_Stage.OnExecute -= onStageFile;

            }
        }
    }
}
