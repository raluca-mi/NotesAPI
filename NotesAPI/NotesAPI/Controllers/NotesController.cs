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
            return Ok(_notes);
            //return CreatedAtRoute("GetNoteById", new { id = note.Id.ToString() }, note);
        }

        /// <summary>
        /// Gets one note by owner id
        /// </summary>
        /// <param name="ownerId">(Guid) id of the owner</param>
        /// <returns>one note</returns>
        /// 
        [HttpGet("{ownerId}")]
        public IActionResult GetNoteByOwner(Guid ownerId)
        {
            /*Note filteredNote=(Note)_notes.Where(note => note.OwnerId == ownerId);
            return Ok(filteredNote);*/
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
        /// <param name="id">(string) id of the note</param>
        /// <returns>searched note, or BadRequest status</returns>
        [HttpGet("{id}")]
        public IActionResult GetNoteById(string id)
        {
            foreach (var note in _notes)
            {
                if (note.Id.ToString() == id)
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
            return StatusCode(StatusCodes.Status400BadRequest, "Category does not exist");
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
