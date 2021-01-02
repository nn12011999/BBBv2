using BBB.Data.DataModel.Request;
using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using BBB.Main.Repositories;
using BBB.Main.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileSaveServices _fileSaveServices;
        private readonly IFileSaveRepository _fileSaveRepository;
        public FileController(IFileSaveServices fileSaveServices,
            IFileSaveRepository fileSaveRepository)
        {
            _fileSaveServices = fileSaveServices;
            _fileSaveRepository = fileSaveRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file,
                                                            [FromForm] string title,
                                                            [FromForm] int CategoryId)
        {

            if (file.Length > 0 && file.ContentType.Contains("video"))
            {
                try
                {
                    FileSave response = null;
                    using (var ms = new MemoryStream())
                    {
                        FileSave f = new FileSave();
                        file.CopyTo(ms);
                        f.FileName = file.FileName;
                        f.FileType = file.ContentType;
                        f.FileData = ms.ToArray();
                        f.Title = title;
                        f.CategoryId = CategoryId;
                        var result = await _fileSaveServices.AddFileSave(f);
                        if (result != "OK")
                        {
                            return BadRequest("Type format is not a video. Plz contact admin");
                        }
                        response = f;
                        response.FileData = null;
                    }
                    return Ok(response);
                }
                catch (Exception e)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = $"Server Error: {e.Message}"
                    });
                    }
                }
            else
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Type format is not a video. Plz contact admin"
                });
            }
        }

        [HttpPost("delete-video")]
        public IActionResult DeleteVideoById([FromBody] RequestById request)
        {
            try
            {
                var response = _fileSaveRepository.GetAllWithOutData();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = $"Server Error: {e.Message}"
                });
            }
        }

        [HttpGet("get-all-video")]
        public IActionResult GetVideoAll()
        {
            try
            {
                var response = _fileSaveRepository.GetAllWithOutData();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = $"Server Error: {e.Message}"
                });
            }
        }

        [HttpGet("get-video-by-id")]
        public IActionResult GetVideoById(int id)
        {
            try
            {
                var response = _fileSaveRepository.GetById(id);
                if (response == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Video not found"
                    });
                }
                return File(response.FileData, response.FileType);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = $"Server Error: {e.Message}"
                });
            }
        }

        [HttpGet("get-video-by-url")]
        public IActionResult GetVideoByUrl(string url)
        {
            try
            {
                var response = _fileSaveRepository.GetByUrl(url);
                if (response == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Video not found"
                    });
                }
                return File(response.FileData, response.FileType);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = $"Server Error: {e.Message}"
                });
            }
        }

        [HttpGet("get-video-by-url-without-data")]
        public IActionResult GetVideoByUrlWithoutData(string url)
        {
            try
            {
                var response = _fileSaveRepository.GetByUrlWithOutData(url);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = $"Server Error: {e.Message}"
                });
            }
        }

        [HttpGet("get-by-category")]
        public IActionResult GetByCategory(string url)
        {
            try
            {
                var response = _fileSaveRepository.GetByCategoryUrl(url);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = $"Server Error: {e.Message}"
                });
            }
        }
    }
}