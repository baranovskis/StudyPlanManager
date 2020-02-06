using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace StudyPlanManager.Models
{
    [Serializable]
    public class StudyCourse : TreeBase
    {
        public string CourseName { get; set; }

        public List<StudyGroup> Groups { get; set; }

        public string BackgroundColor { get; set; }

        public StudyCourse() : base()
        {
            Groups = new List<StudyGroup>();
            BackgroundColor = "#fff";
        }
    }
}