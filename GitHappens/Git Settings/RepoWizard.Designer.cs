
namespace GitHappens.Git_Settings
{
    partial class RepoWizard
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
            this.components = new System.ComponentModel.Container();
            this.txt_folderPath = new System.Windows.Forms.TextBox();
            this.lb_repoFolder = new System.Windows.Forms.Label();
            this.btn_PickFolder = new System.Windows.Forms.Button();
            this.fb_selectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_createRepo = new System.Windows.Forms.Button();
            this.txt_repoLink = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tt_BoxInformation = new System.Windows.Forms.ToolTip(this.components);
            this.chk_isGitHub = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txt_folderPath
            // 
            this.txt_folderPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_folderPath.Location = new System.Drawing.Point(12, 29);
            this.txt_folderPath.Name = "txt_folderPath";
            this.txt_folderPath.ReadOnly = true;
            this.txt_folderPath.Size = new System.Drawing.Size(276, 22);
            this.txt_folderPath.TabIndex = 0;
            this.tt_BoxInformation.SetToolTip(this.txt_folderPath, "Path to empty folder where the Git Repository should\r\nbe created.");
            // 
            // lb_repoFolder
            // 
            this.lb_repoFolder.AutoSize = true;
            this.lb_repoFolder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_repoFolder.Location = new System.Drawing.Point(9, 9);
            this.lb_repoFolder.Name = "lb_repoFolder";
            this.lb_repoFolder.Size = new System.Drawing.Size(96, 13);
            this.lb_repoFolder.TabIndex = 1;
            this.lb_repoFolder.Text = "New Repo Folder";
            // 
            // btn_PickFolder
            // 
            this.btn_PickFolder.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PickFolder.Location = new System.Drawing.Point(294, 28);
            this.btn_PickFolder.Name = "btn_PickFolder";
            this.btn_PickFolder.Size = new System.Drawing.Size(75, 23);
            this.btn_PickFolder.TabIndex = 2;
            this.btn_PickFolder.Text = "Browse";
            this.btn_PickFolder.UseVisualStyleBackColor = true;
            this.btn_PickFolder.Click += new System.EventHandler(this.btn_PickFolder_Click);
            // 
            // fb_selectFolder
            // 
            this.fb_selectFolder.Description = "Select an EMPTY folder to create the repository";
            // 
            // btn_createRepo
            // 
            this.btn_createRepo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_createRepo.Location = new System.Drawing.Point(12, 147);
            this.btn_createRepo.Name = "btn_createRepo";
            this.btn_createRepo.Size = new System.Drawing.Size(357, 49);
            this.btn_createRepo.TabIndex = 3;
            this.btn_createRepo.Text = "Create Repository";
            this.btn_createRepo.UseVisualStyleBackColor = true;
            this.btn_createRepo.Click += new System.EventHandler(this.btn_createRepo_Click);
            // 
            // txt_repoLink
            // 
            this.txt_repoLink.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_repoLink.Location = new System.Drawing.Point(12, 82);
            this.txt_repoLink.Name = "txt_repoLink";
            this.txt_repoLink.Size = new System.Drawing.Size(357, 22);
            this.txt_repoLink.TabIndex = 4;
            this.tt_BoxInformation.SetToolTip(this.txt_repoLink, "Link to the repository on the internet.\r\nThis is were changes will be pushed/pull" +
        "ed from.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = " Repository Link";
            // 
            // chk_isGitHub
            // 
            this.chk_isGitHub.AutoSize = true;
            this.chk_isGitHub.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_isGitHub.Location = new System.Drawing.Point(12, 114);
            this.chk_isGitHub.Name = "chk_isGitHub";
            this.chk_isGitHub.Size = new System.Drawing.Size(116, 24);
            this.chk_isGitHub.TabIndex = 6;
            this.chk_isGitHub.Text = "Using GitHub";
            this.chk_isGitHub.UseVisualStyleBackColor = true;
            // 
            // RepoWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 208);
            this.Controls.Add(this.chk_isGitHub);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_repoLink);
            this.Controls.Add(this.btn_createRepo);
            this.Controls.Add(this.btn_PickFolder);
            this.Controls.Add(this.lb_repoFolder);
            this.Controls.Add(this.txt_folderPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RepoWizard";
            this.Text = "Create New CAD Repo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_folderPath;
        private System.Windows.Forms.Label lb_repoFolder;
        private System.Windows.Forms.Button btn_PickFolder;
        private System.Windows.Forms.FolderBrowserDialog fb_selectFolder;
        private System.Windows.Forms.Button btn_createRepo;
        private System.Windows.Forms.ToolTip tt_BoxInformation;
        private System.Windows.Forms.TextBox txt_repoLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_isGitHub;
    }
}