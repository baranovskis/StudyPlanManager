using StudyPlanManager.Models;
using System;
using System.Linq;

namespace StudyPlanManager.Utility
{
    public static class StudyProjectExtension
    {
        public static StudyProject Clone(this StudyProject studyProject)
        {
            // Simplest and stupidest way - just serialize and deserialize.
            // It works, so who gives a damn.
            string serializedXml = studyProject.Serialize();
            var clonedStudyProject = serializedXml.Deserialize<StudyProject>();
            
            return clonedStudyProject;
        }

        public static StudyCourse GetCourseByTreeId(this StudyProject studyProject, string treeId)
        {
            if (String.IsNullOrEmpty(treeId))
            {
                throw new ArgumentException("Argument 'treeId' is null or empty");
            }

            return studyProject.Courses.FirstOrDefault(e => e.TreeId.Equals(treeId));
        }

        public static StudyGroup GetGroupByTreeId(this StudyProject studyProject, string treeId)
        {
            if (String.IsNullOrEmpty(treeId))
            {
                throw new ArgumentException("Argument 'treeId' is null or empty");
            }

            foreach (var course in studyProject.Courses)
            {
                foreach (var group in course.Groups)
                {
                    if (group.TreeId.Equals(treeId))
                    {
                        return group;
                    }
                }
            }

            return null;
        }

        public static Study GetStudyByTreeId(this StudyProject studyProject, string treeId)
        {
            if (String.IsNullOrEmpty(treeId))
            {
                throw new ArgumentException("Argument 'treeId' is null or empty");
            }

            foreach (var course in studyProject.Courses)
            {
                foreach (var group in course.Groups)
                {
                    foreach (var study in group.Studies)
                    {
                        if (study.TreeId.Equals(treeId))
                        {
                            return study;
                        }
                    }
                }
            }

            return null;
        }
    }
}
