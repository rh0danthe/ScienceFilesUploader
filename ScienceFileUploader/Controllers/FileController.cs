using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScienceFileUploader.Dto;
using ScienceFileUploader.Service.Interface;

namespace ScienceFileUploader.Controllers
{
    [Controller]
    [Route("science/files/")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
    
        public FileController(IFileService fileService) 
        { 
            _fileService = fileService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!string.Equals(extension, ".csv", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Choose file with CSV extension.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _fileService.AddFileAsync(await MapToRequest(file)));
        }
        
        private async Task<FileRequest> MapToRequest(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return new FileRequest
            {
                Content = memoryStream.ToArray(),
                Name = file.FileName
            };
        }
    }
}