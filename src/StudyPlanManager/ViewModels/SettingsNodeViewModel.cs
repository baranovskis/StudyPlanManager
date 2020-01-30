using System.Collections.Generic;

namespace StudyPlanManager.ViewModels
{
    public class SettingsNodeViewModel
    {
        public string TreeId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public List<string> AllowedTypes { get; set; }

        public List<SettingsNodeViewModel> Items { get; set; }

        public SettingsNodeViewModel()
        {
            AllowedTypes = new List<string>();
            Items = new List<SettingsNodeViewModel>();
        }
    }
}
