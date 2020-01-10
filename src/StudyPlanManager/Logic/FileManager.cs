using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanManager.Logic
{
    public static class FileManager
    {
        public const string SettingsPath = @"Data\Defaults\";
        public const string DataPath = @"Data\Projects\";
        public const string FileExtension = "stp";

        public static void WriteToFile(string filePath, string fileContent)
        {
            var file = new FileInfo(filePath);

            // Does nothing if already exists
            file.Directory.Create();

            File.WriteAllText(file.FullName, fileContent);
        }

        public static string ReadFromFile(string filePath)
        {
            string text = File.ReadAllText(filePath, Encoding.UTF8);

            return text;
        }

        public static void DeleteFile(string filePath)
        {
            var file = new FileInfo(filePath);

            if (file.Exists)
            {
                file.Delete();
            }
        }

        public static IEnumerable<string> GetFileList(string path, string fileExtension)
        {
            var files = new List<string>();

            if (Directory.Exists(path))
            {
                string[] filePaths = Directory.GetFiles(path, "*." + fileExtension);

                foreach (var filePath in filePaths)
                {
                    var file = new FileInfo(filePath);
                    files.Add(file.Name);
                }
            }

            return files;
        }
    }
}
