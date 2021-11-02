using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using EfEx.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class  CategoriesController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public CategoriesController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var categories =  _dataService.GetCategories();
            
            return Ok(categories.ToArray());
        }

        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetCategory(int categoryId)
        {
            var category = _dataService.GetCategory(categoryId);

            if (category == null) return NotFound();
            var model = CreateCategoryViewModel(category);
            return Ok(model);
        }
        
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            return Created("", _dataService.CreateCategory(category.CategoryName, category.CategoryDescription));
        }
        
        [HttpPut("{categoryId}")]
        public ActionResult UpdateCategory([FromBody] Category category)
        {
            if (!_dataService.UpdateCategory(category.CategoryId, category.CategoryName, category.CategoryDescription))
                return NotFound();
            
            return Ok();
        }
        

        [HttpDelete("{categoryId}")]
        public ActionResult DeleteCategory(int categoryId)
        {
            if (!_dataService.DeleteCategory(categoryId))
                return NotFound();

            return Ok();
        }

        private CategoryViewModel CreateCategoryViewModel(Category category)
        {
            var model = _mapper.Map<CategoryViewModel>(category);
            model.Url = GetUrl(category);
            return model;
        }
        private string GetUrl(Category category)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { category.CategoryId });
        }

    }
}