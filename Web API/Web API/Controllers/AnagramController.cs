using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClassLibrary;
using System;
using Microsoft.Extensions.Configuration;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnagramController : ControllerBase
    {

        private readonly ILogger<AnagramController> _logger;
        private IClass1 reader;
        private IConfiguration configuration;

        public AnagramController(ILogger<AnagramController> logger, IClass1 reader, IConfiguration configuration)
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
            if (Class1.CheckToWords(a.W1, a.W2))
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
            var list = Class1.findAnagrams(w);
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
            return Ok(Class1.getPermutations(w));
        }
    }
}
