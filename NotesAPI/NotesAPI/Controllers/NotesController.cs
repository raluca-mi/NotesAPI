﻿using Microsoft.AspNetCore.Http;
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
        public IActionResult GetNotes()
        {
           return Ok(_noteCollectionService.GetAll());
        }

        ///// <summary>
        ///// Gets note with given id
        ///// </summary>
        ///// <param name="id">(Guid) id of the note</param>
        ///// <returns>searched note, or BadRequest status</returns>

        //[HttpGet("{id}", Name = "GetNoteById")]
        //public IActionResult GetNoteById(Guid id)
        //{
        //    foreach (var note in _notes)
        //    {
        //        if (note.Id == id)
        //            return Ok(note);
        //    }
        //    return NotFound("Note does not exist");

        //}

        ///// <summary>
        ///// Gets one note by owner id
        ///// </summary>
        ///// <param name="ownerId">(Guid) id of the owner</param>
        ///// <returns>first found note</returns>

        //[HttpGet("owner/{ownerId}")]
        //public IActionResult GetNoteByOwner(Guid ownerId)
        //{
        //    foreach (var note in _notes)
        //    {
        //        if (note.OwnerId == ownerId)
        //            return Ok(note);
        //    }
        //    return NotFound("Note does not exist");
        //}

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

           return Ok(_noteCollectionService.Create(note));
          //  return Ok(_notes);
            //return CreatedAtRoute("GetNoteById", new { id = note.Id }, note);
        }

        ///// <summary>
        ///// Update one note by id
        ///// </summary>
        ///// <param name="id">(Guid) id of the note</param>
        ///// <param name="note">(Note) updated note</param>
        ///// <returns>List of updated notes if the id is valid, otherwise create new note</returns>

        //[HttpPut("{id}")]
        //public IActionResult UpdateNote(Guid id, [FromBody] Note note)
        //{
        //    if (note == null)
        //    {
        //        return BadRequest("Note can't be null");
        //    }

        //    int index = _notes.FindIndex(note => note.Id == id);

        //    if (index == -1)
        //    {
        //        return CreateNote(note);                //if index not found create a new note
        //        //return NotFound("Id not found!");     //if index not found respond with NotFound
        //    }

        //    note.Id = id;
        //    _notes[index] = note;

        //    return Ok(_notes);
        //}


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
        ///// <returns>Ok if the id is valid, otherwise NotFound </returns>

        //[HttpDelete("{id}")]
        //public IActionResult DeleteNote(Guid id)
        //{
        //    int index = _notes.FindIndex(note => note.Id == id) ;
        //    if (index == -1)
        //        return NotFound("The note was not found!");

        //    _notes.RemoveAt(index);
        //    return Ok("The note was deleted");
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


        //From other course

        /* [HttpGet]
         public IActionResult Get([FromHeader(Name = "User-Agent")] string UserAgent)
         {
             return Ok(UserAgent);
         }
        */

    }
}
