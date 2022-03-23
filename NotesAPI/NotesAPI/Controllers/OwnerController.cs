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

        /// <summary>
        /// Get all owners
        /// </summary>
        /// <returns>list of owners</returns>
        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            return Ok(await _ownerCollectionService.GetAll());
        }

        /// <summary>
        /// Get owner by id
        /// </summary>
        /// <param name="id">(Guid) owner id</param>
        /// <returns>Searched owner</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            var owner = await _ownerCollectionService.Get(id);

            if (owner == null)
                return NotFound();
            return Ok(owner);
        }

        /// <summary>
        /// Creates new owner
        /// </summary>
        /// <param name="owner">(Owner) owner</param>
        /// <returns>updated list of owners</returns>
       
        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody] Owner owner)
        {
            if (owner == null)
                return BadRequest("Owner is null");

            return Ok(await _ownerCollectionService.Create(owner));
        }

        /// <summary>
        /// Update one owner by id
        /// </summary>
        /// <param name="id">(Guid) id of the owner</param>
        /// <param name="owner">(Owner) owner to be updated</param>
        /// <returns>List of updated notes if the id is valid, otherwise create new note</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody] Owner owner)
        {
            if (owner == null)
            {
                return BadRequest("Owner can't be null");
            }

            if (await _ownerCollectionService.Update(id, owner) == true)
                return Ok();
            return NotFound();
        }


        /// <summary>
        /// Delete one owner by id
        /// </summary>
        /// <param name="id">(Guid) id of the owner</param>
        /// <returns>Ok if the id is valid, otherwise NotFound </returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {
            if (await _ownerCollectionService.Delete(id) == false)
                return NotFound("The owner was not found!");
            else
                return Ok("The owner has been deleted!");
        }
    }
}
