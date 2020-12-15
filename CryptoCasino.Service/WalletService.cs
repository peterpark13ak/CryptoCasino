using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Service.Exceptions;
using WebCasino.Service.Utility.Validator;

namespace WebCasino.Service
{
    public class WalletService : IWalletService
    {
        private readonly CasinoContext context;

        public WalletService(CasinoContext context)
        {
            this.context = context ?? throw new NullReferenceException();
        }

        public async Task<Wallet> CreateWallet(string userId, int currencyId)
        {
            var wallet = await this.context.Wallets.FirstOrDefaultAsync(wa => wa.UserId == userId);
            var currency = await this.context.Currencies.FirstOrDefaultAsync(cu => cu.Id == currencyId);
            if (wallet != null)
            {
                throw new EntityAlreadyExistsException("The user already has wallet");
            }

            ServiceValidator.ObjectIsNotEqualNull(currency);

            var newWallet = new Wallet()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Currency = currency
            };

            context.Wallets.Add(newWallet);
            await context.SaveChangesAsync();
            return newWallet;
        }


        public async Task<Wallet> RetrieveWallet(string userId)
        {
            //TO DO: OPTIMIZE!!!
            var wallet = await this.context.Wallets
                .Include(wa => wa.User)
                    .ThenInclude(us => us.Cards)
                 .Include(wa => wa.User)
                    .ThenInclude(us => us.Transactions)
                        .ThenInclude(tr => tr.TransactionType)
                .FirstOrDefaultAsync(wa => wa.UserId == userId);

            ServiceValidator.ObjectIsNotEqualNull(wallet);
            return wallet;
        }

    }
}
