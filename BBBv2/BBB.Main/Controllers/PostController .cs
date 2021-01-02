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
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostServices _postServices;
        private readonly ICategoryRepository _categoryRepository;
        public PostController(IPostRepository PostRepository,
            IPostServices PostServices,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository)
        {
            _postRepository = PostRepository;
            _postServices = PostServices;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllPost()
        {
            return Ok(_postRepository.GetAllPost());
        }

        [HttpPost("add-post")]
        public IActionResult AddPost([FromBody] AddPostRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }


            var PostQuery = _postRepository.FindByTitle(request.Title);
            if (PostQuery != null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post have been create"
                });
            }

            var User = _userRepository.FindById(request.UserId);
            if (User == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "User not found"
                });
            }

            var Category = _categoryRepository.FindById(request.CategoryId);
            if (Category == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Category not found"
                });
            }

            var Post = new Post()
            {
                Title = request.Title,
                Context = request.Context,
                TimeStamp = DateTime.UtcNow,
                CategoryId = request.CategoryId,
                UserId = request.UserId
            };

            var response = _postServices.AddPost(Post);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
        }

        [HttpPost("delete-Post")]
        public IActionResult DeletePost([FromBody] DeletePostRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.PostId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
                });
            }

            var Post = _postRepository.FindById(request.PostId);
            if (Post == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
                });
            }

            var response = _postServices.DeletePost(Post);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
        }

        [HttpPost("update-Post")]
        public IActionResult UpdatePost([FromBody] UpdatePostRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.PostId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
                });
            }

            var Post = _postRepository.FindById(request.PostId);
            if (Post == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Post not found"
                });
            }


            var User = _userRepository.FindById(request.UserId);
            if (User == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "User not found"
                });
            }

            var Category = _categoryRepository.FindById(request.CategoryId);
            if (Category == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Category not found"
                });
            }

            
            Post.Title = request.Title;
            Post.Context = request.Context;
            Post.TimeStamp = DateTime.UtcNow;
            Post.CategoryId = request.CategoryId;
            Post.UserId = request.UserId;

            //var response = _postServices.UpdatePost(Post, tags);
            //if (response != "OK")
            //{
            //    return BadRequest("Can not execute. Plz contact admin");
            //}
            //return Ok(response);
            return Ok();
        }

        [HttpGet("get-by-id")]
        public IActionResult GetPostById(int Id)
        {
            var response = _postRepository.FindById(Id);
            if (response == null)
            {
                return BadRequest("Post not found");
            }
            return Ok(response);
        }

        [HttpGet("get-by-url")]
        public IActionResult GetPostByUrl(string url)
        {
            var response = _postRepository.FindByUrl(url);
            if (response == null)
            {
                return BadRequest("Post not found");
            }
            return Ok(response);
        }

        
    }
}