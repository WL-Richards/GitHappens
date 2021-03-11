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
            if(Properties.Settings.Default.gitPath.Length <= 0)
            {
                txt_gitPath.Text = GitManager.gitExistsAtDefault();

                // If git is stored at the default location set that as the gitPath
                if(!txt_gitPath.Text.Equals("Not Found"))
                {
                    Properties.Settings.Default.gitPath = txt_gitPath.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                // Load saved path from Properties
                txt_gitPath.Text = Properties.Settings.Default.gitPath;
            }


            // Load saved values
            chk_lockFileOnSave.Checked = Properties.Settings.Default.lockOnSave;
            chk_LockOnOpen.Checked = Properties.Settings.Default.lockOnOpen;
            chk_UnlockOnPush.Checked = Properties.Settings.Default.unlockOnPush;
        }

        private void btn_browseGitPath_Click(object sender, EventArgs e)
        {
            fd_gitExe.DefaultExt = "*.exe";
            if(fd_gitExe.ShowDialog() == DialogResult.OK)
            {
                txt_gitPath.Text = fd_gitExe.FileName;
                btn_Test_Click(this, e);

                // Update Git Path
                Properties.Settings.Default.gitPath = txt_gitPath.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            if (!txt_gitPath.Text.Equals("Not Found")) {
                Process gitProc = new Process();
                gitProc.StartInfo.FileName = txt_gitPath.Text.Trim();
                gitProc.StartInfo.Arguments = String.Format("--version && {0} lfs install", txt_gitPath.Text.Trim());
                gitProc.StartInfo.UseShellExecute = false;
                gitProc.StartInfo.CreateNoWindow = true;
                gitProc.StartInfo.RedirectStandardOutput = true;
                gitProc.Start();
                gitProc.WaitForExit();
                string output = gitProc.StandardOutput.ReadToEnd();
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

        private void chk_lockFileOnSave_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_lockFileOnSave.Checked)
            {
                chk_LockOnOpen.Checked = false;
                Properties.Settings.Default.lockOnOpen = chk_LockOnOpen.Checked;
            }
            Properties.Settings.Default.lockOnSave = chk_lockFileOnSave.Checked;
            Properties.Settings.Default.Save();
            MessageBox.Show(Properties.Settings.Default.lockOnOpen.ToString() + " " + Properties.Settings.Default.lockOnSave.ToString());
        }

        private void chk_LockOnOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_LockOnOpen.Checked)
            {
                   chk_lockFileOnSave.Checked = false;
                Properties.Settings.Default.lockOnSave = chk_lockFileOnSave.Checked;
            }
            Properties.Settings.Default.lockOnOpen = chk_LockOnOpen.Checked;
            Properties.Settings.Default.Save();
            MessageBox.Show(Properties.Settings.Default.lockOnOpen.ToString() + " " + Properties.Settings.Default.lockOnSave.ToString());
        }

        private void chk_UnlockOnPush_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.unlockOnPush = chk_UnlockOnPush.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
