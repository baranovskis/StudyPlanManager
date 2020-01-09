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

        public List<StudyVariant> StudyVariants { get; private set; }

        private object _locker = new object();

        public StudyManager()
        {
            // TODO
            // - Load settings from 'Data'
            LoadStudyVariants();
        }

        public void LoadStudyVariants()
        {
            lock (_locker)
            {
                StudyVariants = new List<StudyVariant>();

                // TODO: Load projects from 'Data//Projects'
                for (int i = 0; i < 20; i++)
                {
                    CreateStudyVariant($"Test study variant {i + 1}");
                }
            }
        }

        public StudyVariant GetStudyVariant(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Id is empty");

            lock (_locker)
            {
                foreach (var variant in StudyVariants)
                {
                    if (variant.Id.Equals(id))
                        return variant;
                }
            }

            return null;
        }

        // TODO: Rewrite me senpai :3
        public StudyVariant CreateStudyVariant(string name)
        {
            var studyVariant = new StudyVariant();

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

            //if (fileName != "default.stv")
            {
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
            }

            studyVariant.Id = Guid.NewGuid().ToString("N");
            studyVariant.Name = name;
            //studyVariant.FileName = fileName;
            studyVariant.Courses = courses;
            studyVariant.CreationDate = DateTime.Now;
            studyVariant.LastUpdatedDate = DateTime.Now;

            // Add to list
            StudyVariants.Add(studyVariant);

            return studyVariant;
        }

        // TODO: Finish me senpai :3
        public bool DeleteStudyVariant(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Id is empty");

            var studyVariant = GetStudyVariant(id);

            if (studyVariant == null)
                return false;

            lock (_locker)
            {
                // TODO: Remove file.
                StudyVariants.Remove(studyVariant);
            }

            return true;
        }
    }
}
