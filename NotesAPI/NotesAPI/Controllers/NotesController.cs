using Microsoft.AspNetCore.Http;
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
    public class NotesController : ControllerBase
    {
        INoteCollectionService _noteCollectionService;
        public NotesController(INoteCollectionService noteCollectionService)
        {
            _noteCollectionService = noteCollectionService ?? throw new ArgumentNullException(nameof(noteCollectionService));
        }

        /// <summary>
        /// Get all notes
        /// </summary>
        /// <returns>list of notes</returns>

        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            return Ok(await _noteCollectionService.GetAll());
        }

        /// <summary>
        /// Gets note with given id
        /// </summary>
        /// <param name="id">(Guid) id of the note</param>
        /// <returns>searched note, or BadRequest status</returns>

        [HttpGet("{id}", Name = "GetNoteById")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            var note =await _noteCollectionService.Get(id);

            if (note == null)
                return NotFound();
            return Ok(note);
        }

        /// <summary>
        /// Gets one note by owner id
        /// </summary>
        /// <param name="ownerId">(Guid) id of the owner</param>
        /// <returns>first found note</returns>

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetNoteByOwner(Guid ownerId)
        {
            List<Note> filteredNotes = await _noteCollectionService.GetNotesByOwnerId(ownerId);
            if (filteredNotes.Count == 0)
            {
                return NotFound("Note with given owner id doesn't exist!");
            }
            return Ok(filteredNotes);

        }

        /// <summary>
        /// Create new note
        /// </summary>
        /// <param name="note">(Note) note</param>
        /// <returns>updated list of notes</returns>

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            if (note == null)
                return BadRequest("Note is null");
            await _noteCollectionService.Create(note);
            return Ok("Note was created");
          
        }

        /// <summary>
        /// Update one note by id
        /// </summary>
        /// <param name="id">(Guid) id of the note</param>
        /// <param name="note">(Note) updated note</param>
        /// <returns>List of updated notes if the id is valid, otherwise create new note</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, [FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest("Note can't be null");
            }

            if (await _noteCollectionService.Update(id, note) == true)
                return Ok();
            else
            return NotFound();

        }

        /// <summary>
        /// Delete one note
        /// </summary>
        /// <param name="id">(Guid) id of the note</param>
        /// <returns>Ok if the id is valid, otherwise NotFound </returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {

            if (await _noteCollectionService.Delete(id) == false)
                return NotFound("The note was not found!");
            else
                return Ok("The note has been deleted!");
        }


        ///// <summary>
        ///// Update one note
        ///// </summary>
        ///// <param name="id">(Guid) id of the note</param>
        ///// <param name="ownerId">(Guid) id of the owner</param>
        ///// <param name="note">(Note) updated note</param>
        ///// <returns>List of updated notes if the id is valid, otherwise create new note</returns>

        //[HttpPut("{id},{ownerId}")]
        //public IActionResult UpdateNoteByIdAndOwner(Guid id, Guid ownerId, [FromBody] Note note)
        //{
        //    if (note == null)
        //    {
        //        return BadRequest("Note can't be null");
        //    }

        //    int index = _notes.FindIndex(note => note.Id == id && note.OwnerId == ownerId);

        //    if (index == -1)
        //    {
        //        return CreateNote(note);                //if index not found create a new note
        //        //return NotFound("Id not found!");     //if index not found respond with NotFound
        //    }

        //    note.Id = id;
        //    note.OwnerId = ownerId;
        //    _notes[index] = note;

        //    return Ok(_notes);
        //}




        ///// <summary>
        ///// Delete one note
        ///// </summary>
        ///// <param name="id">(Guid) id of the note</param>
        ///// <param name="ownerId">(Guid) id of the owner</param>
        ///// <returns>Ok if the id is valid, otherwise NotFound </returns>

        //[HttpDelete("{id},{ownerId}")]
        //public IActionResult DeleteNoteByIdAndOwner(Guid id,Guid ownerId)
        //{
        //    int index = _notes.FindIndex(note => note.Id == id && note.OwnerId == ownerId);
        //    if (index == -1)
        //        return NotFound("The note was not found!");

        //    _notes.RemoveAt(index);
        //    return Ok("The note was deleted");
        //}

        ///// <summary>
        ///// Delete all notes of a given owner
        ///// </summary>
        ///// <param name="ownerId">(Guid) id of the owner</param>
        ///// <returns>Ok if at least one note found by ownerId, otherwise NotFound</returns>

        //[HttpDelete("owner/{ownerId}")]
        //public IActionResult DeleteNotesByOwner(Guid ownerId)
        //{

        //    int index = _notes.FindIndex(note => note.OwnerId == ownerId);
        //    if (index == -1)
        //        return NotFound("The note was not found!");
        //    while (_notes.FindIndex(note => note.OwnerId == ownerId) != -1)
        //    {
        //        _notes.RemoveAt(index);
        //    }

        //    return Ok("The notes have been deleted");
        //}

    }
}
