using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;
using WebCasino.Service.DTO.Filtering;
using WebCasino.Service.Utility.TableFilterUtilities;

namespace WebCasino.Service.Abstract
{
	public interface ITransactionService
	{
        Task<TableFilteringDTO> GetFiltered(DataTableModel model);

        IQueryable<Transaction> GetAllTransactionsTable();

        Task<IEnumerable<Transaction>> GetUserTransactions(string userId);

        Task<Transaction> RetrieveUserTransaction(string id);

        Task<IEnumerable<Transaction>> GetTransactionByType(string transactionTypeName);

        Task<Transaction> AddWinTransaction(string userId, double originalAmount, string description);

		Task<Transaction> AddStakeTransaction(string userId, double originalAmount, string description);

		Task<Transaction> AddWithdrawTransaction(string userId, string cardId, double originalAmount, string description);

		Task<Transaction> AddDepositTransaction(string userId, string cardId, double originalAmount, string description);

        Task<IEnumerable<Transaction>> RetrieveAllUsersTransaction(string id);



    }
}