using System;
using System.Collections.Generic;

namespace StudyPlanManager.Models
{
    public class StudyCourse: TreeBase
    {
        public string CourseName { get; set; }

        public List<StudyGroup> Groups { get; set; }

        public StudyCourse() : base()
        {
            Groups = new List<StudyGroup>();
        }

        public Study FindStudyByTreeId(string treeId)
        {
            foreach (var group in Groups)
            {
                var study = group.FindStudyByTreeId(treeId);
                if (study != null)
                {
                    return study;
                }
            }

            return null;
        }
    }
}
