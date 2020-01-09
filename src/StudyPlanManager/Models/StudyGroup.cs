using System;
using System.Collections.Generic;

namespace StudyPlanManager.Models
{
    public class StudyGroup: TreeBase
    {
        public string GroupName { get; set; }

        public List<Study> Studies { get; set; }

        public StudyGroup() : base()
        {
            Studies = new List<Study>();
        }

        public Study FindStudyByTreeId(string treeId)
        {
            foreach (var study in Studies)
            {
                if (study.TreeId.Equals(treeId))
                {
                    return study;
                }
            }
            return null;
        }
    }
}
