using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHappens.Git
{
    public partial class CommitDialog : Form
    {
        public CommitDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the list of staged changes on load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommitDialog_Load(object sender, EventArgs e)
        {
            string repoRoot = "";
            
            // Add all staged changes to the staged stages box
            foreach(string item in Properties.Settings.Default.stagedFiles)
            {
                MessageBox.Show(item);
                if (GitManager.inGitRepo(item) && repoRoot.Length <= 0)
                {
                    repoRoot = GitManager.getRepoRoot().Replace("/", "\\"); ;
                }
                chkList_StagedList.Items.Add(item.Replace(repoRoot, ""), true);
            }
        }

        /// <summary>
        /// Commits the staged changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Commit_Click(object sender, EventArgs e)
        {
            foreach (int index in chkList_StagedList.CheckedIndices)
            {
                if (chkList_StagedList.GetItemCheckState(index) == CheckState.Checked)
                {
                    GitManager.stageFile(Properties.Settings.Default.stagedFiles[index]);
                }
            }

            if(GitManager.commitStaged(txt_commitMessage.Text).Contains("branch is up to date"))
            {
                MessageBox.Show("No Changes To Commit", "Information");
            }
            else
            {
                MessageBox.Show("Successfully Committed Changes", "Success");
            }

            // Clear all staged changes after commit
            Properties.Settings.Default.stagedFiles.Clear();
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btn_CommitPush_Click(object sender, EventArgs e)
        {
            // Loop through the items indices in the staged list
            foreach (int index in chkList_StagedList.CheckedIndices)
            {
                // Check if it is staged for commit
                if (chkList_StagedList.GetItemCheckState(index) == CheckState.Checked)
                {
                    GitManager.stageFile(Properties.Settings.Default.stagedFiles[index]);
                }
            }

            // Check if we need to commit anything
            if (GitManager.commitStaged(txt_commitMessage.Text).Contains("branch is up to date"))
            {
                MessageBox.Show("No Changes To Commit", "Information");
            }
            else
            {
                string pushResponse = GitManager.pushFiles();
                MessageBox.Show(pushResponse, pushResponse.Contains("Failed to push") ? "Error" : "Information");
            }

            // Clear all staged changes after commit
            Properties.Settings.Default.stagedFiles.Clear();
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
