using BackEnd.DTOs;
using BackEnd.Helpers;
using BackEnd.Transactions.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Transactions.Services
{
	public class TransactionsService : ITransactionsService
	{
		private const int DEFAULT_TIMEOUT = 1000;
		private static SynchronizedCache transactionsCache = new SynchronizedCache();

		public async Task<string> Create(TransactionRequest transactionRequest)
		{
			Transaction transaction = new Transaction(
				Guid.NewGuid(), 
				EnumHelper.GetValueFromDescription<TransactionType>(transactionRequest.Type.ToLower()), 
				transactionRequest.Amount, 
				DateTime.Now);

			if (transactionsCache.AddWithTimeout(transaction, DEFAULT_TIMEOUT)) {
				return await Task.Run(() => transaction.UUID.ToString());
			} else {
				throw new ArgumentException("The operation is not permitted");
			}			
		}

		public async Task<IList<Transaction>> GetAll()
		{
			return await Task.Run(() => transactionsCache.ReadAll());
		}

		public async Task<double> GetBalance()
		{
			return await Task.Run(() => transactionsCache.ReadBalance());
		}

		public async Task<Transaction> GetByID(string UUID)
		{
			return await Task.Run(() => transactionsCache.Read(UUID));
		}
	}
}
