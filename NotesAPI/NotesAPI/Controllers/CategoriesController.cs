using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        static List<Category> _categories = new List<Category> {
        new Category { Id = "1",Name= "DONE" },
        new Category { Id = "2",Name= "TODO" },
        new Category { Id = "3",Name= "DOING" },
        };

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>Returns list of notes</returns>
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_categories);
        }

        /// <summary>
        /// Gets one category
        /// </summary>
        /// <param name="id">Has one string parameter -the id of the category</param>
        /// <returns>Returns one category</returns>
        /// 
        [HttpGet("{id}")]
        public IActionResult GetOneCategory(string id)
        {
            foreach (var categ in _categories)
            {
                if (categ.Id == id)
                    return Ok(categ);
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Category does not exist");

        }


        /// <summary>
        /// Add a new category
        /// </summary>
        /// <param name="categ">Has one parameter of type Category</param>
        /// <returns>Returns all categories</returns>
        [HttpPost]
        public IActionResult Post([FromBody] Category categ)
        {
            _categories.Add(categ);
            return Ok(_categories);
        }


        /// <summary>
        /// Delete one category
        /// </summary>
        /// <param name="id">Has one string parameter -the id of the category</param>
        /// <returns>Returns the list of categories and status code 200 if the id is valid, otherwise BadRequest </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            foreach(var categ in _categories)
            {
                if(categ.Id==id)
                {
                    _categories.Remove(categ);
                    return Ok(_categories);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Category does not exist");
        }
    }
}