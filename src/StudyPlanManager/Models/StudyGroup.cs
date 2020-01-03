using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
