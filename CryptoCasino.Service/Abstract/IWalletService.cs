using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(string userId, int currencyId);
        Task<Wallet> RetrieveWallet(string userId);

    }
}
