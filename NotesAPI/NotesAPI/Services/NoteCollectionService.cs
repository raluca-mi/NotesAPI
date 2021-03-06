using MongoDB.Driver;
using NotesAPI.Models;
using NotesAPI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Services
{
    public class NoteCollectionService : INoteCollectionService
    {

        private readonly IMongoCollection<Note> _notes;
        public NoteCollectionService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _notes = database.GetCollection<Note>(settings.NoteCollectionName);
        }

        #region Get
        public async Task<Note> GetAsync(Guid id)
        {
            return (await _notes.FindAsync(note => note.Id == id)).FirstOrDefault();
        }

        public async Task<List<Note>> GetAllAsync()
        {
            var result = await _notes.FindAsync(note => true);
            return result.ToList();
        }
     
        public async Task<List<Note>> GetNotesByOwnerIdAsync(Guid ownerId)
        {
            return (await _notes.FindAsync(note => note.OwnerId == ownerId)).ToList();
        }
        #endregion

        #region Create
        public async Task<bool> CreateAsync(Note note)
        {
            if(note.Id == Guid.Empty)
            {
                note.Id = Guid.NewGuid();
            }
            await _notes.InsertOneAsync(note);
            return true;
        }

        #endregion

        #region Delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _notes.DeleteOneAsync(note => note.Id == id);
            if (result.IsAcknowledged && result.DeletedCount == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeleteNotesByOwnerAsync(Guid ownerId)
        {
            var result = await _notes.DeleteManyAsync(owner => owner.OwnerId == ownerId);
            if (result.IsAcknowledged && result.DeletedCount == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeleteNoteByIdAndOwnerAsync(Guid id, Guid ownerId)
        {
            var result = await _notes.DeleteOneAsync(note => note.Id == id && note.OwnerId == ownerId);
            if(result.DeletedCount==0)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Update
        public async Task<bool> UpdateAsync(Guid id, Note note)
        {
            note.Id = id;
            var result = await _notes.ReplaceOneAsync(n => n.Id == id, note);
            if (!result.IsAcknowledged && result.ModifiedCount == 0)
            {
                await _notes.InsertOneAsync(note);
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateNoteByIdAndOwnerAsync(Guid id, Guid ownerId, Note note)
        {
            note.Id = id;
            note.OwnerId = ownerId;
            var result = await _notes.ReplaceOneAsync(note => note.Id == id && note.OwnerId == ownerId, note);
            if(result.ModifiedCount == 0)
            {
                await _notes.InsertOneAsync(note);
                return false;
            }
            return true;
        }

        #endregion
    }
}
