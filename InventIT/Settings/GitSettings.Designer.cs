
namespace InventIT.Settings
{
    partial class GitSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_gitPath = new System.Windows.Forms.Label();
            this.txt_gitPath = new System.Windows.Forms.TextBox();
            this.btn_browseGitPath = new System.Windows.Forms.Button();
            this.fd_gitExe = new System.Windows.Forms.OpenFileDialog();
            this.chk_LockOnOpen = new System.Windows.Forms.CheckBox();
            this.chk_UnlockOnPush = new System.Windows.Forms.CheckBox();
            this.btn_Test = new System.Windows.Forms.Button();
            this.chk_lockFileOnSave = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lb_gitPath
            // 
            this.lb_gitPath.AutoSize = true;
            this.lb_gitPath.Location = new System.Drawing.Point(13, 18);
            this.lb_gitPath.Name = "lb_gitPath";
            this.lb_gitPath.Size = new System.Drawing.Size(45, 13);
            this.lb_gitPath.TabIndex = 0;
            this.lb_gitPath.Text = "Git Path";
            // 
            // txt_gitPath
            // 
            this.txt_gitPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_gitPath.Location = new System.Drawing.Point(64, 14);
            this.txt_gitPath.Name = "txt_gitPath";
            this.txt_gitPath.ReadOnly = true;
            this.txt_gitPath.Size = new System.Drawing.Size(305, 20);
            this.txt_gitPath.TabIndex = 1;
            // 
            // btn_browseGitPath
            // 
            this.btn_browseGitPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_browseGitPath.Location = new System.Drawing.Point(375, 12);
            this.btn_browseGitPath.Name = "btn_browseGitPath";
            this.btn_browseGitPath.Size = new System.Drawing.Size(75, 23);
            this.btn_browseGitPath.TabIndex = 2;
            this.btn_browseGitPath.Text = "Browse";
            this.btn_browseGitPath.UseVisualStyleBackColor = true;
            this.btn_browseGitPath.Click += new System.EventHandler(this.btn_browseGitPath_Click);
            // 
            // fd_gitExe
            // 
            this.fd_gitExe.FileName = "openFileDialog1";
            this.fd_gitExe.Title = "Path to Git Executable";
            // 
            // chk_LockOnOpen
            // 
            this.chk_LockOnOpen.AutoSize = true;
            this.chk_LockOnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_LockOnOpen.Location = new System.Drawing.Point(16, 62);
            this.chk_LockOnOpen.Name = "chk_LockOnOpen";
            this.chk_LockOnOpen.Size = new System.Drawing.Size(159, 24);
            this.chk_LockOnOpen.TabIndex = 4;
            this.chk_LockOnOpen.Text = "Lock File On Open";
            this.chk_LockOnOpen.UseVisualStyleBackColor = true;
            this.chk_LockOnOpen.CheckedChanged += new System.EventHandler(this.chk_LockOnOpen_CheckedChanged);
            // 
            // chk_UnlockOnPush
            // 
            this.chk_UnlockOnPush.AutoSize = true;
            this.chk_UnlockOnPush.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_UnlockOnPush.Location = new System.Drawing.Point(16, 115);
            this.chk_UnlockOnPush.Name = "chk_UnlockOnPush";
            this.chk_UnlockOnPush.Size = new System.Drawing.Size(171, 24);
            this.chk_UnlockOnPush.TabIndex = 5;
            this.chk_UnlockOnPush.Text = "Unlock File On Push";
            this.chk_UnlockOnPush.UseVisualStyleBackColor = true;
            this.chk_UnlockOnPush.CheckedChanged += new System.EventHandler(this.chk_UnlockOnPush_CheckedChanged);
            // 
            // btn_Test
            // 
            this.btn_Test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Test.Location = new System.Drawing.Point(468, 11);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(75, 23);
            this.btn_Test.TabIndex = 6;
            this.btn_Test.Text = "Test";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // chk_lockFileOnSave
            // 
            this.chk_lockFileOnSave.AutoSize = true;
            this.chk_lockFileOnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_lockFileOnSave.Location = new System.Drawing.Point(291, 62);
            this.chk_lockFileOnSave.Name = "chk_lockFileOnSave";
            this.chk_lockFileOnSave.Size = new System.Drawing.Size(159, 24);
            this.chk_lockFileOnSave.TabIndex = 7;
            this.chk_lockFileOnSave.Text = "Lock File On Open";
            this.chk_lockFileOnSave.UseVisualStyleBackColor = true;
            this.chk_lockFileOnSave.CheckedChanged += new System.EventHandler(this.chk_lockFileOnSave_CheckedChanged);
            // 
            // GitSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 151);
            this.Controls.Add(this.chk_lockFileOnSave);
            this.Controls.Add(this.btn_Test);
            this.Controls.Add(this.chk_UnlockOnPush);
            this.Controls.Add(this.chk_LockOnOpen);
            this.Controls.Add(this.btn_browseGitPath);
            this.Controls.Add(this.txt_gitPath);
            this.Controls.Add(this.lb_gitPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GitSettings";
            this.Text = "Git Settings";
            this.Load += new System.EventHandler(this.GitSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_gitPath;
        private System.Windows.Forms.TextBox txt_gitPath;
        private System.Windows.Forms.Button btn_browseGitPath;
        private System.Windows.Forms.OpenFileDialog fd_gitExe;
        private System.Windows.Forms.CheckBox chk_LockOnOpen;
        private System.Windows.Forms.CheckBox chk_UnlockOnPush;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.CheckBox chk_lockFileOnSave;
    }
}