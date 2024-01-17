using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DAL.Repositories.Abstractions
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetItemsAsync();
        Task<T?> GetItemByIDAsync(Guid itemID);
        Task<T> InsertItemAsync(T newItem);
        T UpdateItem(T modifiedItem);
        void DeleteItem(T removableItem);
    }
}
