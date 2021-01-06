using BackEnd.DTOs;
using MediatR;

namespace BackEnd.Transactions.Queries
{
	public class GetUserBalanceQuery : IRequest<BalanceResponse> { }
}
