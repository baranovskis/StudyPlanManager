using StudyPlanManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanManager.Logic
{
    public class StudyRuleManager
    {
        public static int CreditPointMinimum = 32;
        public static int CreditPointMaximum = 36;

        public static List<StudyInfoMessage> CheckStudyProject(StudyProject studyProject)
        {
            var messages = new List<StudyInfoMessage>();

            if (studyProject != null)
            {
                var totals = new int[3];
                bool obligatoryStudiesNotFilled = false;
                bool minimalStudyCountNotMet = false;
                bool tooManyCreditsInStudy = false;
                bool parentStudiesNotFilledIn = false;

                foreach (var course in studyProject.Courses)
                {
                    foreach(var group in course.Groups)
                    {
                        int countByGroup = 0;

                        foreach(var study in group.Studies)
                        {
                            totals[0] += study.CreditPoints[0];
                            totals[1] += study.CreditPoints[1];
                            totals[2] += study.CreditPoints[2];

                            if (study.CreditPoints[0] == 0
                                && study.CreditPoints[1] == 0
                                && study.CreditPoints[2] == 0
                                )
                            {
                                if (study.IsObligatory)
                                {
                                    obligatoryStudiesNotFilled = true;

                                    messages.Add(
                                        new StudyInfoMessage
                                        {
                                            SeverityLevel = SeverityLevel.Warning,
                                            TreeId = study.TreeId
                                        });
                                }
                            }
                            else
                            {
                                countByGroup++;

                                // Check parent studies not filled in.
                                if (!String.IsNullOrEmpty(study.ParentTreeId))
                                {
                                    var parentStudy = StudyManager.Instance.GetStudy(studyProject, study.ParentTreeId);

                                    if (parentStudy != null
                                        && parentStudy.CreditPoints[0] == 0
                                        && parentStudy.CreditPoints[1] == 0
                                        && parentStudy.CreditPoints[2] == 0)
                                    {
                                        parentStudiesNotFilledIn = true;

                                        messages.Add(
                                            new StudyInfoMessage
                                            {
                                                SeverityLevel = SeverityLevel.Error,
                                                TreeId = study.TreeId
                                            });

                                        messages.Add(
                                            new StudyInfoMessage
                                            {
                                                SeverityLevel = SeverityLevel.Error,
                                                TreeId = parentStudy.TreeId
                                            });
                                    }
                                }

                                // Check credit point total in all years.
                                if (study.CreditPointLimit > 0
                                    && study.CreditPoints[0]
                                        + study.CreditPoints[1]
                                        + study.CreditPoints[2] > study.CreditPointLimit)
                                {
                                    tooManyCreditsInStudy = true;

                                    messages.Add(
                                        new StudyInfoMessage
                                        {
                                            SeverityLevel = SeverityLevel.Error,
                                            TreeId = study.TreeId
                                        });
                                }
                            }
                        }

                        // Check for minimal study count in group.
                        if (group.MinimalStudyCount > countByGroup)
                        {
                            minimalStudyCountNotMet = true;
                            messages.Add(
                                new StudyInfoMessage
                                {
                                    SeverityLevel = SeverityLevel.Warning,
                                    TreeId = group.TreeId
                                });
                        }
                    }
                }

                // Check totals by each class - must be between CreditPointMinimum and CreditPointMaximum.
                if (totals[0] < CreditPointMinimum
                    || totals[1] < CreditPointMinimum
                    || totals[2] < CreditPointMinimum)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Error,
                            Message = "There are not enough credit points in some study years"
                        });
                }

                if (totals[0] > CreditPointMaximum
                    || totals[1] > CreditPointMaximum
                    || totals[2] > CreditPointMaximum)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Error,
                            Message = "There are too many credit points in some study years"
                        });
                }

                // Check for obligatory studies not filled in.
                if (obligatoryStudiesNotFilled)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Warning,
                            Message = "Some obligatory studies are not filled in"
                        });
                }

                // Check for minimal study count in group.
                if (minimalStudyCountNotMet)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Warning,
                            Message = "Some groups dont have minimal required study count filled in"
                        });
                }

                // Check credit point total in all years.
                if (tooManyCreditsInStudy)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Error,
                            Message = "Some studies have too many credit points"
                        });
                }

                // Check parent studies not filled in.
                if (parentStudiesNotFilledIn)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Error,
                            Message = "Some studies have parent studies not filled in"
                        });
                }
            }

            return messages;
        }
    }
}
