
namespace InventIT.Git
{
    partial class CommitDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_commitMessage = new System.Windows.Forms.TextBox();
            this.btn_CommitPush = new System.Windows.Forms.Button();
            this.btn_Commit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Commit Message";
            // 
            // txt_commitMessage
            // 
            this.txt_commitMessage.AcceptsReturn = true;
            this.txt_commitMessage.AcceptsTab = true;
            this.txt_commitMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_commitMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_commitMessage.Location = new System.Drawing.Point(13, 30);
            this.txt_commitMessage.Multiline = true;
            this.txt_commitMessage.Name = "txt_commitMessage";
            this.txt_commitMessage.Size = new System.Drawing.Size(643, 309);
            this.txt_commitMessage.TabIndex = 1;
            // 
            // btn_CommitPush
            // 
            this.btn_CommitPush.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CommitPush.Location = new System.Drawing.Point(542, 349);
            this.btn_CommitPush.Name = "btn_CommitPush";
            this.btn_CommitPush.Size = new System.Drawing.Size(114, 40);
            this.btn_CommitPush.TabIndex = 2;
            this.btn_CommitPush.Text = "Commit & Push";
            this.btn_CommitPush.UseMnemonic = false;
            this.btn_CommitPush.UseVisualStyleBackColor = false;
            // 
            // btn_Commit
            // 
            this.btn_Commit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Commit.Location = new System.Drawing.Point(461, 349);
            this.btn_Commit.Name = "btn_Commit";
            this.btn_Commit.Size = new System.Drawing.Size(75, 40);
            this.btn_Commit.TabIndex = 3;
            this.btn_Commit.Text = "Commit";
            this.btn_Commit.UseMnemonic = false;
            this.btn_Commit.UseVisualStyleBackColor = false;
            // 
            // CommitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 401);
            this.Controls.Add(this.btn_Commit);
            this.Controls.Add(this.btn_CommitPush);
            this.Controls.Add(this.txt_commitMessage);
            this.Controls.Add(this.label1);
            this.Name = "CommitDialog";
            this.Text = "Commit Dialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_commitMessage;
        private System.Windows.Forms.Button btn_CommitPush;
        private System.Windows.Forms.Button btn_Commit;
    }
}