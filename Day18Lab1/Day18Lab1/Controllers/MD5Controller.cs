using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Day18Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MD5Controller : ControllerBase
    {
        protected readonly MD5 md5 = MD5.Create();
        public MD5Controller() { }

        protected string Compute(string PlainText)
        {
            byte[] HashCode = md5.ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(PlainText));
            return BitConverter.ToString(HashCode);
        }
        [HttpGet]
        public IActionResult Get(string plainText)
        {
            return Ok(Compute(plainText));
        }

        [HttpPost]
        public IActionResult Validate(string PlainText, string HexText)
        {
            if(HexText.Equals(Compute(PlainText)))
            {
                return Ok(true);
            } else
            {
                return Ok(false);
            }
            //return BadRequest();
        }
    }

    
}
