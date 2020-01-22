﻿using StudyPlanManager.Models;
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
                int totalOfClass10 = 0;
                int totalOfClass11 = 0;
                int totalOfClass12 = 0;

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
                            totalOfClass10 += study.CreditPoints[0];
                            totalOfClass11 += study.CreditPoints[1];
                            totalOfClass12 += study.CreditPoints[2];

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
                if (totalOfClass10 < CreditPointMinimum
                    || totalOfClass11 < CreditPointMinimum
                    || totalOfClass12 < CreditPointMinimum)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Error,
                            Message = "errors.notEnoughCreditPointsInYear"
                        });
                }

                if (totalOfClass10 > CreditPointMaximum
                    || totalOfClass11 > CreditPointMaximum
                    || totalOfClass12 > CreditPointMaximum)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Error,
                            Message = "errors.tooManyCreditPointsInYear"
                        });
                }

                // Check for obligatory studies not filled in.
                if (obligatoryStudiesNotFilled)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Warning,
                            Message = "errors.obligatoryStudiesNotFilled"
                        });
                }

                // Check for minimal study count in group.
                if (minimalStudyCountNotMet)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Warning,
                            Message = "errors.groupMinimalStudyCountNotMet"
                        });
                }

                // Check credit point total in all years.
                if (tooManyCreditsInStudy)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Error,
                            Message = "errors.tooManyCreditPointsInStudy"
                        });
                }

                // Check parent studies not filled in.
                if (parentStudiesNotFilledIn)
                {
                    messages.Add(
                        new StudyInfoMessage
                        {
                            SeverityLevel = SeverityLevel.Error,
                            Message = "errors.parentStudiesNotFilled"
                        });
                }
            }

            return messages;
        }
    }
}
