using StudyPlanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanManager.Utility
{
    public static class StudyProjectExtension
    {
        public static StudyProject Clone(this StudyProject studyProject)
        {
            // Simplest and stupidest way - just serialize and deserialize.
            // It works, so who gives a damn.
            string serializedXml = studyProject.Serialize();
            var clonedStudyProject = serializedXml.Deserialize<StudyProject>();
            
            return clonedStudyProject;
        }
    }
}
