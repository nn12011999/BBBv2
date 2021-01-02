using BBB.Data.DataModel.Request;
using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using BBB.Main.Repositories;
using BBB.Main.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBB.Main.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentServices _commentServices;

        public CommentController(ICommentServices commentServices,
            ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _commentServices = commentServices;
        }

        //[HttpPost("add-comment")]
        //public IActionResult DeleteVideoById([FromBody] RequestById request)
        //{
        //    try
        //    {
        //        var response = _fileSaveRepository.GetAllWithOutData();
        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new ErrorViewModel
        //        {
        //            ErrorCode = "400",
        //            ErrorMessage = $"Server Error: {e.Message}"
        //        });
        //    }
        //}
    }
}
