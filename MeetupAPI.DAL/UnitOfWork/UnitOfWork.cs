using MeetupAPI.DAL;
using MeetupAPI.DAL.Repositories;
using MeetupAPI.DAL.Repositories.Abstractions;
using MeetupAPI.DAL.UnitOfWork.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private MeetupDatabaseContext _context;
        private Dictionary<Type, object> _repositories;
        private bool _isDisposed;

        public UnitOfWork(MeetupDatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories is null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories[typeof(T)] = new BaseRepository<T>(_context);
            }

            return (IBaseRepository<T>)_repositories[typeof(T)];
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _repositories?.Clear();
                    _context.Dispose();
                }
            }
            _isDisposed = true;
        }

    }
}
