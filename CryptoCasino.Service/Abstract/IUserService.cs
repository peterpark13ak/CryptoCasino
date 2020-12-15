using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
    public interface IUserService
    {
        Task<User> RetrieveUser(string id);

        Task<IEnumerable<User>> GetAllUsers();

    

       
        Task<User> PromoteUser(string id);
        Task<User> LockUser(string id);

        Task<User> UnLockUser(string id);

        Task<User> EditUserAlias(string alias, string id);

        int TotalContainingText(string searchText);

        IEnumerable<User> ListByContainingText(string searchText, int page = 1, int pageSize = 10);

        Task<int> Total();
    }
}
