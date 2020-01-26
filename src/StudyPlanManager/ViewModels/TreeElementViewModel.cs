using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanManager.ViewModels
{
    public class TreeElementViewModel
    {
        public string TreeId { get; set; }
        public List<TreeElementViewModel> Childs { get; set; }
    }
}
