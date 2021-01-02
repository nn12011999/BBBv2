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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryRepository categoryRepository,
            ICategoryServices categoryServices)
        {
            _categoryRepository = categoryRepository;
            _categoryServices = categoryServices;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllCategory()
        {
            try
            {
                var response = _categoryRepository.GetAllCategory();
                if (response == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Can not get category."
                    });
                };
                foreach (var item in response)
                {
                    if (item != null && item.ParentCategory != null)
                    {
                        item.ParentCategory.ParentCategory = null;
                    }
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

        [HttpPost("add-category")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.Admin)]
        public IActionResult AddCategory([FromBody] AddCategoryRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Please provide input information correctly."
                    });
                }

                if (request.ParentId != null)
                {
                    var parent = _categoryRepository.FindById((int)request.ParentId);
                    if (parent == null)
                    {
                        return BadRequest(new ErrorViewModel
                        {
                            ErrorCode = "400",
                            ErrorMessage = "Parent category not found"
                        });
                    }
                }

                var categoryQuery = _categoryRepository.FindByName(request.CategoryName);
                if (categoryQuery != null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Category have been create"
                    });
                }

                var category = new Category()
                {
                    Name = request.CategoryName,
                    ParentId = request.ParentId,
                };

                var response = _categoryServices.AddCategory(category);
                if (response != "OK")
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Can not execute. Plz contact admin"
                    });
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

        [HttpPost("delete-category")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.Admin)]
        public IActionResult DeleteCategory([FromBody] DeleteCategoryRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Please provide input information correctly."
                    });
                }

                if (request.CategoryId <= 0)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Category not found"
                    });
                }

                var category = _categoryRepository.FindById(request.CategoryId);
                if (category == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Category not found"
                    });
                }

                var response = _categoryServices.DeleteCategory(category);
                if (response != "OK")
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Can not execute. Plz contact admin"
                    });
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

        [HttpPost("update-category")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.Admin)]
        public IActionResult UpdateCategory([FromBody] UpdateCategoryRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Please provide input information correctly."
                    });
                }

                var category = _categoryRepository.FindById(request.CategoryId);
                if (category == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Category not found"
                    });
                }

                if (request.ParentId != null)
                {
                    var parent = _categoryRepository.FindById(request.ParentId.GetValueOrDefault());
                    if (parent == null)
                    {
                        return BadRequest(new ErrorViewModel
                        {
                            ErrorCode = "400",
                            ErrorMessage = "Parent category not found"
                        });
                    }
                }

                category.ParentId = request.ParentId;
                category.Name = request.CategoryName;

                var response = _categoryServices.UpdateCategory(category);
                if (response != "OK")
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Can not execute. Plz contact admin"
                    });
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

        [HttpGet("get-by-id")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var response = _categoryRepository.FindById(id);
                if (response == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Category not found"
                    });
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

        [HttpGet("get-by-url")]
        public IActionResult GetCategoryByUrl(string url)
        {
            try
            {
                var response = _categoryRepository.FindByUrl(url);
                if (response == null)
                {
                    return BadRequest(new ErrorViewModel
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Category not found"
                    });
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

        [HttpPost("check")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RoleDefine.Admin)]
        public IActionResult CheckToken()
        {
            try
            {
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
    }
}
