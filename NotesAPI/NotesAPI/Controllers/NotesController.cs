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
        /// <returns>list of notes</returns>
        
        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notes);
        }

        /// <summary>
        /// Create new note
        /// </summary>
        /// <param name="note">(Note) note</param>
        /// <returns>updated list of notes</returns>
        
        [HttpPost]
        public IActionResult CreateNote([FromBody] Note note)
        {
            _notes.Add(note);
            //return Ok(_notes);
            return CreatedAtRoute("GetNoteById", new { id = note.Id }, note);
        }

        /// <summary>
        /// Gets one note by owner id
        /// </summary>
        /// <param name="ownerId">(Guid) id of the owner</param>
        /// <returns>one note</returns>
         
        [HttpGet("owner/{ownerId}")]
        public IActionResult GetNoteByOwner(Guid ownerId)
        {
            foreach (var note in _notes)
            {
                if (note.OwnerId == ownerId)
                    return Ok(note);
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Note does not exist");
        }

        /// <summary>
        /// Gets note with given id
        /// </summary>
        /// <param name="id">(Guid) id of the note</param>
        /// <returns>searched note, or BadRequest status</returns>
        
        [HttpGet("{id}", Name = "GetNoteById")]
        public IActionResult GetNoteById(Guid id)
        {
            foreach (var note in _notes)
            {
                if (note.Id == id)
                    return Ok(note);
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Note does not exist");

        }


        /// <summary>
        /// Delete one note
        /// </summary>
        /// <param name="id">(string) id of the note</param>
        /// <returns>list of notes and status code 200 if the id is valid, otherwise BadRequest </returns>
        
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            foreach (var note in _notes)
            {
                if (note.Id.ToString() == id)
                {
                    _notes.Remove(note);
                    return Ok(_notes);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Note does not exist");
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
