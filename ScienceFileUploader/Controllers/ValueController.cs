using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScienceFileUploader.Service.Interface;

namespace ScienceFileUploader.Controllers
{
    [Controller]
    [Route("science/values/")]
    public class ValueController : Controller
    {
        private readonly IValueService _valueService;
    
        public ValueController(IValueService valueService) 
        { 
            _valueService = valueService;
        }
        
        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetAll([FromRoute] string fileName)
        {
            return Ok(await _valueService.GetAllByFileNameAsync(fileName));
        }
    }
}