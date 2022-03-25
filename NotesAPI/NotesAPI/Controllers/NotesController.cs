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

        #region Get

        /// <summary>
        /// Get all notes
        /// </summary>
        /// <returns>list of notes</returns>

        [HttpGet]
        public async Task<IActionResult> GetNotesAsync()
        {
            return Ok(await _noteCollectionService.GetAllAsync());
        }

        /// <summary>
        /// Gets note with given id
        /// </summary>
        /// <param name="id">(Guid) id of the note</param>
        /// <returns>searched note, or BadRequest status</returns>

        [HttpGet("{id}", Name = "GetNoteById")]
        public async Task<IActionResult> GetNoteByIdAsync(Guid id)
        {
            var note =await _noteCollectionService.GetAsync(id);

            if (note == null)
                return NotFound("Note not foud");
            return Ok(note);
        }

        /// <summary>
        /// Gets notes by owner id
        /// </summary>
        /// <param name="ownerId">(Guid) id of the owner</param>
        /// <returns>found notes</returns>

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetNotesByOwnerAsync(Guid ownerId)
        {
            List<Note> filteredNotes = await _noteCollectionService.GetNotesByOwnerIdAsync(ownerId);
            if (filteredNotes.Count == 0)
            {
                return NotFound("Notes not found!");
            }
            return Ok(filteredNotes);

        }
        #endregion

        #region Create

        /// <summary>
        /// Create new note
        /// </summary>
        /// <param name="note">(Note) note</param>
        /// <returns>updated list of notes</returns>

        [HttpPost]
        public async Task<IActionResult> CreateNoteAsync([FromBody] Note note)
        {
            if (note == null)
                return BadRequest("Note is null!");
            await _noteCollectionService.CreateAsync(note);
            return Ok();
          
        }
        #endregion

        #region Update

        /// <summary>
        /// Update one note by id
        /// </summary>
        /// <param name="id">(Guid) id of the note</param>
        /// <param name="note">(Note) updated note</param>
        /// <returns>List of updated notes if the id is valid, otherwise create new note</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNoteAsync(Guid id, [FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest("Note is null!");
            }

            if (await _noteCollectionService.UpdateAsync(id, note) == true)
                return Ok();

            return NotFound("Note not found!");

        }

        /// <summary>
        /// Update one note
        /// </summary>
        /// <param name = "id" > (Guid)id of the note</param>
        /// <param name = "ownerId" > (Guid)id of the owner</param>
        /// <param name = "note" > (Note)updated note</param>
        /// <returns>List of updated notes if the id is valid, otherwise create new note</returns>

        [HttpPut("{id},{ownerId}")]
        public async Task<IActionResult> UpdateNoteByIdAndOwnerAsync(Guid id, Guid ownerId, [FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest("Note is null!");
            }

            if (await _noteCollectionService.UpdateNoteByIdAndOwnerAsync(id, ownerId, note) == false)
                return NotFound("Id not found!");     //if index not found respond with NotFound

            return Ok("Note updated!");
        }
        #endregion

        #region Delete

        /// <summary>
        /// Delete one note
        /// </summary>
        /// <param name="id">(Guid) id of the note</param>
        /// <returns>Ok if the id is valid, otherwise NotFound </returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteAsync(Guid id)
        {

            if (await _noteCollectionService.DeleteAsync(id) == false)
                return NotFound("Note not found!");
            else
                return Ok();
        }

        /// <summary>
        /// Delete all notes of a given owner
        /// </summary>
        /// <param name="ownerId">(Guid) id of the owner</param>
        /// <returns>Ok if at least one note found by ownerId, otherwise NotFound</returns>

        [HttpDelete("owner/{ownerId}")]
        public async Task<IActionResult> DeleteNotesByOwnerAsync(Guid ownerId)
        {

            if (await _noteCollectionService.DeleteNotesByOwnerAsync(ownerId) == false)
                return NotFound("Note not found!");

            return Ok();
        }


        /// <summary>
        /// Delete one note
        /// </summary>
        /// <param name="id">(Guid) id of the note</param>
        /// <param name="ownerId">(Guid) id of the owner</param>
        /// <returns>Ok if the id is valid, otherwise NotFound </returns>

        [HttpDelete("{id},{ownerId}")]
        public async Task<IActionResult> DeleteNoteByIdAndOwnerAsync(Guid id, Guid ownerId)
        {
           if(await _noteCollectionService.DeleteNoteByIdAndOwnerAsync(id,ownerId)==false)
                return NotFound("Note not found!");

            return Ok();
        }

        #endregion
      


    }
}
