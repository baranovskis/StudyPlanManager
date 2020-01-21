using StudyPlanManager.Models;
using System.Collections.Generic;

namespace StudyPlanManager.ViewModels
{
    public class StudyProjectViewModel
    {
        public StudyProject Project { get; set; }

        public List<StudyInfoMessage> Messages { get; set; }

        public StudyProjectViewModel()
        {
            Messages = new List<StudyInfoMessage>();
        }
    }
}
