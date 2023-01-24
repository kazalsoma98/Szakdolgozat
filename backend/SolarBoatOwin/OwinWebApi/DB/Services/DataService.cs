using Microsoft.EntityFrameworkCore;
using OwinWebApi.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinWebApi.DB
{
    internal class DataService
    {
        private readonly SolarBoatContext context;

        public DataService()
        {
            context=new SolarBoatContext();
        }

        internal async Task<int> AddNewAsync(Data value)
        {
            await context.Data.AddAsync(value);
            await context.SaveChangesAsync();
            return value.Id;
        }

        internal async Task DeleteAllAsync()
        {
            await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Data]");
        }

        internal async Task<List<Data>> GetAllAsync()
        {
            int limit = 60;
            int c=await context.Data.CountAsync();
            int n = c-limit;
            n= Math.Max(n, 0);
            return await context.Data.Skip(n).ToListAsync();
        }

        internal async Task<Data> GetByIdAsync(int id)
        {
            return await context.Data.FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
