﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Repositories.Data;

namespace Talabat.Repositories.Interfaces.Contract
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TalabatDbContext _dbContext;

        public GenericRepository(TalabatDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
         return  await _dbContext.Set<T>().ToListAsync();
            
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);  //using hashsets as unique key 
        }
    }
}
