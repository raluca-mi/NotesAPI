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
    public class OwnerController: ControllerBase
    {

        static List<Owner> _owners = new List<Owner> { 
        new Owner{ Id=Guid.NewGuid(), Name="Bob"},
        new Owner{ Id=Guid.NewGuid(), Name="Raluca"}};

        /// <summary>
        /// Get all owners
        /// </summary>
        /// <returns>list of owners</returns>
        [HttpGet]
        public IActionResult GetOwner()
        {
            return Ok(_owners);
        }


        /// <summary>
        /// Creates new owner
        /// </summary>
        /// <param name="owner">(Owner) owner</param>
        /// <returns>updated list of owners</returns>
        [HttpPost]
        public IActionResult CreateOwner([FromBody] Owner owner)
        {
            if (owner == null)
                return BadRequest("Owner is null");

            _owners.Add(owner);
            return Ok(_owners);
        }



        /// <summary>
        /// Delete one owner by id
        /// </summary>
        /// <param name="id">(Guid) id of the owner</param>
        /// <returns>Ok if the id is valid, otherwise NotFound </returns>

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            int index = _owners.FindIndex(owner => owner.Id == id);
            if (index == -1)
                return NotFound("The note was not found!");

            _owners.RemoveAt(index);
            return Ok("The owner was deleted");
        }

        /// <summary>
        /// Update one owner by id
        /// </summary>
        /// <param name="id">(Guid) id of the owner</param>
        /// <param name="owner">(Owner) owner to be updated</param>
        /// <returns>List of updated notes if the id is valid, otherwise create new note</returns>

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody] Owner owner)
        {
            if (owner == null)
            {
                return BadRequest("Owner can't be null");
            }

            int index = _owners.FindIndex(note => note.Id == id);

            if (index == -1)
            {
                return CreateOwner(owner);                //if index not found create a new owner
                //return NotFound("Id not found!");     //if index not found respond with NotFound
            }

            owner.Id = id;
            _owners[index] = owner;

            return Ok(_owners);
        }

    }
}
