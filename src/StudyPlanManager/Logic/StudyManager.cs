using StudyPlanManager.Models;
using StudyPlanManager.Utility;
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

                var fileList = FileManager.GetFileList(AppDomain.CurrentDomain.BaseDirectory + FileManager.DataPath, "xml");

                foreach (var fileName in fileList)
                {
                    var studyProject = FileManager.LoadObjectFromFile<StudyProject>(FileManager.DataPath, fileName);

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
            {
                throw new ArgumentException("Argument 'id' is null or empty");
            }

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
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Argument 'id' is null or empty");
            }

            if (String.IsNullOrEmpty(treeId))
            {
                throw new ArgumentException("Argument 'treeId' is null or empty");
            }

            var studyProject = GetStudyProject(id);

            if (studyProject == null)
            {
                return null;
            }

            Study study = null;

            lock (_locker)
            {
                study = FindStudyByTreeId(studyProject, treeId);
            }

            return study;
        }

        public Study GetStudy(StudyProject studyProject, string treeId)
        {
            if (String.IsNullOrEmpty(treeId))
            {
                throw new ArgumentException("Argument 'treeId' is null or empty");
            }

            if (studyProject == null)
            {
                return null;
            }

            Study study = null;

            lock (_locker)
            {
                study = FindStudyByTreeId(studyProject, treeId);
            }

            return study;
        }

        public StudyProject CreateStudyProject(string name, string parentId)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Argument 'name' is null or empty");
            }

            var studyProject = new StudyProject();

            if (!String.IsNullOrEmpty(parentId))
            {
                // Create from existing project.
                var existingStudyProject = StudyManager.Instance.GetStudyProject(parentId);
                if (existingStudyProject != null)
                {
                    studyProject = existingStudyProject.Clone();
                }
                else
                {
                    throw new NullReferenceException("Specified parent study project does not exist!");
                }
            }
            else
            {
                // Populate with courses
                foreach(var course in SettingManager.Instance.AvailableStudyCourses)
                {
                    UpdateCourses(studyProject, course);
                }

                // Populate with groups
                foreach (var group in SettingManager.Instance.AvailableStudyGroups)
                {
                    UpdateGroups(studyProject, group);
                }
            }

            studyProject.Id = Guid.NewGuid().ToString("N");
            studyProject.Name = name;
            studyProject.CreationDate = DateTime.Now;
            studyProject.LastUpdatedDate = DateTime.Now;
            studyProject.FileName = Guid.NewGuid().ToString("N") + ".xml";

            // Add to list
            StudyProjects.Add(studyProject);

            // Initial save to file
            FileManager.SaveObjectToFile(studyProject, FileManager.DataPath, studyProject.FileName);

            return studyProject;
        }

        public bool SaveAllStudyProjects()
        {
            foreach (var studyProject in StudyProjects)
            {
                lock (_locker)
                {
                    studyProject.LastUpdatedDate = DateTime.Now;
                    FileManager.SaveObjectToFile(studyProject, FileManager.DataPath, studyProject.FileName);
                }
            }

            return true;
        }

        public bool SaveStudyProject(string id, string name)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Argument 'id' is null or empty");
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Argument 'name' is null or empty");
            }

            var studyProject = GetStudyProject(id);

            if (studyProject == null)
            {
                return false;
            }

            lock (_locker)
            {
                studyProject.Name = name;
                studyProject.LastUpdatedDate = DateTime.Now;
                FileManager.SaveObjectToFile(studyProject, FileManager.DataPath, studyProject.FileName);
            }

            return true;
        }

        public bool DeleteStudyProject(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Argument 'id' is null or empty");
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

        public bool RestoreStudyProject(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Argument 'id' is null or empty");
            }

            var studyProject = GetStudyProject(id);

            if (studyProject == null)
            {
                return false;
            }

            if (String.IsNullOrEmpty(studyProject.FileName))
            {
                throw new Exception($"Study project '{studyProject.Id}' name is empty");
            }

            lock (_locker)
            {
                int index = StudyProjects.FindIndex(ind => ind.Equals(studyProject));

                if (index > -1)
                {
                    StudyProjects[index] = FileManager.LoadObjectFromFile<StudyProject>(FileManager.DataPath, studyProject.FileName);
                }
            }

            return true;
        }

        private void DeleteStudyProjectFile(StudyProject studyProject)
        {
            if (studyProject == null)
            {
                throw new ArgumentException("Argument 'studyProject' is null");
            }

            if (String.IsNullOrEmpty(studyProject.FileName))
            {
                throw new Exception($"Study project '{studyProject.Id}' name is empty!");
            }

            string fullFilePath = AppDomain.CurrentDomain.BaseDirectory + FileManager.DataPath + studyProject.FileName;
            FileManager.DeleteFile(fullFilePath);
        }

        public Study FindStudyByTreeId(StudyGroup studyGroup, string treeId)
        {
            if (studyGroup == null)
            {
                throw new ArgumentException("Argument 'studyGroup' is null");
            }

            if (String.IsNullOrEmpty(treeId))
            {
                throw new Exception("Argument 'treeId' is null or empty!");
            }

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
            if (studyCourse == null)
            {
                throw new ArgumentException("Argument 'studyCourse' is null");
            }

            if (String.IsNullOrEmpty(treeId))
            {
                throw new Exception("Argument 'treeId' is null or empty!");
            }

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
            if (studyProject == null)
            {
                throw new ArgumentException("Argument 'studyProject' is null");
            }

            if (String.IsNullOrEmpty(treeId))
            {
                throw new Exception("Argument 'treeId' is null or empty!");
            }

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

        public void UpdateStudies(Study newStudy)
        {
            if (newStudy == null)
            {
                throw new ArgumentException("Argument 'newStudy' is null");
            }

            foreach (var studyProject in StudyProjects)
            {
                UpdateStudies(studyProject, newStudy);
            }
        }

        public void UpdateStudies(StudyProject studyProject, Study newStudy)
        {
            if (studyProject == null)
            {
                throw new ArgumentException("Argument 'studyProject' is null");
            }

            if (newStudy == null)
            {
                throw new ArgumentException("Argument 'newStudy' is null");
            }

            foreach (var studyCourse in studyProject.Courses)
            {
                UpdateStudies(studyCourse, newStudy);
            }
        }

        public void UpdateStudies(StudyCourse studyCourse, Study newStudy)
        {
            if (studyCourse == null)
            {
                throw new ArgumentException("Argument 'studyCourse' is null");
            }

            if (newStudy == null)
            {
                throw new ArgumentException("Argument 'newStudy' is null");
            }

            foreach (var studyGroup in studyCourse.Groups)
            {
                UpdateStudies(studyGroup, newStudy);
            }
        }

        public void UpdateStudies(StudyGroup studyGroup, Study newStudy)
        {
            if (studyGroup == null)
            {
                throw new ArgumentException("Argument 'studyGroup' is null");
            }

            if (newStudy == null)
            {
                throw new ArgumentException("Argument 'newStudy' is null");
            }

            foreach (var study in studyGroup.Studies)
            {
                if (study.TreeId == newStudy.TreeId)
                {
                    study.StudyName = newStudy.StudyName;
                    study.CreditPoints = newStudy.CreditPoints;
                    study.CreditPointLimit = newStudy.CreditPointLimit;
                    study.IsObligatory = newStudy.IsObligatory;
                    study.ParentTreeId = newStudy.ParentTreeId;
                }
            }
        }

        public void UpdateGroups(StudyGroup newStudyGroup)
        {
            if (newStudyGroup == null)
            {
                throw new ArgumentException("Argument 'newStudyGroup' is null");
            }

            foreach (var studyProject in StudyProjects)
            {
                UpdateGroups(studyProject, newStudyGroup);
            }
        }

        public void UpdateGroups(StudyProject studyProject, StudyGroup newStudyGroup)
        {
            if (studyProject == null)
            {
                throw new ArgumentException("Argument 'studyProject' is null");
            }

            if (newStudyGroup == null)
            {
                throw new ArgumentException("Argument 'newStudyGroup' is null");
            }

            foreach (var studyCourse in studyProject.Courses)
            {
                UpdateGroups(studyCourse, newStudyGroup);
            }
        }

        public void UpdateGroups(StudyCourse studyCourse, StudyGroup newStudyGroup)
        {
            if (studyCourse == null)
            {
                throw new ArgumentException("Argument 'studyCourse' is null");
            }

            if (newStudyGroup == null)
            {
                throw new ArgumentException("Argument 'newStudyGroup' is null");
            }

            bool foundGroup = false;

            foreach (var studyGroup in studyCourse.Groups)
            {
                if (studyGroup.TreeId == newStudyGroup.TreeId)
                {
                    foundGroup = true;

                    studyGroup.GroupName = newStudyGroup.GroupName;
                    studyGroup.MinimalStudyCount = newStudyGroup.MinimalStudyCount;
                }
            }

            if (!foundGroup)
            {
                studyCourse.Groups.Add(newStudyGroup);
            }
        }

        public void UpdateCourses(StudyCourse newStudyCourse)
        {
            if (newStudyCourse == null)
            {
                throw new ArgumentException("Argument 'newStudyCourse' is null");
            }

            foreach (var studyProject in StudyProjects)
            {
                UpdateCourses(studyProject, newStudyCourse);
            }
        }

        public void UpdateCourses(StudyProject studyProject, StudyCourse newStudyCourse)
        {
            if (studyProject == null)
            {
                throw new ArgumentException("Argument 'studyProject' is null");
            }

            if (newStudyCourse == null)
            {
                throw new ArgumentException("Argument 'newStudyCourse' is null");
            }

            bool foundCourse = false;

            foreach (var studyCourse in studyProject.Courses)
            {
                if (studyCourse.TreeId == newStudyCourse.TreeId)
                {
                    foundCourse = true;

                    studyCourse.CourseName = newStudyCourse.CourseName;
                    studyCourse.BackgroundColor = newStudyCourse.BackgroundColor;
                }
            }

            // Add course to project if not exists
            if (!foundCourse)
            {
                studyProject.Courses.Add(newStudyCourse);
            }
        }
    }
}