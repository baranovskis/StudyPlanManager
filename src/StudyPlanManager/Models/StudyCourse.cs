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

        [XmlIgnoreAttribute()]
        public Color BackgroundColor { get; set; }

        [XmlElement("BackgroundColor")]
        public string BackgroundColorHtml
        {
            get
            {
                return ColorTranslator.ToHtml(BackgroundColor);
            }
            set
            {
                BackgroundColor = ColorTranslator.FromHtml(value);
            }
        }

        public StudyCourse() : base()
        {
            Groups = new List<StudyGroup>();
        }
    }
}