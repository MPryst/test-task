using BackEnd.DTOs;
using BackEnd.Transactions.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Transactions.Queries
{
	public class GetTransactionByIDQueryHandler : IRequestHandler<GetTransactionByIDQuery, TransactionResponse>
	{
		private readonly ITransactionsService transactionsService;

		public GetTransactionByIDQueryHandler(ITransactionsService transactionsService) => this.transactionsService = transactionsService;

		public async Task<TransactionResponse> Handle(GetTransactionByIDQuery request, CancellationToken cancellationToken)
		{			
			return TransactionResponse.FromEntity(await transactionsService.GetByID(request.ID));
		}
	}
}
