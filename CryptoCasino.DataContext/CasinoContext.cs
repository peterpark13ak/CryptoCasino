using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebCasino.Entities;
using WebCasino.Entities.Base;

namespace WebCasino.DataContext
{
	public class CasinoContext : IdentityDbContext<User>
	{
		public CasinoContext()
		{
		}

		public CasinoContext(DbContextOptions<CasinoContext> options) : base(options)
		{
		}

		public DbSet<Wallet> Wallets { get; set; }
		public DbSet<Currency> Currencies { get; set; }

		public DbSet<BankCard> BankCards { get; set; }

		public DbSet<LoginLog> LoginLogs { get; set; }

		public DbSet<Transaction> Transactions { get; set; }

		public DbSet<CurrencyExchangeRate> ExchangeRates { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Currency>().HasData(this.SeedCurrencies());
			builder.Entity<TransactionType>().HasData(this.SeedTransactionTypes());
			builder.Entity<IdentityRole>().HasData(this.SeedDefaultRoles());

			builder.Entity<Wallet>()
				.HasOne(w => w.User)
				.WithOne(u => u.Wallet)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<BankCard>()
				.HasOne(bc => bc.User)
				.WithMany(u => u.Cards)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<LoginLog>()
				.HasOne(l => l.User)
				.WithMany(u => u.Logs)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Transaction>()
				.HasOne(tr => tr.User)
				.WithMany(us => us.Transactions)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Transaction>()
				.HasOne(tr => tr.Card)
				.WithMany(card => card.Transcations)
				.OnDelete(DeleteBehavior.Restrict);

			this.SeedDefaultAdmin(builder);

			base.OnModelCreating(builder);
		}

		public override int SaveChanges()
		{
			this.ApplyAuditInfoRules();
			this.ApplyDeletionRules();
			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			this.ApplyAuditInfoRules();
			this.ApplyDeletionRules();
			return base.SaveChangesAsync(cancellationToken);
		}

		private void ApplyDeletionRules()
		{
			var entitiesForDeletion = this.ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Deleted && e.Entity is IDeletable);

			foreach (var entry in entitiesForDeletion)
			{
				var entity = (IDeletable)entry.Entity;
				entity.DeletedOn = DateTime.Now;
				entity.IsDeleted = true;
				entry.State = EntityState.Modified;
			}
		}

		private void ApplyAuditInfoRules()
		{
			var newlyCreatedEntities = this.ChangeTracker.Entries()
				.Where(e => e.Entity is IModifiable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

			foreach (var entry in newlyCreatedEntities)
			{
				var entity = (IModifiable)entry.Entity;

				if (entry.State == EntityState.Added && entity.CreatedOn == null)
				{
					entity.CreatedOn = DateTime.Now;
				}
				else
				{
					entity.ModifiedOn = DateTime.Now;
				}
			}
		}

		private Currency[] SeedCurrencies()
		{
			var usd = new Currency() { Id = 1, Name = "USD" };
			var gbp = new Currency() { Id = 2, Name = "GBP" };
			var euro = new Currency() { Id = 3, Name = "EUR" };
			var bgn = new Currency() { Id = 4, Name = "BGN" };
			return new Currency[] { gbp, usd, euro, bgn };
		}

		private TransactionType[] SeedTransactionTypes()
		{
			var win = new TransactionType() { Id = 1, Name = "Win" };
			var stake = new TransactionType() { Id = 2, Name = "Stake" };
			var deposit = new TransactionType() { Id = 3, Name = "Deposit" };
			var withdraw = new TransactionType() { Id = 4, Name = "Withdraw" };

			return new TransactionType[] { win, stake, deposit, withdraw };
		}

		private IdentityRole[] SeedDefaultRoles()
		{
			var administrator = new IdentityRole() { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" };
			var player = new IdentityRole() { Id = "2", Name = "Player", NormalizedName = "PLAYER" };

			return new IdentityRole[] { administrator, player };
		}

		private void SeedDefaultAdmin(ModelBuilder builder)
		{
			var adminUser = new User
			{
				Id = Guid.NewGuid().ToString(),
				UserName = "Admin",
				NormalizedUserName = "admin@mail.com".ToUpper(),
				Email = "admin@mail.com",
				TwoFactorEnabled = false,
				NormalizedEmail = "admin@mail.com".ToUpper(),
				EmailConfirmed = true,
				PhoneNumber = "+359359",
				PhoneNumberConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString("D"),
				Alias = "Boss",
				ModifiedOn = DateTime.Now,
				Locked = false,
				IsDeleted = false,
				AccessFailedCount = 0,
				LockoutEnabled = false
			};

			var hashePass = new PasswordHasher<User>().HashPassword(adminUser, "!Aa12345678");
			adminUser.PasswordHash = hashePass;

			builder.Entity<User>().HasData(adminUser);

			builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = 1.ToString(),
				UserId = adminUser.Id
			});

            var wallet = new Wallet
            {
                Id = "admin-wallet",
                CurrencyId = 1,
                UserId = adminUser.Id,
                DisplayBalance = 0,
                NormalisedBalance = 0
            };

            builder.Entity<Wallet>().HasData(wallet);
		}
	}
}