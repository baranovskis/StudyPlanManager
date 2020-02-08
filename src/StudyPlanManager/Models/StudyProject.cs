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
            FileName = Guid.NewGuid().ToString("N") + ".xml";

            Courses = new List<StudyCourse>();
        }
    }
}
