using System;

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
