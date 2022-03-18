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
    public class NotesController : ControllerBase
    {
        static List<Note> _notes = new List<Note> { 
        new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "First Note", Description = "First Note Description" },
        new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "Second Note", Description = "Second Note Description" },
        new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "Third Note", Description = "Third Note Description" },
        new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Note { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = Guid.NewGuid(), Title = "Fifth Note", Description = "Fifth Note Description" }
        };

        /// <summary>
        /// Get all notes
        /// </summary>
        /// <returns>Returns list of notes</returns>
        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notes);
        }

        /// <summary>
        /// Create new note
        /// </summary>
        /// <param name="note">Gets from body a note of type Note </param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateNote([FromBody] Note note)
        {
            _notes.Add(note);
            return Ok(_notes);
        }


        //From other course

        /* [HttpGet]
         public IActionResult Get([FromHeader(Name = "User-Agent")] string UserAgent)
         {
             return Ok(UserAgent);
         }
        */

    }
}
