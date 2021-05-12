using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using GitHappens.Inventor_Integration;

namespace GitHappens.Git
{
    /// <summary>
    /// Handles all process interaction with Git
    /// </summary>
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
        public static bool testGit()
        {
            return runGitCommand("--version").Trim().Contains("git version");
        }

        /// <summary>
        /// Get the users set Git email
        /// </summary>
        /// <returns></returns>
        public static string getUserEmail()
        {
            return runGitCommand("config user.email").Trim();
        }

        /// <summary>
        /// Sets the users email to the global email in Git
        /// </summary>
        /// <param name="email">Email to change the git settings to</param>
        /// <returns></returns>
        public static string setUserEmail(string email)
        {
            return runGitCommand(String.Format("config --global user.email {0}", email)).Trim();
        }

        /// <summary>
        /// Get the top level of the current Git repo
        /// </summary>
        /// <returns>String pointing to the top level of the current Git repo</returns>
        public static string getRepoRoot()
        {
           return runGitCommand("rev-parse --show-toplevel").Trim();
        }
        /// <summary>
        /// On call start try to setup Git if it has not previously been set, return the status of the Git binary location
        /// </summary>
        public static string setupGit()
        {
            // Set a variable 'gitExists' to the status of wether or not git existed at the default intsall path
            string gitExists = GitManager.gitExistsAtDefault();

            // If the property was not already set set it
            if (Properties.Settings.Default.gitPath.Length <= 0)
            {
                Properties.Settings.Default.gitPath = gitExists;
                
            }

            // However if has been set simply retrieve the value
            else
            {
                gitExists = Properties.Settings.Default.gitPath;
            }

            // Save property changes
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
            // Verify the file actually exists
            if (filePath == null || !File.Exists(filePath)){
                return false;
            }

            // Change the current project directory to the
            Directory.SetCurrentDirectory(Directory.GetParent(filePath).FullName);

            // Run the status command to see if we are currently in a git repo
            return !runGitCommand("status").Contains("not a git repository") && filePath.Length > 0;
        }

        /// <summary>
        /// Check to see if any changes have been made to the point where it is worth committing the file
        /// </summary>
        /// <param name="filePath">File to check</param>
        /// <returns>If we can commit the current file</returns>
        public static bool canCommit(string filePath)
        {
            return !runGitCommand(String.Format("status {0}", filePath)).Trim().Contains("working tree clean");
        }

        /// <summary>
        /// See if there are actually commits waiting to be pushed
        /// </summary>
        /// <returns>The status of pushability</returns>
        public static bool canPush()
        {
            return runGitCommand("status").Contains("Your branch is ahead of");
        }

        /// <summary>
        /// Open the Commit / Push dialog
        /// </summary>
        public static void openCommitDialog()
        {
            new CommitDialog().ShowDialog();
        }

        /// <summary>
        /// Push content to the cloud
        /// </summary>
        /// <returns>The status of completion</returns>
        public static string pushFiles()
        {
            // Update the files before push
            updateFiles();
            string pushResult = runGitCommand("push");
            if (pushResult.Contains("fatal"))
            {
                pushResult = String.Format("Failed to push: {0}", pushResult);
            }
            else
            {
                pushResult = "Push Successful";

                // Automatically Unlock the file after a successful push
                if(Properties.Settings.Default.unlockOnPush)
                    LockFileManager.unlockFile(EnvironmentManager.getCurrentDocument(), true);
            }

            return pushResult;           
        }

        /// <summary>
        /// Updates all files in the repository forcing the remote changes
        /// </summary>
        public static void updateFiles()
        {
            if (MessageBox.Show("This will overwrite any uncommitted changes. Are you sure you wish to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                new Thread(new ThreadStart(() =>
                {
                    MessageBox.Show(runGitCommand("fetch"));
                    MessageBox.Show(runGitCommand("restore ."));
                    MessageBox.Show(runGitCommand("pull"));
                }));
            }
        }

        /// <summary>
        /// Create a local commit
        /// </summary>
        /// <param name="message">Message to describe the commit</param>
        /// <returns>Result of the Git proc.</returns>
        public static string commitStaged(string message)
        {
            return runGitCommand(String.Format("commit -m \"{0}\"", message));
        }

        /// <summary>
        /// Commit and Push the lock file to lock editing
        /// </summary>
        /// <returns></returns>
        public static void pushLockFile(string filePath, bool locked)
        {
            if (LockFileManager.canEditFile(filePath))
            {
                // Commit message that uses the name of the commit in GitHub
                string commitMessage = String.Format("has now {0} {1}", locked ? "locked" : "unlocked", Path.GetFileName(filePath));
                runGitCommand(String.Format("add {0}/.git_lock.lck", GitManager.getRepoRoot()));
                runGitCommand(String.Format("commit -m \"{0}\"", commitMessage));
                runGitCommand("push");
                MessageBox.Show(String.Format(Path.GetFileName(filePath) + " has successfully been {0}", locked ? "locked" : "unlocked"), "Information");
            }
           
        }

        /// <summary>
        /// Stage a file for commit
        /// </summary>
        /// <param name="filePath"></param>
        public static void stageFile(string filePath)
        {
            runGitCommand(String.Format("add {0}", filePath));
        }

        /// <summary>
        /// Unstage a file for commit
        /// </summary>
        /// <param name="filePath"></param>
        public static void unstageFile(string filePath)
        {
            runGitCommand(String.Format("restore --staged {0}", filePath));
        }

        /// <summary>
        /// Pull down any new changes to the lock file
        /// </summary>
        /// <returns>Result of the Git Proc</returns>
        public static void updateLockFile()
        {
            runGitCommand("fetch");
            runGitCommand(String.Format("checkout origin/main {0}/.git_lock.lck", getRepoRoot()));
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
                {
                    return error;
                }
                else
                {
                    return gitProc.StandardOutput.ReadToEnd();
                }
                
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
