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

        [HttpGet]
        public IActionResult GetFile(string fileName)
        {
            string fullname = $"UploadFiles/{fileName}";
            if(!System.IO.File.Exists(fullname))
            {
                return NotFound();
            }
            var rawData = System.IO.File.ReadAllBytes(fullname);
            //we FIX this is an IMAGE
            return File(rawData, "image/jpg");
        }
    }
}
