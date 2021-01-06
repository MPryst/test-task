using BackEnd.DTOs;
using BackEnd.Helpers;
using MediatR;
using System;

namespace BackEnd.Transactions.Queries
{
	public class CreateTransactionCommand : IRequest<string>
	{
		public TransactionRequest TransactionRequest { get; }

		public CreateTransactionCommand(TransactionRequest transactionRequest)
		{
			ValidateInputs(transactionRequest);

			TransactionRequest = transactionRequest;
		}

		private void ValidateInputs(TransactionRequest transactionRequest)
		{
			TransactionType type = EnumHelper.GetValueFromDescription<TransactionType>(transactionRequest.Type.ToLower());

			if (transactionRequest.Amount <= 0d) throw new ArgumentOutOfRangeException("The amount must be greater than zero");
		}
	}
}
