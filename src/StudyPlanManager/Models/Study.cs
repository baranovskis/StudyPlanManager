using System;

namespace StudyPlanManager.Models
{
    [Serializable]
    public class Study : TreeBase
    {
        public string StudyName { get; set; }

        public int[] CreditPoints { get; set; }

        public int CreditPointLimit { get; set; }

        public bool IsObligatory { get; set; }

        public string ParentTreeId { get; set; }

        public Study() : base()
        {
            CreditPoints = new int[3];
        }
    }
}