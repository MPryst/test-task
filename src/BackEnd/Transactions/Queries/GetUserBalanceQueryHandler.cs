using BackEnd.DTOs;
using BackEnd.Transactions.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Transactions.Queries
{
	public class GetUserBalanceQueryHandler : IRequestHandler<GetUserBalanceQuery, BalanceResponse>
	{
		private readonly ITransactionsService transactionsService;

		public GetUserBalanceQueryHandler(ITransactionsService transactionsService) => this.transactionsService = transactionsService;


		public async Task<BalanceResponse> Handle(GetUserBalanceQuery request, CancellationToken cancellationToken)
			=> new BalanceResponse() { balance = await transactionsService.GetBalance() };

	}
}
