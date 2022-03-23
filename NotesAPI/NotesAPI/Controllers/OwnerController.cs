using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models;
using NotesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnerController: ControllerBase
    {
        IOwnerCollectionService _ownerCollectionService;
        public OwnerController(IOwnerCollectionService ownerCollectionService)
        {
            _ownerCollectionService = ownerCollectionService ?? throw new ArgumentNullException(nameof(ownerCollectionService));
        }


        ///// <summary>
        ///// Get all owners
        ///// </summary>
        ///// <returns>list of owners</returns>
        //[HttpGet]
        //public IActionResult GetOwners()
        //{
        //    return Ok(_ownerCollectionService.GetAll());
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetOwnerById(Guid id)
        //{
        //    var owner = _ownerCollectionService.Get(id);

        //    if (owner == null)
        //        return NotFound();
        //    return Ok(owner);

        //}

        ///// <summary>
        ///// Creates new owner
        ///// </summary>
        ///// <param name="owner">(Owner) owner</param>
        ///// <returns>updated list of owners</returns>
        //[HttpPost]
        //public IActionResult CreateOwner([FromBody] Owner owner)
        //{

        //    if (owner == null)
        //        return BadRequest("Owner is null");

        //    return Ok(_ownerCollectionService.Create(owner));

        //}


        ///// <summary>
        ///// Update one owner by id
        ///// </summary>
        ///// <param name="id">(Guid) id of the owner</param>
        ///// <param name="owner">(Owner) owner to be updated</param>
        ///// <returns>List of updated notes if the id is valid, otherwise create new note</returns>

        //[HttpPut("{id}")]
        //public IActionResult UpdateOwner(Guid id, [FromBody] Owner owner)
        //{

        //    if (owner == null)
        //    {
        //        return BadRequest("Owner can't be null");
        //    }

        //    if (_ownerCollectionService.Update(id, owner) == true)
        //        return Ok();
        //    return NotFound();

        //}


        ///// <summary>
        ///// Delete one owner by id
        ///// </summary>
        ///// <param name="id">(Guid) id of the owner</param>
        ///// <returns>Ok if the id is valid, otherwise NotFound </returns>

        //[HttpDelete("{id}")]
        //public IActionResult DeleteOwner(Guid id)
        //{

        //    if (_ownerCollectionService.Delete(id) == false)
        //        return NotFound("The owner was not found!");
        //    else
        //        return Ok("The owner has been deleted!");
        //}

    }
}
