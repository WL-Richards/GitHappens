using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHappens.Inventor_Integration.Item_Panels
{
    class FileManagementPanel
    {
        private ButtonDefinition btn_LockFile;
        private RibbonPanel fileManagementPanel;

        /// <summary>
        /// Create the file management panel (eg. lock files, etc.)
        /// </summary>
        /// <param name="versionControlTab">Parent version control tab</param>
        public void createFileManagementPanel(RibbonTab versionControlTab,string envName)
        {
            fileManagementPanel = versionControlTab.RibbonPanels.Add("File Management", "Autodesk:VCS:Edit_Manager", Guid.NewGuid().ToString());

            // Create the button to lock files
            btn_LockFile = ApplicationManager.getInventorApplication().CommandManager.ControlDefinitions.AddButtonDefinition("Toggle File\nLock", 
                string.Format("Autodesk:VCS:LockFile:{0}", envName), 
                CommandTypesEnum.kFilePropertyEditCmdType, 
                Guid.NewGuid().ToString(), "", "", 
                IconManager.smallLockFilePicture, 
                IconManager.largeLockFilePicture);

            btn_LockFile.OnExecute += onLockFile;

            // Add the lock file option to the UI
            fileManagementPanel.CommandControls.AddButton(btn_LockFile, true);
        }

        public void disableButtons()
        {
            btn_LockFile.Enabled = false;
        }

        public void enableButtons()
        {
            btn_LockFile.Enabled = true;
        }

        /// <summary>
        /// Manually Toggle the file lock
        /// </summary>
        /// <param name="Context"></param>
        private void onLockFile(NameValueMap Context)
        {
            if (Git.LockFileManager.isFileLocked(EnvironmentManager.getCurrentDocument()))
            {
                Git.LockFileManager.unlockFile(EnvironmentManager.getCurrentDocument(), false);
            }
            else
            {
                Git.LockFileManager.lockFile(EnvironmentManager.getCurrentDocument(), false);
            }
        }

        /// <summary>
        /// Cleanup and remove all bindings for closure
        /// </summary>
        public void Close()
        {
            if(fileManagementPanel != null)
            {
                fileManagementPanel.Delete();

                btn_LockFile.Delete();
                btn_LockFile.OnExecute -= onLockFile;
            }
        }
    }
}
