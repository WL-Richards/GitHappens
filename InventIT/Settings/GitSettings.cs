using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventIT.Git;
using System.Diagnostics;

namespace InventIT.Settings
{
    public partial class GitSettings : Form
    {

        public GitSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On Form Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GitSettings_Load(object sender, EventArgs e)
        {
            txt_gitPath.Text = Properties.Settings.Default.gitPath;

            // Load saved values
            chk_lockFileOnSave.Checked = Properties.Settings.Default.lockOnSave;
            chk_LockOnOpen.Checked = Properties.Settings.Default.lockOnOpen;
            chk_UnlockOnPush.Checked = Properties.Settings.Default.unlockOnPush;

            txt_email.Text = Git.GitManager.getUserEmail();
        }

        /// <summary>
        /// Called when the button to browse to a new file is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_browseGitPath_Click(object sender, EventArgs e)
        {
            fd_gitExe.DefaultExt = "*.exe";
            if(fd_gitExe.ShowDialog() == DialogResult.OK)
            {
                txt_gitPath.Text = fd_gitExe.FileName;

                // Update Git Path
                Properties.Settings.Default.gitPath = fd_gitExe.FileName.Trim();
                Properties.Settings.Default.Save();

                // When the git executable is changed try to install lfs
                GitManager.installLFS();
            }
        }

        /// <summary>
        /// When the Test binary button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Test_Click(object sender, EventArgs e)
        {

            if (!Properties.Settings.Default.gitPath.Equals("Not Found")) {

                string output = GitManager.testGit();
                if (output.Contains("git version"))
                    MessageBox.Show("Git Successfully Setup.", "Success");
                else
                    MessageBox.Show("Failed to reach Git.", "Error");
                
            }
            else
            {
                MessageBox.Show("An error occurred when attempting to utilize Git. Please check the path to the executable", "Error");
            }
        }

        /// <summary>
        /// On Change of lock file on save set the value and then uncheck lockOnOpen as one can only be set at a time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_lockFileOnSave_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_lockFileOnSave.Checked)
            {
                chk_LockOnOpen.Checked = false;
                Properties.Settings.Default.lockOnOpen = chk_LockOnOpen.Checked;
            }
            Properties.Settings.Default.lockOnSave = chk_lockFileOnSave.Checked;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// On Change of lock file on open set the value and then uncheck lockOnSave as one can only be set at a time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_LockOnOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_LockOnOpen.Checked)
            {
                   chk_lockFileOnSave.Checked = false;
                Properties.Settings.Default.lockOnSave = chk_lockFileOnSave.Checked;
            }
            Properties.Settings.Default.lockOnOpen = chk_LockOnOpen.Checked;
            Properties.Settings.Default.Save();
        }

        // Set the value of unlock on push
        private void chk_UnlockOnPush_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.unlockOnPush = chk_UnlockOnPush.Checked;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Set the Git email globally to the email set there
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SetEmail_Click(object sender, EventArgs e)
        {
            GitManager.setUserEmail(txt_email.Text.Trim());
        }
    }
}
