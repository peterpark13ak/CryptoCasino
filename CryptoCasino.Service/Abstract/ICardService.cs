using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
	public interface ICardService
    {
		Task<BankCard> AddCard(string cardNumber, string userId, DateTime Expiration);

		Task<IEnumerable<BankCard>> GetAllCards(string userId);

		Task<BankCard> GetCard(string cardNumber);

		Task<double> Withdraw(string cardNumber, double amount);

        Task<double> Deposit(string cardNumber, double amount);


        Task<BankCard> RemoveCard(string cardNumber);


    }
}
