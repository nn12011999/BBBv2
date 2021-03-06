﻿using BBB.Data.DataModel.Request;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.UserAndAdmin)]
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
                return Ok();
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.UserAndAdmin)]
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
                if (comment.UserId != request.UserId)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "User can't change this comment"
                    });
                }
                comment.Context = request.Context;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.UserAndAdmin)]
        public IActionResult DeleteComment([FromBody] DeleteCommentRequest request)
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
                if (comment.UserId != request.UserId)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "User can't change this comment"
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

        [HttpGet("get-comment-of-post-by-id")]
        public IActionResult GetCommentOfPostByPostId(int id)
        {
            try
            {
                var response = _commentRepository.GetByPostId(id);
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

        [HttpGet("get-comment-of-post-by-url")]
        public IActionResult GetCommentOfPostByPostUrl(string Url)
        {
            try
            {
                var response = _commentRepository.GetByPostUrl(Url);
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
