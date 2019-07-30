using System;
using System.IO;
using System.Security.Permissions;

namespace EncodingProgram
{
    static class Validator
    {      
        public static void CheckFile(String path)
        {
            if (!(File.Exists(path)))
            {
                throw new FileNotFoundException($"Source file {path} was not found.");
            }
            CheckAccessRights(FileIOPermissionAccess.Read, path);
        }

        public static void CheckDirectory(String path)
        {
            if (!(Directory.Exists(path)))
            {
                throw new DirectoryNotFoundException($"Destination directory {path} was not found.");
            }
            CheckAccessRights(FileIOPermissionAccess.Append, path);
        }


        public static void CheckDestinationFileAvailable(String path)
        {
            if (File.Exists(path))
            {
                CheckIfDestinationFileNeedsToBeReplace(path);
            }
        }

        private static void CheckIfDestinationFileNeedsToBeReplace(String path)
        {
            if (UserDialog.AskUserAboutFileReplacement(path))
            {
                CheckAccessRights(FileIOPermissionAccess.Write, path);
            }
            else
            {
                throw new ArgumentException("Cannot work without destination file.");
            }
        }

        private static void CheckAccessRights(FileIOPermissionAccess right, String path)
        {
            FileIOPermission filePermission = new FileIOPermission(right, path);
            filePermission.Demand();
        }

    }
}
