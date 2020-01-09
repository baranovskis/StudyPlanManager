using StudyPlanManager.Logic;
using System;
using System.Collections.Generic;

namespace StudyPlanManager.Models
{
    public class StudyProject
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public List<StudyCourse> Courses { get; set; }

        public StudyProject()
        {
            FileName = Guid.NewGuid().ToString("N") + ".stp";
        }

        public static StudyProject LoadFile(string fileName)
        {
            StudyProject studyProject = null;

            if (!String.IsNullOrEmpty(fileName))
            {
                string fullFilePath = AppDomain.CurrentDomain.BaseDirectory + FileManager.DataPath + fileName;
                string fileContent = FileManager.ReadFromFile(fullFilePath);

                if (!String.IsNullOrEmpty(fileContent))
                {
                    studyProject = fileContent.Deserialize<StudyProject>();
                }
            }

            return studyProject;
        }

        public void SaveFile()
        {
            if (!String.IsNullOrEmpty(FileName))
            {
                var xmlText = this.Serialize();

                string fullFilePath = AppDomain.CurrentDomain.BaseDirectory + FileManager.DataPath + FileName;
                FileManager.WriteToFile(fullFilePath, xmlText);
            }
        }

        public void DeleteFile()
        {
            if (!String.IsNullOrEmpty(FileName))
            {
                string fullFilePath = AppDomain.CurrentDomain.BaseDirectory + FileManager.DataPath + FileName;
                FileManager.DeleteFile(fullFilePath);
            }
        }

        public Study FindStudyByTreeId(string treeId)
        {
            foreach (var course in Courses)
            {
                var study = course.FindStudyByTreeId(treeId);
                if (study != null)
                {
                    return study;
                }
            }

            return null;
        }
    }
}
