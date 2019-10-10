using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AnagramLibrary;
using System;
using Microsoft.Extensions.Configuration;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnagramController : ControllerBase
    {

        private readonly ILogger<AnagramController> _logger;
        private IAnagramLibrary reader;
        private IConfiguration configuration;

        public AnagramController(ILogger<AnagramController> logger, IAnagramLibrary reader, IConfiguration configuration)
        {
            _logger = logger;
            this.reader = reader;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("/checkAnagram")]
        public IActionResult CheckAnagram([FromBody] Anagram a)
        {
            reader.ReadFile(configuration["dictonaryFileName"]);
            if (AnagramLibrary.AnagramLibrary.CheckToWords(a.W1, a.W2))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("/getKnownAnagrams")]
        public IActionResult GetKnownAnagrams([FromQuery] string w)
        {
            Console.WriteLine("" + w);
            reader.ReadFile(configuration["dictonaryFileName"]);
            var list = AnagramLibrary.AnagramLibrary.findAnagrams(w);
            if (list != null)
            {
                return Ok(list);
            }
            _logger.LogError("No anagram found for " + w);
            return NotFound();
        }

        [HttpGet]
        [Route("/getPermutations")]
        public IActionResult GetPermutations([FromQuery] string w)
        {
            return Ok(AnagramLibrary.AnagramLibrary.getPermutations(w));
        }
    }
}
