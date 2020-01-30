using StudyPlanManager.Models;
using System.Collections.Generic;

namespace StudyPlanManager.ViewModels
{
    public class SettingsViewModel
    {
        public string Name { get; set; }

        public List<SettingsNodeViewModel> AvailableStudies { get; set; }

        public List<SettingsNodeViewModel> AvailableGroups { get; set; }

        public List<SettingsNodeViewModel> AvailableCourses { get; set; }

        public List<SettingsNodeViewModel> ProjectTree { get; set; }

        public SettingsViewModel()
        {
            AvailableStudies = new List<SettingsNodeViewModel>();
            AvailableGroups = new List<SettingsNodeViewModel>();
            AvailableCourses = new List<SettingsNodeViewModel>();
            ProjectTree = new List<SettingsNodeViewModel>();
        }
    }
}
