using Microsoft.AspNetCore.Mvc;

namespace Day14Lab1.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadFile()
        {
            if(Request.Form.Files.Count > 0)
            {
                foreach(var file in Request.Form.Files)
                {
                    var fileName = file.FileName;
                    using(var outputFile = System.IO.File.Create($"UploadFiles/{fileName}"))
                    {
                        file.CopyTo(outputFile);
                    }
                }
            }
            return Ok("Saved!");
        }
    }
}
