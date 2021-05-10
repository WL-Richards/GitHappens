using GitHappens.Inventor_Integration.Item_Panels;
using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHappens.Inventor_Integration
{

    /// <summary>
    /// Used to manage Ribbon tabs across different environments without regenerating them each time
    /// </summary>
    class UIManager
    {
        private static Dictionary<string, RibbonTabContainer> environmentRibbons = new Dictionary<string, RibbonTabContainer>();

        /// <summary>
        /// Whether or not the UI for the current environment has already been created
        /// </summary>
        /// <returns></returns>
        private static bool previouslyCreatedUI(string currentEnvironment)
        {
            return (environmentRibbons.ContainsKey(currentEnvironment));
        }

        /// <summary>
        /// Create the UI be that restore a previously generated one or create a new instance
        /// </summary>
        /// <param name="uiManger"></param>
        public static void createUI(UserInterfaceManager uiManger, bool inGitRepo)
        {
            // If this is our first time creating the UI
            if (!previouslyCreatedUI(EnvironmentManager.getCurrentEnvironment()))
            {
                // Create a new container for the RibbonTab
                environmentRibbons.Add(EnvironmentManager.getCurrentEnvironment(), new RibbonTabContainer(EnvironmentManager.getCurrentEnvironment(), uiManger));

            }
            MessageBox.Show(environmentRibbons.Count.ToString());
        }
        /// <summary>
        /// Cleanup all RibbonTabs
        /// </summary>
        public static void Close()
        {
            foreach(KeyValuePair<string, RibbonTabContainer> entry in environmentRibbons)
            {
                entry.Value.Close();
            }
        }

        /// <summary>
        /// Disable functionality that is only available when in a Git repo
        /// </summary>
        public static void disableRepoFunctionality()
        {

        }
    }
}
