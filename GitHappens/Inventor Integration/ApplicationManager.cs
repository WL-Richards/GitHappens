using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GitHappens.Inventor_Integration
{
    /// <summary>
    /// Manages Application events
    /// </summary>
    class ApplicationManager
    {

        private static Inventor.Application InventorApplication;

        /// <summary>
        /// Called on Document Save
        /// </summary>
        /// <param name="DocumentObject">Object representative of the document</param>
        /// <param name="BeforeOrAfter">The current timing of the event</param>
        /// <param name="Context">The current context of the event</param>
        /// <param name="HandlingCode">How the event was handled</param>
        public static void onDocumentSave(_Document DocumentObject, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            // Check before we save if the file has been saved before
            bool firstTimeSave = false;
            if (BeforeOrAfter == EventTimingEnum.kBefore)
            {
                // If the file exists or not determines if this is a first time save
                firstTimeSave = !System.IO.File.Exists(DocumentObject.File.FullFileName);
            }
                
            // Check if our current lock status allows us to edit the file
            if (Git.LockFileManager.canEditFile(EnvironmentManager.getCurrrentDocument()))
            {
                // If it is a first time save don't lock the file as it doesn't exist for anyone else
                if (Properties.Settings.Default.lockOnSave && !firstTimeSave)
                    Git.LockFileManager.lockFile(EnvironmentManager.getCurrrentDocument(), true);
                HandlingCode = HandlingCodeEnum.kEventHandled;
            }
            else
            {
                // Before saving check to see if we can actually save this document if no inform the user that the current document is locked
                if (BeforeOrAfter == EventTimingEnum.kBefore)
                {
                    MessageBox.Show("You Cannot Save this file as it is currently locked by another user", "Error");
                }
                HandlingCode = HandlingCodeEnum.kEventCanceled;
            }
        }

        /// <summary>
        /// Called when a new Document is opened
        /// </summary>
        /// <param name="DocumentObject"></param>
        /// <param name="FullDocumentName">Full Document Path</param>
        /// <param name="BeforeOrAfter"></param>
        /// <param name="Context"></param>
        /// <param name="HandlingCode"></param>
        public static void onDocumentOpen(_Document DocumentObject, string FullDocumentName, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            
            if (BeforeOrAfter == EventTimingEnum.kAfter)
            {
                if (Git.GitManager.inGitRepo(EnvironmentManager.getCurrrentDocument()))
                {
                    if (Git.LockFileManager.isFileLocked(EnvironmentManager.getCurrrentDocument()) &&
                        !Git.LockFileManager.canEditFile(EnvironmentManager.getCurrrentDocument()))
                    {

                        // If the file is locked inform them that no changes will be saved to this file if they say the dont want to continue close the object
                        if (MessageBox.Show("This file is currently locked by another user. Any changes made to this WILL NOT BE SAVED! Do you want to Proceed", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            DocumentObject.Close();
                        }
                    }
                }

                // Clear and create a new UI for the different environment
                EnvironmentManager.createUserInterface(AddInSetup.getUIManager());
            }
            else if (BeforeOrAfter == EventTimingEnum.kBefore)
            {
                // Redundant in the local scope, useful in the global
                EnvironmentManager.setCurrrentDocument(FullDocumentName);
                if (Git.GitManager.inGitRepo(EnvironmentManager.getCurrrentDocument()))
                {
                    if (Properties.Settings.Default.lockOnOpen)
                    {
                        Git.LockFileManager.lockFile(EnvironmentManager.getCurrrentDocument(), true);
                    }

                    // Update the lock file
                    Git.GitManager.updateLockFile();
                }

            }
            HandlingCode = HandlingCodeEnum.kEventHandled;
        }
    
        /// <summary>
        /// Get the current inventor application object
        /// </summary>
        /// <returns></returns>
        public static Inventor.Application getInventorApplication()
        {
            return InventorApplication;
        }

        /// <summary>
        /// Set the current inventor application object
        /// </summary>
        /// <param name="inventorApplication">New inventor application object</param>
        public static void setInventorApplication(Inventor.Application inventorApplication)
        {
            InventorApplication = inventorApplication;   
        }
    }
}
