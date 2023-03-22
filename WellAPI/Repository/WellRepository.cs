using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WellAPI.Models;
using WellAPI.Data;
using System.Linq;

namespace WellAPI.Repository
{
    public class WellRepository : IWellRepository
    {
        private readonly ApiContext _context;
        public WellRepository(ApiContext context)
        {
            _context = context;
        }
        public IEnumerable<Well> GetAll()
        {
            return _context.Wells.ToList();
        }
        public Well GetById(long WellID)
        {
            return _context.Wells.Find(WellID);
        }
        public void Add(Well Well)
        {
            _context.Wells.Add(Well);
        }
        public void Update(Well Well)
        {
            _context.Entry(Well).State = EntityState.Modified;
        }
        public void Delete(long WellID)
        {
            Well Well = _context.Wells.Find(WellID);
            _context.Wells.Remove(Well);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
