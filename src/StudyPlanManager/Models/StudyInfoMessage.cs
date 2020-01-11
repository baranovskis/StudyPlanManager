using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanManager.Models
{
    public class StudyInfoMessage
    {
        public string Message { get; set; }
        public SeverityLevel SeverityLevel { get; set; }
        public string TreeId { get; set; }
    }
}
