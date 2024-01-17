using MeetupAPI.DAL;
using MeetupAPI.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        protected readonly MeetupDatabaseContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(MeetupDatabaseContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetItemsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetItemByIDAsync(Guid itemID)
        {
            return await _dbSet.FindAsync(itemID);
        }

        public async Task<T> InsertItemAsync(T newItem)
        {
            await _dbSet.AddAsync(newItem);
            return newItem;
        }

        public T UpdateItem(T modifiedItem)
        {
            _dbSet.Update(modifiedItem);
            return modifiedItem;
        }

        public void DeleteItem(T removableItem)
        {
            _dbSet.Remove(removableItem);
        }
    }
}
