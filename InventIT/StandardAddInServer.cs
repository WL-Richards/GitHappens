using Inventor;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InventIT
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

        // UI Management
        private UserInterfaceManager UIManger;
        private UserInterfaceEvents UIEventManger;

        // Currently loaded environment
        private string CurrentEnvironment = "";

        // Buttons 
        private ButtonDefinition btn_Commit;
        private ButtonDefinition btn_Checkout;

        private ButtonDefinition btn_LockFile;

        // Settings
        private ButtonDefinition btn_GitSettings;

        // Ribbon Mangers
        private RibbonTab tab_VersionControl;

        private RibbonPanel panel_FileManagement;
        private RibbonPanel panel_BasicGit;
        private RibbonPanel panel_Settings;

       

        /// <summary>
        /// AddInServer Constructor, Rarely used as the Activate method serves a similar purpose
        /// </summary>
        public StandardAddInServer()
        {

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

            if (firstTime)
            {
                // Create the Event Manger for the Add-in
                this.UIEventManger = m_inventorApplication.UserInterfaceManager.UserInterfaceEvents;

                // Create an event handler for when the environment changes (eg. Start Screen (ZeroDoc) -> Part File)
                this.UIEventManger.OnEnvironmentChange += UIEventManger_OnEnvironmentChange;

                // Get the applications UI manager
                this.UIManger = m_inventorApplication.UserInterfaceManager;
            }

        }

        /// <summary>
        /// Handles Changing of Environments in Inventor 
        /// </summary>
        /// <param name="Environment">New Environment</param>
        /// <param name="EnvironmentState">The Current State of the Environment (eg. has this been loaded prior)</param>
        /// <param name="BeforeOrAfter">Where in the environment transition process we are</param>
        /// <param name="Context">Context pertaining to the handler call</param>
        /// <param name="HandlingCode">Whether or not the handler was successfully handled</param>
        private void UIEventManger_OnEnvironmentChange(Inventor.Environment Environment, EnvironmentStateEnum EnvironmentState, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            // Check if the environment state changing is creating a new environment or resuming a previously opened one while also making sure the following is only run once per environment init
            if ((EnvironmentState == EnvironmentStateEnum.kActivateEnvironmentState || EnvironmentState == EnvironmentStateEnum.kResumeEnvironmentState) && CurrentEnvironment != Environment.DisplayName)
            {
                // TODO: Instead of recreating the entire tab each time, check to see if it has previously been created and re-use that instance. Currently it just kills everything
                cleanUpRibbons();

                // Set the CurrentEnvironment variable so this code is only run once per change
                CurrentEnvironment = Environment.DisplayName;

                // Convert the Environment name "Inventor" to ZeroDoc as that is its internal name
                if (CurrentEnvironment.Equals("Inventor"))
                    createUserInterface("ZeroDoc");

                // If unnecessary simply pass the environment name to UI creator.
                else
                {
                    createUserInterface(CurrentEnvironment);
                    MessageBox.Show(m_inventorApplication.ActiveDocument.File.FullFileName);
                }

               

                // Finally set the handling state of the handler
                HandlingCode = HandlingCodeEnum.kEventHandled;
            }
            else
            {
                // If it was not the type of environment change we wanted and thus did not handle it set the HandlingCode accordingly 
                HandlingCode = HandlingCodeEnum.kEventNotHandled;
            }

        }

       

        /// <summary>
        /// Create the Version Control Tab and sub panels
        /// </summary>
        /// <param name="environmentName">The name of the current environment so that we create the tab in the correct location</param>
        private void createUserInterface(String environmentName)
        {
            // Create and add the versionControl tab to the current environment
            tab_VersionControl = UIManger.Ribbons[environmentName].RibbonTabs.Add("Version Control", "Autodesk:VCS", Guid.NewGuid().ToString());

            // Create a push / pull / commit panel for basic git functionality
            panel_BasicGit = tab_VersionControl.RibbonPanels.Add("Push & Pull", "Autodesk:VCS:Commit_Push", Guid.NewGuid().ToString());
            panel_FileManagement = tab_VersionControl.RibbonPanels.Add("File Management", "Autodesk:VCS:Edit_Manager", Guid.NewGuid().ToString());
            panel_Settings = tab_VersionControl.RibbonPanels.Add("Git Settings", "Autodesk:VCS:Settings", Guid.NewGuid().ToString());

            #region Basic Git 
            // Create a commit file button
            btn_Commit = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("Commit\nFile", "Autodesk:VCS:Commit", CommandTypesEnum.kFileOperationsCmdType, Guid.NewGuid().ToString(), "", "", IconManager.smallCommitPicture, IconManager.largeCommitPicture);

            // Link that commit button to a handler
            btn_Commit.OnExecute += M_commitButton_OnExecute;


            // Create a new checkout / pull changes button
            btn_Checkout = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("Pull\nChanges", "Autodesk:VCS:Checkout", CommandTypesEnum.kFileOperationsCmdType, Guid.NewGuid().ToString(), "", "", IconManager.smallCheckoutPicture, IconManager.largeCheckoutPicture);

            // Link the pull button to a functional handler
            btn_Checkout.OnExecute += M_checkoutButton_OnExecute;

            // Finally add all the buttons to the corresponding panels with large icons enabled
            panel_BasicGit.CommandControls.AddButton(btn_Commit, true);
            panel_BasicGit.CommandControls.AddButton(btn_Checkout, true);

            #endregion

            #region File Management
            btn_LockFile = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("Manually Lock\nFile", "Autodesk:VCS:LockFile", CommandTypesEnum.kFilePropertyEditCmdType, Guid.NewGuid().ToString(), "", "", IconManager.smallLockFilePicture, IconManager.largeLockFilePicture);
            btn_LockFile.OnExecute += Btn_LockFile_OnExecute;

            panel_FileManagement.CommandControls.AddButton(btn_LockFile, true);
            #endregion

            #region Git Settings
            btn_GitSettings = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("Settings", "Autodesk:VCS:SettingsButton", CommandTypesEnum.kNonShapeEditCmdType, Guid.NewGuid().ToString(), "", "", IconManager.smallSettingsPicture, IconManager.largeSettingsPicture);
            btn_GitSettings.OnExecute += Btn_GitSettings_OnExecute;

            panel_Settings.CommandControls.AddButton(btn_GitSettings, true);
            #endregion
        }

        private void Btn_GitSettings_OnExecute(NameValueMap Context)
        {
            new Settings.GitSettings().Show();
        }

        private void Btn_LockFile_OnExecute(NameValueMap Context)
        {
            MessageBox.Show("Lock File");
        }

        /// <summary>
        /// When the checkout / pull button is pressed
        /// </summary>
        /// <param name="Context">Caller/Handler info</param>
        private void M_checkoutButton_OnExecute(NameValueMap Context)
        {
            MessageBox.Show("Checkout");
        }

        /// <summary>
        /// When the commit button is pressed
        /// </summary>
        /// <param name="Context">Caller/Handler info</param>
        private void M_commitButton_OnExecute(NameValueMap Context)
        {
            MessageBox.Show("Commit");
        }

        /// <summary>
        /// Called when trying to unload/close the add-in and thus cleanup must be done
        /// </summary>
        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Clean up non-null objects
            cleanUpRibbons();

            // Release objects.
            m_inventorApplication = null;


            // Run garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Handles cleanup of non-null objects
        /// </summary>
        private void cleanUpRibbons()
        {
            if (panel_BasicGit != null)
            {
                panel_BasicGit.Delete();

                btn_Commit.Delete();
                btn_Commit.OnExecute -= M_commitButton_OnExecute;

                btn_Checkout.Delete();
                btn_Checkout.OnExecute -= M_checkoutButton_OnExecute;
            }

            if(panel_FileManagement != null)
            {
                panel_FileManagement.Delete();

                btn_LockFile.Delete();
                btn_LockFile.OnExecute -= Btn_LockFile_OnExecute;
            }

            if(panel_Settings != null)
            {
                panel_Settings.Delete();

                btn_GitSettings.Delete();
                btn_GitSettings.OnExecute -= Btn_GitSettings_OnExecute;
            }

            if (tab_VersionControl != null)
                tab_VersionControl.Delete();

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
