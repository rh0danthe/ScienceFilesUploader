using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScienceFileUploader.Data;
using ScienceFileUploader.Entities;
using ScienceFileUploader.Exceptions.File;
using ScienceFileUploader.Repository.Interface;

namespace ScienceFileUploader.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly DataContext _context;
    
        public FileRepository(DataContext context) 
        { 
            _context = context;
        }
        
        public async Task<File> CreateAsync(File file)
        {
            var dbFile = await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();
            return dbFile.Entity;
        }

        public async Task<File> GetByIdAsync(int fileId)
        {
            var dbFile = await _context.Files.FindAsync(fileId);
            if (dbFile == null)
                throw new FileNotFoundException("This Id does not exist");
            return dbFile;
        }

        public async Task<File> GetByNameAsync(string fileName)
        {
            var dbFile = await _context.Files.FirstOrDefaultAsync(f => f.Name == fileName);
            if (dbFile == null)
                throw new FileNotFoundException("file with this name does not exist");
            return dbFile;
        }

        public async Task<bool> IfExistByNameAsync(string fileName)
        {
            return await _context.Files.FirstOrDefaultAsync(f => f.Name == fileName) != null;
        }

        public async Task<bool> DeleteAsync(string fileName)
        {
            var dbFile = await _context.Files.FirstOrDefaultAsync(f => f.Name == fileName);
            if (dbFile == null)
                throw new FileNotFoundException("File with this name does not exist");
            var res = _context.Files.Remove(dbFile);
            await _context.SaveChangesAsync();
            return res.State == EntityState.Deleted;
        }
    }
}