using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Services
{
    public interface INoteCollectionService : ICollectionService<Note>
    {
        Task<List<Note>> GetNotesByOwnerIdAsync(Guid ownerId);
        Task<bool> DeleteNotesByOwnerAsync(Guid ownerId);
        Task<bool> DeleteNoteByIdAndOwnerAsync(Guid id, Guid ownerId);
        Task<bool> UpdateNoteByIdAndOwnerAsync(Guid id, Guid ownerId, Note note);
    }
}
