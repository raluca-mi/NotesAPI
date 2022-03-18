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
            _owners.Add(owner);
            return Ok(_owners);
        }
    }
}
