using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Service.Utility.Validator;

namespace WebCasino.Service
{
	public class CardService : ICardService
	{
		private readonly CasinoContext dbContext;

		public CardService(CasinoContext dbContext)
		{
			ServiceValidator.ObjectIsNotEqualNull(dbContext);

			this.dbContext = dbContext;
		}

		public async Task<BankCard> AddCard(string cardNumber, string userId, DateTime expiration)
		{
			ServiceValidator.IsInputStringEmptyOrNull(cardNumber);
			ServiceValidator.CheckStringLength(cardNumber, 16, 16);
			ServiceValidator.ValidateCardNumber(cardNumber);
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.CheckCardExpirationDate(expiration);
			
			var bankCard = new BankCard()
			{
				CardNumber = cardNumber,
				UserId = userId,
				Expiration = expiration,
				IsDeleted = false,
			};

			await this.dbContext.BankCards.AddAsync(bankCard);
			await this.dbContext.SaveChangesAsync();

			return bankCard;
		}

		public async Task<IEnumerable<BankCard>> GetAllCards(string userId)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);

			var bankCardQuery = await this.dbContext.BankCards
				.Where(u => u.UserId == userId && u.IsDeleted == false)
				.ToListAsync();

			return bankCardQuery;
		}
		
        //TO DO: FIX THIS
		public async Task<BankCard> GetCard(string cardNumber)
		{
			ServiceValidator.IsInputStringEmptyOrNull(cardNumber);
			//ServiceValidator.ValidateCardNumber(cardNumber);

			var bankCardQuery = await this.dbContext.BankCards
				.FirstOrDefaultAsync(c => c.Id == cardNumber && c.IsDeleted == false);

			ServiceValidator.ObjectIsNotEqualNull(bankCardQuery);

			return bankCardQuery;
		}


		public async Task<BankCard> RemoveCard(string cardNumber)
		{
			ServiceValidator.IsInputStringEmptyOrNull(cardNumber);
			ServiceValidator.ValidateCardNumber(cardNumber);

			var bankCardQuery = await this.dbContext.BankCards
				.FirstOrDefaultAsync(c => c.CardNumber == cardNumber && c.IsDeleted == false);

			ServiceValidator.ObjectIsNotEqualNull(bankCardQuery);

			bankCardQuery.IsDeleted = true;

			await this.dbContext.SaveChangesAsync();

			return bankCardQuery;
		}

		//TODO: CHOFEXX - What currency to Withdraw
		public async Task<double> Withdraw(string cardNumber, double amount)
		{
			ServiceValidator.IsInputStringEmptyOrNull(cardNumber);
			//ServiceValidator.ValidateCardNumber(cardNumber);
			ServiceValidator.ValueIsBetween(amount, 0, double.MaxValue);

			var bankCardQuery = await this.dbContext.BankCards
				.FirstOrDefaultAsync(c => c.Id == cardNumber && c.IsDeleted == false);

			ServiceValidator.ObjectIsNotEqualNull(bankCardQuery);
			ServiceValidator.CheckCardExpirationDate(bankCardQuery.Expiration);
			
			bankCardQuery.MoneyRetrieved += amount;
			await this.dbContext.SaveChangesAsync();
			

			return amount;
		}

        public async Task<double> Deposit(string cardNumber, double amount)
        {
            ServiceValidator.IsInputStringEmptyOrNull(cardNumber);
            ServiceValidator.ValueIsBetween(amount, 0, double.MaxValue);

            var bankCardQuery = await this.dbContext.BankCards
                .FirstOrDefaultAsync(c => c.Id == cardNumber && c.IsDeleted == false);

            ServiceValidator.ObjectIsNotEqualNull(bankCardQuery);
            ServiceValidator.CheckCardExpirationDate(bankCardQuery.Expiration);

            bankCardQuery.MoneyAdded += amount;
            await this.dbContext.SaveChangesAsync();


            return amount;
        }
    }
}