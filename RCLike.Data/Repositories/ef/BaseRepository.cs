using Microsoft.EntityFrameworkCore;
using RCLike.Data.Contexts;
using RCLike.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCLike.Data.Repositories.ef
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<T> DbSet;

        public BaseRepository(AppDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }
    }
}
