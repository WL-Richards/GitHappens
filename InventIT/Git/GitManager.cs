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
            return runGitCommand("--verison").Trim();
        }

        /// <summary>
        /// Get the users set Git email
        /// </summary>
        /// <returns></returns>
        public static string getUserEmail()
        {
            return runGitCommand("config user.email").Trim();
        }

        public static string setUserEmail(string email)
        {
            return runGitCommand(String.Format("config --global user.email {0}", email)).Trim();
        }

        /// <summary>
        /// Get the top level of the current Git repo
        /// </summary>
        /// <returns></returns>
        public static string getRepoRoot()
        {
           return runGitCommand("rev-parse --show-toplevel").Trim();
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
        /// Open the Commit / Push dialog
        /// </summary>
        public static void openCommitDialog()
        {
            new CommitDialog().Show();
        }



        /// <summary>
        /// Push content to the cloud
        /// </summary>
        /// <returns></returns>
        public static string pushFiles()
        {
            return "";
        }

        /// <summary>
        /// Create a local commit
        /// </summary>
        /// <param name="message">Message to describe the commit</param>
        /// <returns>Result of the Git proc.</returns>
        public static string commitFile(string message)
        {
            return "";
        }

        /// <summary>
        /// Commit and Push a single file (eg. the lock file)
        /// </summary>
        /// <param name="message">Message to use with the single file push</param>
        /// <returns></returns>
        public static string pushLockFile(string message)
        {
            return "";
        }

        /// <summary>
        /// Pull down any new changes to the lock file
        /// </summary>
        /// <returns>Result of the Git Proc</returns>
        public static string updateLockFile()
        {
            return "";
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
            return runGitCommand("lfs install").Trim();
        }
    }
}
