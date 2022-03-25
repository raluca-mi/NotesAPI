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
        /// <returns>List of owners</returns>
        [HttpGet]
        public async Task<IActionResult> GetOwnersAsync()
        {
            return Ok(await _ownerCollectionService.GetAllAsync());
        }

        /// <summary>
        /// Get owner by id
        /// </summary>
        /// <param name="id">(Guid) owner id</param>
        /// <returns>Searched owner</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerByIdAsync(Guid id)
        {
            var owner = await _ownerCollectionService.GetAsync(id);

            if (owner == null)
                return NotFound();
            return Ok(owner);
        }

        /// <summary>
        /// Create new owner
        /// </summary>
        /// <param name="owner">(Owner) owner</param>
        /// <returns>Updated list of owners</returns>
       
        [HttpPost]
        public async Task<IActionResult> CreateOwnerAsync([FromBody] Owner owner)
        {
            if (owner == null)
                return BadRequest("Owner is null");

            return Ok(await _ownerCollectionService.CreateAsync(owner));
        }

        /// <summary>
        /// Update one owner by id
        /// </summary>
        /// <param name="id">(Guid) id of the owner</param>
        /// <param name="owner">(Owner) owner to be updated</param>
        /// <returns>List of updated notes if the id is valid, otherwise create new note</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwnerAsync(Guid id, [FromBody] Owner owner)
        {
            if (owner == null)
            {
                return BadRequest("Owner is null");
            }

            if (await _ownerCollectionService.UpdateAsync(id, owner) == true)
                return Ok();
            return NotFound();
        }


        /// <summary>
        /// Delete one owner by id
        /// </summary>
        /// <param name="id">(Guid) id of the owner</param>
        /// <returns>Ok if the id is valid, otherwise NotFound </returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwnerAsync(Guid id)
        {
            if (await _ownerCollectionService.DeleteAsync(id) == false)
                return NotFound("Owner not found!");
            else
                return Ok();
        }
    }
}
