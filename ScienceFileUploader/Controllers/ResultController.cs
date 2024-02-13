using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScienceFileUploader.Dto;
using ScienceFileUploader.Service.Interface;

namespace ScienceFileUploader.Controllers
{
    [Controller]
    [Route("science/results/")]
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;
    
        public ResultController(IResultService resultService) 
        { 
            _resultService = resultService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ResultQueryParams parameters)
        {
            if (parameters is {FileName : not null,  MinTime: null, MaxTime: null, MinValue: null, MaxValue: null})
                return Ok(await _resultService.GetByFileNameAsync(parameters.FileName));
            if (parameters is {FileName : null, MinTime: not null, MaxTime: not null, MinValue: null, MaxValue: null })
                return Ok(await _resultService.GetAllByTimeAsync(parameters.MinTime.Value, parameters.MaxTime.Value));
            if (parameters is {FileName : null,MinTime: null, MaxTime: null, MinValue: not null, MaxValue: not null })
                return Ok(await _resultService.GetAllByParametersAsync(parameters.MinValue.Value, parameters.MaxValue.Value));
            if (parameters is {FileName : null, MinTime: null, MaxTime: null, MinValue: null, MaxValue: null })
                return Ok(await _resultService.GetAllAsync());
            return BadRequest("Invalid Parameters");
        }
    }
}