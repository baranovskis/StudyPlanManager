using StudyPlanManager.Models;
using System.Collections.Generic;

namespace StudyPlanManager.Logic
{
    public class SettingManager
    {
        private static SettingManager _instance;

        public static SettingManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SettingManager();
                }

                return _instance;
            }
        }
        
        public StudyProject DefaultStudyProject { get; set; }
        public List<Study> AvailableStudies { get; set; }

        public SettingManager()
        {
            LoadSettings();
            LoadDefaultConfiguration();
            SaveSettings();
        }

        public void LoadSettings()
        {
            DefaultStudyProject = FileManager.LoadObjectFromFile<StudyProject>(FileManager.SettingsPath, "default.xml");
            AvailableStudies = FileManager.LoadObjectFromFile<List<Study>>(FileManager.SettingsPath, "studies.xml");
        }

        public void SaveSettings()
        {
            FileManager.SaveObjectToFile(DefaultStudyProject, FileManager.SettingsPath, "default.xml");
            FileManager.SaveObjectToFile(AvailableStudies, FileManager.SettingsPath, "studies.xml");
        }

        private void LoadDefaultConfiguration()
        {
            if (DefaultStudyProject == null)
            {
                DefaultStudyProject = StudyManager.Instance.CreateStudyProject("default", null);
            }

            if (AvailableStudies == null)
            {
                AvailableStudies = new List<Study>()
                {
                    new Study
                    {
                        StudyName = "Latv 1",
                        IsObligatory = true,
                        CreditPointLimit = 12
                    },
                    new Study
                    {
                        StudyName = "Anglu 1",
                        IsObligatory = false,
                        CreditPointLimit = 6
                    },
                    new Study
                    {
                        StudyName = "Latv 2",
                        IsObligatory = false,
                        CreditPointLimit = 6
                    },
                    new Study
                    {
                        StudyName = "Latgaliesu",
                        IsObligatory = false,
                        CreditPointLimit = 6
                    }
                };
            }
        }
    }
}
