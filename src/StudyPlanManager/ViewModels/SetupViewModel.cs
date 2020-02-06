using StudyPlanManager.Models;
using System.Collections.Generic;

namespace StudyPlanManager.ViewModels
{
    public class SetupViewModel
    {
        public List<Study> Studies;
        public List<StudyGroup> Groups;
        public List<StudyCourse> Courses;

        public SetupViewModel()
        {
            Studies = new List<Study>();
            Groups = new List<StudyGroup>();
            Courses = new List<StudyCourse>();
        }
    }
}
