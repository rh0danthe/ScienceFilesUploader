using System;
using System.Threading.Tasks;
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
    }
}