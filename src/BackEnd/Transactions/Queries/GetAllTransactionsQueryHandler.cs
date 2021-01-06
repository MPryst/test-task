using BackEnd.DTOs;
using BackEnd.Transactions.Services;
using MediatR;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Transactions.Queries
{
	public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, IList<TransactionResponse>>
	{
		private readonly ITransactionsService transactionsService;

		public GetAllTransactionsQueryHandler(ITransactionsService transactionsService) => this.transactionsService = transactionsService;

		public async Task<IList<TransactionResponse>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
		{
			return (await transactionsService.GetAll())
				.Select(x => TransactionResponse.FromEntity(x)).ToList();
		}
	}
}
