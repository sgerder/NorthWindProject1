using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using EfEx.Domain;


namespace WebService
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {

        IDataService _dataService;

        public CategoriesController(IDataService dataService)
        {
            _dataService = dataService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var categories =  _dataService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetCategory(int categoryId)
        {
            var category = _dataService.GetCategory(categoryId);

            if (category == null) return NotFound();

            return Ok(category);
        }

        /*
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            return Created("", _dataService.CreateCategory(category.Name, category.Description));
        }
        
        [HttpPut("{categoryId}")]
        public ActionResult UpdateCategory([FromBody] Category category)
        {
            if (!_dataService.UpdateCategory(category.Id, category.Name, category.Description))
                return NotFound();
            
            return Ok();
        }
        */

        [HttpDelete("{categoryId}")]
        public ActionResult DeleteCategory(int categoryId)
        {
            if (!_dataService.DeleteCategory(categoryId))
                return NotFound();

            return Ok();
        }


    }
}