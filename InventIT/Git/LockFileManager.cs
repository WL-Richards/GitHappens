using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace InventIT.Git
{
    /// <summary>
    /// Manages the Git lockfile
    /// </summary>
    class LockFileManager
    {
        /// <summary>
        /// Get an array representation of the current file permissions
        /// </summary>
        /// <param name="fileName">Name of the requested file</param>
        /// <returns>Array of 2 bools one for whether or not the file is exists in the lock file and one for if we can edit it</returns>
        private static bool[] getFilePerms(string filePath)
        {
            // File is locked and we cant edit it
            bool[] locked = {true, false};
            if (GitManager.inGitRepo(filePath))
            {
                string lockFilePath = GitManager.getRepoRoot() + "/git_lock.lck";

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
                    if (line.Contains(Path.GetFileName(filePath).Trim()))
                    {
                        // Check if our email is associated with that file, i
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
                        
                    }
                    // If the file is not found in that line set locked = false as if it is found it will be evaluated
                    else
                    {
                        // File is not locked and the current user could edit it
                        locked[0] = false;
                        locked[1] = true;
                    }
                }

                

            }

            return locked;
        }

        /// <summary>
        /// Checks if the file is able to be edited by us
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
        /// Lock the passed file and push that change to the cloud
        /// </summary>
        /// <param name="filePath">Path to locked file</param>
        public static void lockFile(string filePath)
        {
            if (GitManager.inGitRepo(filePath))
            {
                string lockFilePath = GitManager.getRepoRoot() + "/git_lock.lck";

                // Check if the file is not locked and we can edit it
                if (!isFileLocked(filePath) && canEditFile(filePath))
                {
                    using (StreamWriter sw = File.AppendText(lockFilePath))
                    {
                        sw.WriteLine(String.Format("{0}, {1}", Path.GetFileName(filePath), GitManager.getUserEmail()));
                    }
                }
            }
        }

        /// <summary>
        /// Unlock the requested file
        /// </summary>
        /// <param name="filePath"></param>
        public static void unlockFile(string filePath)
        {
            if (GitManager.inGitRepo(filePath))
            {
                string lockFilePath = GitManager.getRepoRoot() + "/git_lock.lck";

                // Verify to make sure the file we are trying to lock is not already locked
                if (isFileLocked(filePath) && canEditFile(filePath))
                {
                    // Write the new data to the lock file
                    var lines = File.ReadAllLines(lockFilePath).Where(line => !line.Trim().Contains(Path.GetFileName(filePath))).ToArray();
                    File.WriteAllLines(lockFilePath, lines);
                }
            }
        }
    }
}
