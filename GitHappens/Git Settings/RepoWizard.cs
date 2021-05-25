using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GitHappens.Git_Settings
{
    public partial class RepoWizard : Form
    {
        public RepoWizard()
        {
            InitializeComponent();
        }

        private void btn_PickFolder_Click(object sender, EventArgs e)
        {
            // If a folder is selected check if the folder is empty
            if(fb_selectFolder.ShowDialog() == DialogResult.OK)
            {
                // Check if there are already files in the selected folder
                if(Directory.GetFiles(fb_selectFolder.SelectedPath).Length > 0)
                {
                    // If there were inform the user that the folder was not empty and then reopen the dialog
                    MessageBox.Show("Selected folder was not empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    btn_PickFolder_Click(null, null);
                }

                // If the folder was empty set the selected path to the text box
                txt_folderPath.Text = fb_selectFolder.SelectedPath;
            }
        }

        private void btn_createRepo_Click(object sender, EventArgs e)
        {
            if (txt_folderPath.Text.Length > 0 && txt_repoLink.Text.Length > 0) {
                if (!Git.GitManager.createGitRepo(txt_folderPath.Text, txt_repoLink.Text, chk_isGitHub.Checked))
                    MessageBox.Show("An error occurred when creating the repository. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                else
                {
                    MessageBox.Show("Successfully created repository", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Some fields were missing required information. Please correct and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            
        }
    }
}
