using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace InventIT.Git
{
    public class GitManager
    {
        /// <summary>
        /// Check to see if the git executable exists at the default install location
        /// </summary>
        /// <returns></returns>
        public static string gitExistsAtDefault()
        {
            if (File.Exists(@"C:\Program Files\Git\bin\git.exe"))
                return @"C:\Program Files\Git\bin\git.exe";
            else if (File.Exists(@"C:\Program Files (x86)\Git\bin\git.exe"))
                return @"C:\Program Files (x86)\Git\bin\git.exe";
            else
                return "Not Found";
        }

        /// <summary>
        /// Test the current path to the Git binary to verify it is working.
        /// </summary>
        /// <param name="gitPath">Path to the Git binary.</param>
        /// <returns>Process exit code</returns>
        public static string testGit()
        {
            return runGitCommand("--verison");
        }

        /// <summary>
        /// On call start try to setup Git if it has not previously been set, return the status of the Git binary location
        /// </summary>
        public static string setupGit()
        {
            string gitExists = GitManager.gitExistsAtDefault();

            // If there was a
            if (Properties.Settings.Default.gitPath.Length <= 0)
            {
                Properties.Settings.Default.gitPath = gitExists;
                
            }
            else
            {
                gitExists = Properties.Settings.Default.gitPath;
            }

            Properties.Settings.Default.Save();
            // Return the status of the Git binary
            return gitExists;
        }

        /// <summary>
        /// Checks whether or not a file is currently in a Git repository
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Status of tracking</returns>
        public static bool inGitRepo(string filePath)
        {
            // Change the current project directory to the
            Directory.SetCurrentDirectory(Directory.GetParent(filePath).FullName);
            if (!runGitCommand("status").Contains("not a git repository"))
                return true;
            return false;
        }

        /// <summary>
        /// Easy to use point of interaction with Git, simply pass the command to run
        /// </summary>
        /// <param name="command">Command to be run by git</param>
        /// <returns>The error if it exists, if then the standard output of the command </returns>
        private static string runGitCommand(string command)
        {
            if (File.Exists(Properties.Settings.Default.gitPath))
            {
                Process gitProc = new Process();
                gitProc.StartInfo.FileName = Properties.Settings.Default.gitPath.Trim();
                gitProc.StartInfo.Arguments = command;
                gitProc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                gitProc.StartInfo.UseShellExecute = false;
                gitProc.StartInfo.CreateNoWindow = true;
                gitProc.StartInfo.RedirectStandardOutput = true;
                gitProc.StartInfo.RedirectStandardError = true;
                gitProc.Start();
                gitProc.WaitForExit();

                // Checking standard error / output
                string error = gitProc.StandardError.ReadToEnd();
                if (error.Length > 0)
                    return error;
                else
                    return gitProc.StandardOutput.ReadToEnd();
            }

            else
            {
                MessageBox.Show("Git was not present at the given file path (File Missing)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Git Not Present";

            }
        }

        /// <summary>
        /// Install Git LFS into the users local Git instance
        /// </summary>
        /// <param name="gitPath">Path to the Git executable</param>
        /// <returns>Exit status of the process</returns>
        public static string installLFS()
        {
            return runGitCommand("lfs install");
        }
    }
}
