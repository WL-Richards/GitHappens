using Inventor;
using GitHappens.Inventor_Integration;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace GitHappens
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("fecf1836-db82-4457-bf2f-5298f719cba9")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        private Inventor.Application m_inventorApplication;



        /// <summary>
        /// AddInServer Constructor, Rarely used as the Activate method serves a similar purpose
        /// </summary>
        public StandardAddInServer()
        {
            // Run the addin Git setup
            AddInSetup.setupGit();
        }

        /// <summary>
        /// Called when inventor is launched and the add-in is loaded
        /// </summary>
        /// <param name="addInSiteObject">Reference to the Add-in application which contains all API functionality</param>
        /// <param name="firstTime">Has the add-in been activated before</param>
        public void Activate(ApplicationAddInSite addInSiteObject, bool firstTime)
        {

            // Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;

            // Set the current inventor application object to
            ApplicationManager.setInventorApplication(m_inventorApplication);

            if (firstTime)
            {
                // Setup UI
                AddInSetup.setupUIManagers();
            }

        }

        /// <summary>
        /// Called when trying to unload/close the add-in and thus cleanup must be done
        /// </summary>
        public void Deactivate()
        {
            // Clean up non-null objects
            Inventor_Integration.EnvironmentManager.cleanUpUI();

            // Release objects.
            m_inventorApplication = null;
            ApplicationManager.setInventorApplication(m_inventorApplication);

            // Run garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        

        /// <summary>
        /// Unused / Obsolete, However required by the Application
        /// </summary>
        /// <param name="commandID"></param>
        public void ExecuteCommand(int commandID) { }

        /// <summary>
        /// Allows exposing of AddIn API to other programs
        /// </summary>
        public object Automation { get { return null; } }

    }
}
