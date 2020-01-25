﻿using StudyPlanManager.Logic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace StudyPlanManager.Controllers
{
    public class ExportController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetExcel(string id)
        {
            var studyProject = StudyManager.Instance.GetStudyProject(id);

            HttpResponseMessage response;

            if (studyProject != null)
            {
                var excelFile = new ExcelFileManager();
                excelFile.StudyProject = studyProject;

                var stream = excelFile.GenerateExcelFile();

                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "export.xlsx";

                // Media types
                // https://stackoverflow.com/questions/4212861/what-is-a-correct-mime-type-for-docx-pptx-etc
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return response;
        }
    }
}