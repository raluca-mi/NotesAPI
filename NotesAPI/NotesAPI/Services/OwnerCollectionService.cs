using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Services
{
    public class OwnerCollectionService// : IOwnerCollectionService
    {
        static List<Owner> _owners = new List<Owner> {
        new Owner{ Id=Guid.NewGuid(), Name="Ana"},
        new Owner{ Id=Guid.NewGuid(), Name="Raluca"}};

        //public bool Create(Owner owner)
        //{
        //    _owners.Add(owner);
        //    return true;
        //}

        //public bool Delete(Guid id)
        //{
        //    int index = _owners.FindIndex(owner => owner.Id == id);
        //    if (index == -1)
        //        return false;

        //    _owners.RemoveAt(index);
        //    return true;
        //}

        //public Owner Get(Guid id)
        //{
        //    int index = _owners.FindIndex(note => note.Id == id);
        //    if (index == -1)
        //        return null;
        //    return _owners[index];
        //}

        //public List<Owner> GetAll()
        //{
        //    return _owners;
        //}

        //public bool Update(Guid id, Owner owner)
        //{
        //    int index = _owners.FindIndex(owner => owner.Id == id);

        //    if (index == -1)
        //    {
        //        Create(owner);
        //        return false; 
        //    }

        //    owner.Id = id;
        //    _owners[index] = owner;

        //    return true;
        //}
    }
}
