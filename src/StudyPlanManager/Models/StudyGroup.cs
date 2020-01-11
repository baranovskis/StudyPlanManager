using System;
using System.Collections.Generic;

namespace StudyPlanManager.Models
{
    public class StudyGroup: TreeBase
    {
        public string GroupName { get; set; }

        public List<Study> Studies { get; set; }

        public int MinimalStudyCount { get; set; }

        public StudyGroup() : base()
        {
            Studies = new List<Study>();
        }
    }
}
