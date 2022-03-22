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
        private static List<Note> _notes = new List<Note> { new Note { Id = new Guid("00000000-0000-0000-0000-000000000001"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "First Note", Description = "First Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000002"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Second Note", Description = "Second Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000003"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Third Note", Description = "Third Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000004"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Note { Id = new Guid("00000000-0000-0000-0000-000000000005"), CategoryId = "1", OwnerId = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Fifth Note", Description = "Fifth Note Description" }
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
            if(note==null)
                return BadRequest("Note is null");
            
            _notes.Add(note);
            return Ok(_notes);
            //return CreatedAtRoute("GetNoteById", new { id = note.Id }, note);
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
        public IActionResult DeleteNote(Guid id)
        {
            int index = _notes.FindIndex(note => note.Id == id) ;
            if (index == -1)
                return NotFound("The note was not found!");

            _notes.RemoveAt(index);
            return Ok("The note was deleted");
        }


        [HttpPut("{id}")]
        public IActionResult UpdateNote(Guid id, [FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest("Note can't be null");
            }

            int index = _notes.FindIndex(note => note.Id == id);

            if (index == -1)
            {
                return CreateNote(note);                //if index not found create a new note
                //return NotFound("Id not found!");     //if index not found respond with NotFound
            }

            note.Id = id;
            _notes[index] = note;

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
