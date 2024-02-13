using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScienceFileUploader.Data;
using ScienceFileUploader.Entities;
using ScienceFileUploader.Exceptions.Result;
using ScienceFileUploader.Repository.Interface;

namespace ScienceFileUploader.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly DataContext _context;
    
        public ResultRepository(DataContext context) 
        { 
            _context = context;
        }
        public async Task<Result> CreateAsync(Result result)
        {
            var dbResult = await _context.Results.AddAsync(result);
            await _context.SaveChangesAsync();
            return dbResult.Entity;
        }

        public async Task<Result> GetByIdAsync(int resultId)
        {
            var dbResult = await _context.Results.FindAsync(resultId);
            if (dbResult == null)
                throw new ResultNotFoundException("This Id does not exist");
            return dbResult;
        }
        
        public async Task<ICollection<Result>> GetAllAsync()
        {
            return await _context.Results.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<Result> GetByFileNameAsync(string name)
        {
            var dbResult = await _context.Results.FirstOrDefaultAsync(r => r.FileName == name);
            if (dbResult == null)
                throw new ResultNotFoundException("Result with such filename does not exist");
            return dbResult;
        }

        public async Task<ICollection<Result>> GetAllByParametersAsync(int minParameter, int maxParameter)
        {
            var results =  await _context.Results
                .Where(r => minParameter < r.AvgByParameters && r.AvgByParameters < maxParameter)
                .OrderBy(r => r.Id).ToListAsync();
            if (results == null)
                throw new ResultNotFoundException("Result with such average parameter range does not exist");
            return results;
        }

        public async Task<ICollection<Result>> GetAllByTimeAsync(int minTime, int maxTime)
        {
            var results = await _context.Results
                .Where(r => minTime < r.AvgExperimentDuration && r.AvgExperimentDuration < maxTime)
                .OrderBy(r => r.Id).ToListAsync();
            if (results == null)
                throw new ResultNotFoundException("Result with such average time range does not exist");
            return results;
        }
    }
}