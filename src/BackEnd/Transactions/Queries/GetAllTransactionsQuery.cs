using BackEnd.DTOs;
using MediatR;
using System.Collections.Generic;

namespace BackEnd.Transactions.Queries
{
	public class GetAllTransactionsQuery : IRequest<IList<TransactionResponse>> { }
}
