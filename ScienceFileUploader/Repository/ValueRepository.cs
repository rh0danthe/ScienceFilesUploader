using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScienceFileUploader.Data;
using ScienceFileUploader.Entities;
using ScienceFileUploader.Exceptions.Value;
using ScienceFileUploader.Repository.Interface;

namespace ScienceFileUploader.Repository
{
    public class ValueRepository : IValueRepository
    {
        private readonly DataContext _context;
    
        public ValueRepository(DataContext context) 
        { 
            _context = context;
        }
        public async Task<Value> CreateAsync(Value value)
        {
            var dbValue = await _context.Values.AddAsync(value);
            await _context.SaveChangesAsync();
            return dbValue.Entity;
        }

        public async Task<ICollection<Value>> GetAllByFileNameAsync(string fileName)
        {
            var values = await _context.Values.Where(v => v.FileName == fileName).ToListAsync();
            if (values == null)
                throw new ValueNotFoundException("File with this name does not exist");
            return values;
        }
    }
}