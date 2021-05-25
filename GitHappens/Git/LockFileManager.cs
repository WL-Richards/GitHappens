using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace GitHappens.Git
{
    /// <summary>
    /// Manages the Git lockfile
    /// </summary>
    class LockFileManager
    {
        /// <summary>
        /// Get an array representation of the current file permissions
        /// 
        /// Index 0 - Status of File lock
        /// Index 1- Whether or not the current user can edit the file even if it is locked
        /// </summary>
        /// <param name="fileName">Name of the requested file</param>
        /// <returns>Array of 2 bools one for whether or not the file is exists in the lock file and one for if we can edit it</returns>
        private static bool[] getFilePerms(string filePath)
        {
            // File is unlocked and we can edit it
            bool[] locked = { false, true };
            // Check if the current file is in a git repository
            if (GitManager.inGitRepo(filePath))
            {

                // Get the file path to the lock file
                string lockFilePath = GitManager.getRepoRoot() + "/.git_lock.lck";

                // Get the file path to the root of the repository converting forward slash delimiters into the Windows equivalent while escaping back slashes
                string topLevel = GitManager.getRepoRoot().Replace("/", "\\");

                // Check if the lock file exists, if not create it
                if (!File.Exists(lockFilePath))
                {
                    File.Create(lockFilePath);
                }

                // Read the lock file
                string[] lines = File.ReadAllLines(lockFilePath);

                // Check if the file is empty
                if (lines.Length <= 0)
                {
                    // File is not locked and we can edit it
                    locked[0] = false;
                    locked[1] = true;
                }

                // Loop through each line in the lock file
                foreach (string line in lines)
                {
                    
                    // If the line contains the current file
                    if (line.Contains(filePath.Replace(topLevel, "").Trim()))
                    {
                        // Next, Check if our email is associated with that file
                        if (line.Contains(GitManager.getUserEmail()))
                        {
                            // File is locked and file is editable by the user
                            locked[0] = true;
                            locked[1] = true;
                        }
                        else
                        {
                            // File is locked and the current user cant edit
                            locked[0] = true;
                            locked[1] = false;
                        }

                        // Return the status of the file
                        return locked;
                    }
                }
            }
            // Return the status of the file
            return locked;


        }

        /// <summary>
        /// Checks if the file is able to be edited by the current user
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Status of our edit-ability</returns>
        public static bool canEditFile(string filePath)
        {
            return getFilePerms(filePath)[1];
        }

        /// <summary>
        /// Checks if the file is in the lock file.
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Status of file lock</returns>
        public static bool isFileLocked(string filePath)
        {
            return getFilePerms(filePath)[0];
        }

        /// <summary>
        /// Lock the passed file locally within the lock file
        /// </summary>
        /// <param name="filePath">Path to locked file</param>
        private static void lockFileLocal(string filePath)
        {
            if (GitManager.inGitRepo(filePath))
            {
                string lockFilePath = GitManager.getRepoRoot().Replace("/", "\\") + "\\.git_lock.lck";
                string topLevel = GitManager.getRepoRoot().Replace("/", "\\");

                // Check if the file is not locked and we can edit it
                if (!isFileLocked(filePath) && canEditFile(filePath))
                {
                    using (StreamWriter sw = File.AppendText(lockFilePath))
                    {
                        sw.WriteLine(String.Format("{0}, {1}", filePath.Replace(topLevel, ""), GitManager.getUserEmail()));
                        sw.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Unlock the requested file locally
        /// </summary>
        /// <param name="filePath"></param>
        private static void unlockFileLocal(string filePath)
        {
            if (GitManager.inGitRepo(filePath))
            {
                string lockFilePath = GitManager.getRepoRoot() + "/.git_lock.lck";
                string topLevel = GitManager.getRepoRoot().Replace("/", "\\");

                // Verify to make sure the file we are trying to lock is not already locked
                if (isFileLocked(filePath) && canEditFile(filePath))
                {
                    // Write the new data to the lock file
                    var lines = File.ReadAllLines(lockFilePath).Where(line => !line.Trim().Contains(filePath.Replace(topLevel, ""))).ToArray();
                    File.WriteAllLines(lockFilePath, lines);
                }
            }
        }

        /// <summary>
        /// Unlocks file and pushes to git
        /// </summary>
        /// <param name="filePath"></param>
        public static void unlockFile(string filePath, bool isAutoLock)
        {
            if (LockFileManager.canEditFile(filePath))
            {
                unlockFileLocal(filePath);
                GitManager.pushLockFile(filePath, false);
            }
            else if (!isAutoLock)
            {
                MessageBox.Show("This file is currently locked by another user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        /// <summary>
        /// Locks the file and pushes to Git
        /// </summary>
        /// <param name="filePath"></param>
        public static void lockFile(string filePath, bool isAutoLock)
        {
            if (canEditFile(filePath))
            {
                lockFileLocal(filePath);
                GitManager.pushLockFile(filePath, true);
            }
            else if (!isAutoLock)
            {
                MessageBox.Show("This file is currently locked by another user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
