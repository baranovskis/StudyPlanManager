using StudyPlanManager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
        public List<StudyGroup> AvailableStudyGroups { get; set; }
        public List<StudyCourse> AvailableStudyCourses { get; set; }

        public SettingManager()
        {
            LoadSettings();
            LoadDefaultConfiguration();
            SaveSettings();
        }

        public void LoadSettings()
        {
            AvailableStudies = FileManager.LoadObjectFromFile<List<Study>>(FileManager.SettingsPath, "studies.xml");
            AvailableStudyGroups = FileManager.LoadObjectFromFile<List<StudyGroup>>(FileManager.SettingsPath, "study_groups.xml");
            AvailableStudyCourses = FileManager.LoadObjectFromFile<List<StudyCourse>>(FileManager.SettingsPath, "study_courses.xml");
            DefaultStudyProject = FileManager.LoadObjectFromFile<StudyProject>(FileManager.SettingsPath, "default.xml");
        }

        public void SaveSettings()
        {
            FileManager.SaveObjectToFile(AvailableStudies, FileManager.SettingsPath, "studies.xml");
            FileManager.SaveObjectToFile(AvailableStudyGroups, FileManager.SettingsPath, "study_groups.xml");
            FileManager.SaveObjectToFile(AvailableStudyCourses, FileManager.SettingsPath, "study_courses.xml");
            FileManager.SaveObjectToFile(DefaultStudyProject, FileManager.SettingsPath, "default.xml");
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
                        StudyName = "Latviešu valoda un literatūra I",
                        IsObligatory = true,
                        CreditPointLimit = 12
                    },
                    new Study
                    {
                        StudyName = "Latviešu valoda un literatūra II",
                        IsObligatory = false,
                        CreditPointLimit = 6
                    },
                    new Study
                    {
                        StudyName = "Svešvaloda I",
                        IsObligatory = false,
                        CreditPointLimit = 12
                    },
                    new Study
                    {
                        StudyName = "Svešvaloda II",
                        IsObligatory = false,
                        CreditPointLimit = 6
                    },
                    new Study
                    {
                        StudyName = "Svešvaloda",
                        IsObligatory = false,
                        CreditPointLimit = 6
                    },
                    new Study
                    {
                        StudyName = "Mazākumtautības valoda un literatūra",
                        IsObligatory = false,
                        CreditPointLimit = 6
                    }
                };
            }

            if (AvailableStudyGroups == null)
            {
                AvailableStudyGroups = new List<StudyGroup>
                {
                    new StudyGroup
                    {
                        GroupName = "Valodu mācību joma",
                        MinimalStudyCount = 3
                    },
                    new StudyGroup
                    {
                        GroupName = "Sociālā un pilsoniskā mācību joma",
                        MinimalStudyCount = 1
                    },
                    new StudyGroup
                    {
                        GroupName = "Kultūras izpratnes un pašizpausmes mākslā mācību joma",
                        MinimalStudyCount = 2
                    },
                    new StudyGroup
                    {
                        GroupName = "Dabaszinātņu mācību joma",
                        MinimalStudyCount = 4
                    },
                    new StudyGroup
                    {
                        GroupName = "Matemātikas vmācību joma",
                        MinimalStudyCount = 1
                    },
                    new StudyGroup
                    {
                        GroupName = "Tehnoloģiju mācību joma",
                        MinimalStudyCount = 1
                    },
                    new StudyGroup
                    {
                        GroupName = "Veselīnas, drošības un fiziskās aktivitātes mācību joma",
                        MinimalStudyCount = 1
                    },
                    new StudyGroup
                    {
                        GroupName = "Starpdisciplinārs kurss",
                        MinimalStudyCount = 1
                    }
                };
            }

            if (AvailableStudyCourses == null)
            {
                AvailableStudyCourses = new List<StudyCourse>
                {
                    new StudyCourse
                    {
                        CourseName = "Pamatkursi",
                        BackgroundColor = "#90ee90"
                    },
                    new StudyCourse
                    {
                        CourseName = "Padziļinātie kursi",
                        BackgroundColor = "#FFFFE0"
                    },
                    new StudyCourse
                    {
                        CourseName = "Specializētie kursi",
                        BackgroundColor = "#FFB6C1"
                    }
                };
            }
        }

        public StudyCourse GetCourseByTreeId(string treeId)
        {
            if (String.IsNullOrEmpty(treeId))
            {
                throw new ArgumentException("Argument 'treeId' is null or empty");
            }

            return AvailableStudyCourses.FirstOrDefault(e => e.TreeId.Equals(treeId));
        }

        public StudyGroup GetGroupByTreeId(string treeId)
        {
            if (String.IsNullOrEmpty(treeId))
            {
                throw new ArgumentException("Argument 'treeId' is null or empty");
            }

            return AvailableStudyGroups.FirstOrDefault(e => e.TreeId.Equals(treeId));
        }

        public Study GetStudyByTreeId(string treeId)
        {
            if (String.IsNullOrEmpty(treeId))
            {
                throw new ArgumentException("Argument 'treeId' is null or empty");
            }

            return AvailableStudies.FirstOrDefault(e => e.TreeId.Equals(treeId));
        }
    }
}
