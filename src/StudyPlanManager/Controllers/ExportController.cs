using StudyPlanManager.Logic;
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
        public IHttpActionResult GetExcel(string id)
        {
            var studyProject = StudyManager.Instance.GetStudyProject(id);

            if (studyProject == null)
                return NotFound();

            var excelFile = new ExcelFileManager
            {
                StudyProject = studyProject
            };

            var data = excelFile.GenerateExcelFile();

            if (data == null)
                return InternalServerError();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(new MemoryStream(data))
            };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "export.xlsx";

            // Media types
            // https://stackoverflow.com/questions/4212861/what-is-a-correct-mime-type-for-docx-pptx-etc
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            return ResponseMessage(response);
        }
    }
}