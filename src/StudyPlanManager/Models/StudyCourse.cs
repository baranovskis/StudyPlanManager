using System;
using System.Collections.Generic;
using System.Drawing;

namespace StudyPlanManager.Models
{
    public class StudyCourse: TreeBase
    {
        public string CourseName { get; set; }

        public List<StudyGroup> Groups { get; set; }

        public Color BackgroundColor { get; set; }

        public StudyCourse() : base()
        {
            Groups = new List<StudyGroup>();
        }
    }
}
