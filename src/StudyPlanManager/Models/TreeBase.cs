using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanManager.Models
{
    public class TreeBase
    {
        public readonly string TreeId;

        public TreeBase()
        {
            TreeId = Guid.NewGuid().ToString("N");
        }
    }
}
