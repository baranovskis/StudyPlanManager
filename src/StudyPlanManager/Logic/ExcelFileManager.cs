using StudyPlanManager.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;
using System.IO;
using System.Collections.Generic;
using System;
using NPOI.XSSF.UserModel;

namespace StudyPlanManager.Logic
{
    public class ExcelFileManager
    {
        private XSSFWorkbook _workbook;

        private ICellStyle _normalCellStyle;
        private ICellStyle _normalCenteredCellStyle;

        private ICellStyle _greyCellStyle;
        private ICellStyle _greyCenteredCellStyle;
        private ICellStyle _greyRightCellStyle;

        public StudyProject StudyProject { get; set; }

        public ExcelFileManager()
        {
            _workbook = new XSSFWorkbook();
            CreateCellStyles();
        }

        private void CreateCellStyles()
        {
            var normalFont = _workbook.CreateFont();
            normalFont.FontHeightInPoints = 12;

            var normalBoldFont = _workbook.CreateFont();
            normalBoldFont.FontHeightInPoints = 12;
            normalBoldFont.IsBold = true;

            _normalCellStyle = _workbook.CreateCellStyle();
            _normalCellStyle.BorderBottom = BorderStyle.Thin;
            _normalCellStyle.BorderLeft = BorderStyle.Thin;
            _normalCellStyle.BorderRight = BorderStyle.Thin;
            _normalCellStyle.BorderTop = BorderStyle.Thin;
            _normalCellStyle.VerticalAlignment = VerticalAlignment.Center;
            _normalCellStyle.SetFont(normalFont);

            _normalCenteredCellStyle = _workbook.CreateCellStyle();
            _normalCenteredCellStyle.CloneStyleFrom(_normalCellStyle);
            _normalCenteredCellStyle.Alignment = HorizontalAlignment.Center;

            _greyCellStyle = _workbook.CreateCellStyle();
            _greyCellStyle.CloneStyleFrom(_normalCellStyle);
            _greyCellStyle.FillPattern = FillPattern.SolidForeground;
            _greyCellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            _greyCellStyle.SetFont(normalBoldFont);

            _greyCenteredCellStyle = _workbook.CreateCellStyle();
            _greyCenteredCellStyle.CloneStyleFrom(_greyCellStyle);
            _greyCenteredCellStyle.Alignment = HorizontalAlignment.Center;

            _greyRightCellStyle = _workbook.CreateCellStyle();
            _greyRightCellStyle.CloneStyleFrom(_greyCellStyle);
            _greyRightCellStyle.Alignment = HorizontalAlignment.Right;
        }

