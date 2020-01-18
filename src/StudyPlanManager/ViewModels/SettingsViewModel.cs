using StudyPlanManager.Models;
using System.Collections.Generic;

namespace StudyPlanManager.ViewModels
{
    public class SettingsViewModel
    {
        public List<Study> AvailableStudies;

        public SettingsViewModel()
        {
            AvailableStudies = new List<Study>();
        }
    }
}
