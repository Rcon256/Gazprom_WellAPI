using System.Collections.Generic;
using WellAPI.Models;

namespace WellAPI.Repository
{
    public interface IWellRepository
    {
        IEnumerable<Well> GetAll();
        Well GetById(long WellID);
        void Add(Well well);
        void Update(Well well);
        void Delete(long WellID);
        void Save();
    }
}