        public MemoryStream GenerateExcelFile()
        {
            MemoryStream stream = null;

            var sheet = _workbook.CreateSheet("Project");

            CreateTitleRow(sheet);

            var totalIndexRanges = new List<Tuple<int, int>>();

            foreach (var studyCourse in StudyProject.Courses)
            {
                int rowIndex = sheet.LastRowNum + 1;
                CreateRowsFromStudyCourse(sheet, studyCourse);

                int currentRowIndex = sheet.LastRowNum;
                var totalIndexRange = new Tuple<int, int>(rowIndex + 1, currentRowIndex);
                totalIndexRanges.Add(totalIndexRange);
            }

            CreateTotalRow(sheet, totalIndexRanges);

            for (int i = 0; i <= 5; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            // Test
            //var file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"Data\Test\test.xlsx");
            //file.Directory.Create();
            //using (var xfile = new FileStream(file.FullName, FileMode.Create, FileAccess.Write))
            //{
            //    _workbook.Write(xfile);
            //    xfile.Close();
            //}

            using (var exportData = new MemoryStream())
            {
                _workbook.Write(exportData);
                stream = exportData;
            }

            return stream;
        }

        private void CreateTitleRow(ISheet sheet)
        {
            var titleRow = sheet.CreateRow(0);
            titleRow.CreateCell(0).SetCellValue("Mācību plāns");
            titleRow.GetCell(0).CellStyle = _greyCenteredCellStyle;

            titleRow.CreateCell(1).CellStyle = _greyCenteredCellStyle;
            titleRow.CreateCell(2).CellStyle = _greyCenteredCellStyle;
            titleRow.CreateCell(3).CellStyle = _greyCenteredCellStyle;
            titleRow.CreateCell(4).CellStyle = _greyCenteredCellStyle;
            titleRow.CreateCell(5).CellStyle = _greyCenteredCellStyle;

            var cellRange = new CellRangeAddress(0, 0, 0, 5);
            sheet.AddMergedRegion(cellRange);
        }

        private void CreateTotalRow(ISheet sheet, List<Tuple<int, int>> totalIndexRanges)
        {
            int rowIndex = sheet.LastRowNum + 1;

            var totalRow = sheet.CreateRow(rowIndex);
            totalRow.CreateCell(0).SetCellValue("KOPĀ");
            totalRow.CreateCell(1);

            totalRow.CreateCell(2);
            totalRow.CreateCell(3);
            totalRow.CreateCell(4);
            totalRow.CreateCell(5);

            totalRow.GetCell(0).CellStyle = _greyRightCellStyle;
            totalRow.GetCell(1).CellStyle = _greyRightCellStyle;

            string totalFormat = String.Empty;

            foreach (var totalIndexRange in totalIndexRanges)
            {
                totalFormat += @"{0}" + (totalIndexRange.Item1 + 1) + ":{0}" + (totalIndexRange.Item2 + 1) + ",";
            }

            totalFormat = totalFormat.Substring(0, totalFormat.Length - 1);
            totalFormat = "SUM(" + totalFormat + ")";

            char columnName = 'C';

            for (int i = 2; i <= 5; i++)
            {
                totalRow.GetCell(i).SetCellType(CellType.Formula);
                totalRow.GetCell(i).SetCellFormula(String.Format(totalFormat, columnName));
                totalRow.GetCell(i).CellStyle = _greyCenteredCellStyle;
                columnName++;
            }

            var cellRange = new CellRangeAddress(rowIndex, rowIndex, 0, 1);
            sheet.AddMergedRegion(cellRange);
        }

        public void CreateRowsFromStudyCourse(ISheet sheet, StudyCourse studyCourse)
        {
            int rowIndex = sheet.LastRowNum + 1;

            var courseRow = sheet.CreateRow(rowIndex);
            courseRow.CreateCell(0).SetCellValue("Mācību joma");
            courseRow.CreateCell(1).SetCellValue(studyCourse.CourseName);
            courseRow.CreateCell(2).SetCellValue("10. klase");
            courseRow.CreateCell(3).SetCellValue("11. klase");
            courseRow.CreateCell(4).SetCellValue("12. klase");
            courseRow.CreateCell(5).SetCellValue("KOPĀ");

            courseRow.GetCell(0).CellStyle = _greyCellStyle;
            courseRow.GetCell(1).CellStyle = _greyCellStyle;
            courseRow.GetCell(2).CellStyle = _greyCenteredCellStyle;
            courseRow.GetCell(3).CellStyle = _greyCenteredCellStyle;
            courseRow.GetCell(4).CellStyle = _greyCenteredCellStyle;
            courseRow.GetCell(5).CellStyle = _greyCenteredCellStyle;

            foreach (var studyGroup in studyCourse.Groups)
            {
                CreateRowsFromStudyGroup(sheet, studyGroup);
            }
        }

        public void CreateRowsFromStudyGroup(ISheet sheet, StudyGroup studyGroup)
        {
            int rowIndex = sheet.LastRowNum + 1;

            foreach (var study in studyGroup.Studies)
            {
                if (study.CreditPoints[0] > 0
                    || study.CreditPoints[1] > 0
                    || study.CreditPoints[2] > 0)
                {
                    CreateRowFromStudy(sheet, study);
                }
            }

            var row = sheet.GetRow(rowIndex);
            row.GetCell(0).SetCellValue(studyGroup.GroupName);

            var cellRange = new CellRangeAddress(rowIndex, sheet.LastRowNum, 0, 0);
            sheet.AddMergedRegion(cellRange);
        }

        public void CreateRowFromStudy(ISheet sheet, Study study)
        {
            int rowIndex = sheet.LastRowNum + 1;

            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("");
            row.CreateCell(1).SetCellValue(study.StudyName);
            row.CreateCell(2).SetCellValue(study.CreditPoints[0]);
            row.CreateCell(3).SetCellValue(study.CreditPoints[1]);
            row.CreateCell(4).SetCellValue(study.CreditPoints[2]);

            var totalCell = row.CreateCell(5);
            totalCell.SetCellType(CellType.Formula);
            totalCell.SetCellFormula($"SUM(C{rowIndex + 1}:E{rowIndex + 1})");

            row.GetCell(0).CellStyle = _normalCellStyle;
            row.GetCell(1).CellStyle = _normalCellStyle;
            row.GetCell(2).CellStyle = _normalCenteredCellStyle;
            row.GetCell(3).CellStyle = _normalCenteredCellStyle;
            row.GetCell(4).CellStyle = _normalCenteredCellStyle;
            row.GetCell(5).CellStyle = _normalCenteredCellStyle;
        }
    }
}