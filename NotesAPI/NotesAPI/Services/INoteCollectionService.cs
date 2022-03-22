using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Services
{
    public interface INoteCollectionService : ICollectionService<Note>
    {
        List<Note> GetNotesByOwnerId(Guid ownerId);
    }
}
