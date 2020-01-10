using StudyPlanManager.Models;
using System;
using System.Collections.Generic;

namespace StudyPlanManager.Logic
{
    public class StudyManager
    {
        private static StudyManager _instance;

        public static StudyManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudyManager();
                }

                return _instance;
            }
        }

        public List<StudyProject> StudyProjects { get; private set; }

        private object _locker = new object();

        public StudyManager()
        {
            LoadStudyProjects();
        }

        public void LoadStudyProjects()
        {
            lock (_locker)
            {
                StudyProjects = new List<StudyProject>();

                var fileList = FileManager.GetFileList(AppDomain.CurrentDomain.BaseDirectory + FileManager.DataPath, FileManager.FileExtension);

                foreach(var fileName in fileList)
                {
                    var studyProject = LoadStudyProjectFile(fileName);
                    if (studyProject != null)
                    {
                        StudyProjects.Add(studyProject);
                    }
                }
            }
        }

        public StudyProject GetStudyProject(string id)
        {
            if (String.IsNullOrEmpty(id))
                throw new ArgumentException("Id is empty");

            lock (_locker)
            {
                foreach (var studyProject in StudyProjects)
                {
                    if (studyProject.Id.Equals(id))
                    {
                        return studyProject;
                    }
                }
            }

            return null;
        }

        public Study GetStudy(string id, string treeId)
        {
            Study study = null;
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id is empty");
            }

            if (String.IsNullOrEmpty(treeId))
            {
                throw new ArgumentException("TreeId is empty");
            }

            var studyProject = GetStudyProject(id);
            if (studyProject == null)
            {
                return null;
            }

            lock (_locker)
            {
                study = FindStudyByTreeId(studyProject, treeId);
            }

            return study;
        }

        // TODO: Rewrite me senpai :3
        public StudyProject CreateStudyProject(string name, string parentId)
        {
            var studyProject = new StudyProject();

            var courses = new List<StudyCourse>
            {
                new StudyCourse
                {
                    CourseName = "Pamatkursi",
                    Groups = new List<StudyGroup>
                    {
                        new StudyGroup
                        {
                            GroupName = "Valodu",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Latviesu valoda un literatura"
                                },
                                new Study
                                {
                                    StudyName = "Anglu val. I"
                                },
                                new Study
                                {
                                    StudyName = "Vacu val. I"
                                }
                            }
                        },
                        new StudyGroup
                        {
                            GroupName = "Sociala un pilsoniska",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Vestures un socialas zinatnes I"
                                }
                            }
                        },
                        new StudyGroup
                        {
                            GroupName = "Dabaszinatnu",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Fizika I"
                                },
                                new Study
                                {
                                    StudyName = "Kimija I"
                                },
                                new Study
                                {
                                    StudyName = "Biologija I"
                                }
                            }
                        },
                    }
                },

                new StudyCourse
                {
                    CourseName = "Specialie kursi",
                    Groups = new List<StudyGroup>
                    {
                        new StudyGroup
                        {
                            GroupName = "Valodu",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Anglu val. II"
                                },
                                new Study
                                {
                                    StudyName = "Vacu val. II"
                                }
                            }
                        },
                        new StudyGroup
                        {
                            GroupName = "Dabaszinatnu",
                            Studies = new List<Study>
                            {
                                new Study
                                {
                                    StudyName = "Fizika II"
                                },
                                new Study
                                {
                                    StudyName = "Kimija II"
                                },
                            }
                        },
                    }
                }
            };


            var rand = new Random();

            foreach (var course in courses)
            {
                foreach (var group in course.Groups)
                {
                    foreach (var study in group.Studies)
                    {
                        for (var i = 0; i < study.CreditPoints.Length; i++)
                        {
                            study.CreditPoints[i] = rand.Next(0, 10);
                        }
                    }
                }
            }

            studyProject.Id = Guid.NewGuid().ToString("N");
            studyProject.Name = name;
            //studyProject.FileName = fileName;
            studyProject.Courses = courses;
            studyProject.CreationDate = DateTime.Now;
            studyProject.LastUpdatedDate = DateTime.Now;

            // Add to list
            StudyProjects.Add(studyProject);

            return studyProject;
        }

        public bool SaveStudyProject(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id is empty");
            }

            var studyProject = GetStudyProject(id);
            if (studyProject == null)
            {
                return false;
            }

            lock (_locker)
            {
                SaveStudyProjectFile(studyProject);
            }

            return true;
        }

        public bool DeleteStudyProject(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id is empty");
            }

            var studyProject = GetStudyProject(id);
            if (studyProject == null)
            {
                return false;
            }

            lock (_locker)
            {
                DeleteStudyProjectFile(studyProject);
                StudyProjects.Remove(studyProject);
            }

            return true;
        }

        public static StudyProject LoadStudyProjectFile(string fileName)
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

        public void SaveStudyProjectFile(StudyProject studyProject)
        {
            if (!String.IsNullOrEmpty(studyProject.FileName))
            {
                var xmlText = studyProject.Serialize();

                string fullFilePath = AppDomain.CurrentDomain.BaseDirectory + FileManager.DataPath + studyProject.FileName;
                FileManager.WriteToFile(fullFilePath, xmlText);
            }
        }

        public void DeleteStudyProjectFile(StudyProject studyProject)
        {
            if (!String.IsNullOrEmpty(studyProject.FileName))
            {
                string fullFilePath = AppDomain.CurrentDomain.BaseDirectory + FileManager.DataPath + studyProject.FileName;
                FileManager.DeleteFile(fullFilePath);
            }
        }

        public Study FindStudyByTreeId(StudyGroup studyGroup, string treeId)
        {
            foreach (var study in studyGroup.Studies)
            {
                if (study.TreeId.Equals(treeId))
                {
                    return study;
                }
            }
            return null;
        }

        public Study FindStudyByTreeId(StudyCourse studyCourse, string treeId)
        {
            foreach (var studyGroup in studyCourse.Groups)
            {
                var study = FindStudyByTreeId(studyGroup, treeId);
                if (study != null)
                {
                    return study;
                }
            }

            return null;
        }

        public Study FindStudyByTreeId(StudyProject studyProject, string treeId)
        {
            foreach (var studyCourse in studyProject.Courses)
            {
                var study = FindStudyByTreeId(studyCourse, treeId);
                if (study != null)
                {
                    return study;
                }
            }

            return null;
        }
    }
}
