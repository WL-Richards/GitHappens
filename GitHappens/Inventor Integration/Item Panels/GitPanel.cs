using GitHappens.Git;
using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHappens.Inventor_Integration.Item_Panels
{
    class GitPanel
    {
        // Buttons 
        private ButtonDefinition btn_Commit;
        private ButtonDefinition btn_Checkout;
        private ButtonDefinition btn_Stage;
        private RibbonPanel basicGitPanel;

        /// <summary>
        /// Create the basic Git control panel
        /// </summary>
        /// <param name="versionControlTab">Version control tab</param>
        public void createBasicGitPanel(RibbonTab versionControlTab, bool inGitRepo, string envName)
        {
            // Add the basic git functionality
            basicGitPanel = versionControlTab.RibbonPanels.Add("Push & Pull", "Autodesk:VCS:Commit_Push", Guid.NewGuid().ToString());

            // Create the commit button and add a method for functionality
            btn_Commit = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Commit\nFile", 
                string.Format("Autodesk:VCS:Commit:{0}", envName), 
                CommandTypesEnum.kFileOperationsCmdType, 
                Guid.NewGuid().ToString(), "", "", 
                IconManager.smallCommitPicture, 
                IconManager.largeCommitPicture);

            btn_Commit.OnExecute += onCommit;


            // Create the checkout button and create a method for functionality.
            btn_Checkout = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Pull\nChanges",
                string.Format("Autodesk:VCS:Checkout{0}", envName),
                CommandTypesEnum.kFileOperationsCmdType,
                Guid.NewGuid().ToString(), "", "",
                IconManager.smallCheckoutPicture,
                IconManager.largeCheckoutPicture);

            btn_Checkout.OnExecute += onCheckout;

            btn_Stage = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Stage\nCurrent File",
                string.Format("Autodesk:VCS:Stage{0}", envName),
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
        private void onStageFile(NameValueMap Context)
        {
            if (GitManager.canCommit(EnvironmentManager.getCurrrentDocument()))
                Properties.Settings.Default.stagedFiles.Add(EnvironmentManager.getCurrrentDocument());
            else
                MessageBox.Show("The selected file has no pending change and will not be committed", "Information");
        }

        /// <summary>
        /// When the checkout / pull button is pressed
        /// </summary>
        /// <param name="Context">Caller/Handler info</param>
        private void onCheckout(NameValueMap Context)
        {
            if (Git.GitManager.inGitRepo(EnvironmentManager.getCurrrentDocument()))
                Git.GitManager.updateFiles();
        }

        /// <summary>
        /// When the commit button is pressed
        /// </summary>
        /// <param name="Context">Caller/Handler info</param>
        private void onCommit(NameValueMap Context)
        {
            if (GitManager.canCommit(EnvironmentManager.getCurrrentDocument()))
            {
                if (Properties.Settings.Default.stagedFiles.Count <= 0)
                    Properties.Settings.Default.stagedFiles.Add(EnvironmentManager.getCurrrentDocument());
                GitManager.openCommitDialog();
            }
            else
                MessageBox.Show("The selected file has no pending change and will not be committed", "Information");
        }
    
        /// <summary>
        /// Cleanup and remove all bindings for closure
        /// </summary>
        public void Close()
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
