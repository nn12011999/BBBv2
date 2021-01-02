using BBB.Data.DataModel.Request;
using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using BBB.Main.Repositories;
using BBB.Main.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BBB.Main.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentServices _commentServices;

        public CommentController(ICommentServices commentServices,
            ICommentRepository commentRepository,
            IPostRepository postRepository,
            IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _commentServices = commentServices;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpPost("add-comment")]
        public IActionResult AddComment([FromBody] AddCommentRequest request)
        {
            try
            {
                var post = _postRepository.FindById(request.PostId);
                if(post == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Post not found"
                    });
                }

                var user = _userRepository.FindById(request.UserId);
                if (post == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "User not found"
                    });
                }

                var comment = new Comment
                {
                    Context = request.Context,
                    UserId = request.UserId,
                    PostId = request.PostId,
                    TimeStamp = DateTime.UtcNow
                };
                var response = _commentServices.AddComment(comment);
                if (response != "OK")
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Can not execute. Plz contact admin"
                    });
                }
                return Ok(comment);
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

        [HttpPost("update-comment")]
        public IActionResult UpdateComment([FromBody] UpdateCommentRequest request)
        {
            try
            {
                var comment = _commentRepository.GetById(request.CommentId);
                if (comment == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Comment not found"
                    });
                }

                var response = _commentServices.UpdateComment(comment);
                if (response != "OK")
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Can not execute. Plz contact admin"
                    });
                }
                return Ok(comment);
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

        [HttpPost("delete-comment")]
        public IActionResult DeleteComment([FromBody] RequestById request)
        {
            try
            {
                var comment = _commentRepository.GetById(request.Id);
                if (comment == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Comment not found"
                    });
                }

                var response = _commentServices.DeleteComment(comment);
                if (response != "OK")
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Can not execute. Plz contact admin"
                    });
                }
                return Ok(comment);
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
