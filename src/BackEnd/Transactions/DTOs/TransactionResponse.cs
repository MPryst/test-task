using BackEnd.Transactions.Domain;
using System;

namespace BackEnd.DTOs
{
	public class TransactionResponse
	{
		public string uuid { get; set; }

		public TransactionType type { get; set; }

		public double amount { get; set; }

		public string effectiveDate { get; set; }

		public static TransactionResponse FromEntity(Transaction transaction) {
			return new TransactionResponse()
			{
				uuid = transaction.UUID.ToString(),
				amount = transaction.Amount,
				effectiveDate = transaction.EffectiveDate.ToString("f"),
				type = transaction.Type
			};
		}
	}
}