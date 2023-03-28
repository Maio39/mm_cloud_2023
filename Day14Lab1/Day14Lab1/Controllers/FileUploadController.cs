using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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
            Bitmap bitmap = new Bitmap(fullname);
            Graphics graphic = Graphics.FromImage(bitmap);

            string myWater = $"MMLogo {DateTime.Now}";
            Font font = new Font(FontFamily.GenericSansSerif,20,FontStyle.Bold);
            Size l_w = graphic.MeasureString(myWater, font).ToSize();
            Point p = new Point(10,20);
            Point p1 = new Point(11, 21);

            //Background
            graphic.FillRectangle(new SolidBrush(Color.Aquamarine), new Rectangle(p, l_w));
            //Subpixeling
            graphic.DrawString(myWater, font, Brushes.Black, p1);
            //
            graphic.DrawString(myWater, font, Brushes.White, p);

            using(MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
                return File(ms.ToArray(),"image/png");
            }

            /*/we FIX this is an IMAGE
            return File(rawData, "image/jpg");
            //*/
        }
    }
}
