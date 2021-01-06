using System;
using System.ComponentModel;

namespace BackEnd.Transactions.Domain
{
	public class Transaction
	{
		public Guid UUID { get; set; }		

		public TransactionType Type  { get; set; }

		public double Amount { get; set; }

		public DateTime EffectiveDate { get; set; }

		public Transaction(Guid uUID, TransactionType type, double amount, DateTime effectiveDate)
		{
			UUID = uUID;
			Type = type;
			Amount = amount;
			EffectiveDate = effectiveDate;
		}
	}
}

namespace BackEnd
{
	public enum TransactionType
	{
		[Description("credit")]
		Credit,

		[Description("debit")]
		Debit
	}
}