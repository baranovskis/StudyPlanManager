using System;

namespace StudyPlanManager.Models
{
    public class TreeBase
    {
        public string TreeId { get; set; }

        public TreeBase()
        {
            TreeId = Guid.NewGuid().ToString("N");
        }
    }
}
