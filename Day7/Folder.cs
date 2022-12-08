using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    public class Folder
    {

        private string folderName;
        private Folder parentFolder;
        private List<Folder> subFolders;
        private List<(string, int)> files;

        public Folder(string name, Folder parent)
        {
            folderName = name;
            parentFolder = parent;
            subFolders = new List<Folder>();
            files = new List<(string, int)>();
        }

        public string GetName()
        {
            return folderName;
        }

        public Folder GetSubFolder(string name)
        {
            return subFolders.Find(x => x.GetName().Equals(name));
        }

        public Folder GetParentFolder()
        {
            return parentFolder;
        }

        public void AddFile(string name, int size)
        {
            bool exists = files.Any(file => file.Item1.Equals(name));
            if (!exists)
            {
                files.Add((name, size));
            }
            else
            {
                System.Console.WriteLine("File " + name + " already exists in folder " + folderName + ".");
            }
        }

        public void AddFolder(string name)
        {
            bool exists = subFolders.Any(folder => folder.GetName().Equals(name));
            if (!exists)
            {
                subFolders.Add(new Folder(name, this));
            }
            else
            {
                System.Console.WriteLine("Folder " + name + " already exists in folder " + folderName + ".");
            }
        }

        public int GetSize()
        {
            int totalSize = 0;
            files.ForEach(x => totalSize += x.Item2);
            subFolders.ForEach(x => totalSize += x.GetSize());
            return totalSize;
        }

        public List<Folder> GetAllSubFolderList()
        {
            List<Folder> folders = new List<Folder>();
            folders.AddRange(subFolders);
            subFolders.ForEach(folder => folders.AddRange(folder.GetAllSubFolderList()));
            return folders;
        }

        public List<(string, int)> GetFolderFiles()
        {
            return files;
        }
    }
}
