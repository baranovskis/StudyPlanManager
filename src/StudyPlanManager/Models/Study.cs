using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanManager.Models
{
    public class Study: TreeBase
    {
        public string StudyName { get; set; }

        public int[] CreditPoints { get; set; }

        public Study() : base()
        {
            CreditPoints = new int[3];
        }
    }
}
