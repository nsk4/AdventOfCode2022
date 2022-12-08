using System;
using System.Collections.Generic;

namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int sizeThreshold = 100000;
            const int totalSpace = 70000000;
            const int upgradeSize = 30000000;

            // Parse folder structure
            Folder root = new Folder("/", null);
            Folder current = root;
            foreach (string line in System.IO.File.ReadLines("input.txt"))
            {
                string[] splits = line.Split(' ');

                switch (splits[0])
                {
                    case "$":
                        // Line is a command
                        // No need to do anything for ls command
                        if (splits[1].Equals("cd"))
                        {
                            // Change active folder
                            if (splits[2].Equals("/"))
                            {
                                // Reset to root
                                current = root;
                            }
                            else if (splits[2].Equals(".."))
                            {
                                // Move to parent folder
                                if (current != root)
                                {
                                    current = current.GetParentFolder();
                                }
                                else
                                {
                                    Console.WriteLine("Current folder is already root.");
                                }
                            }
                            else
                            {
                                // Move to selected folder
                                current = current.GetSubFolder(splits[2]);
                            }
                        }
                        break;
                    case "dir":
                        // Line is a folder info
                        current.AddFolder(splits[1]);
                        break;
                    default:
                        // Line is a file info
                        current.AddFile(splits[1], int.Parse(splits[0]));
                        break;
                }
            }

            List<Folder> allFolders = root.GetAllSubFolderList();
            allFolders.Sort((x, y) => x.GetSize().CompareTo(y.GetSize()));

            #region Part 1 - total size of small folders
            int smallFoldersSize = 0;
            allFolders.ForEach(folder =>
            {
                int folderSize = folder.GetSize();
                if (folderSize < sizeThreshold)
                {
                    smallFoldersSize += folder.GetSize();
                }
                Console.WriteLine("Folder: " + folder.GetName() + ", size: " + folderSize);
            });

            Console.WriteLine("Total size of small folders: " + smallFoldersSize);
            #endregion

            #region Part 2 - optimal folder to delete
            int requiredAdditionalSpace = upgradeSize - (totalSpace - root.GetSize());
            // Since list is ordered the first folder that is bigger than required additional space needs to be deleted.
            Folder folderToDelete = allFolders.Find(x => x.GetSize() > requiredAdditionalSpace);

            Console.WriteLine("Deleting folder " + folderToDelete.GetName() + " with size " + folderToDelete.GetSize() + " will be sufficient for the upgrade.");
            #endregion

            Console.ReadLine();
        }
    }
}
