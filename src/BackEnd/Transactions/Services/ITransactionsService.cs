using BackEnd.DTOs;
using BackEnd.Transactions.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Transactions.Services
{
	public interface ITransactionsService
	{
		public Task<double> GetBalance();

		public Task<string> Create(TransactionRequest transactionRequest);

		public Task<IList<Transaction>> GetAll();

		public Task<Transaction> GetByID(string UUID);

	}
}
