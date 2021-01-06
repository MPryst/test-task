using BackEnd.Transactions.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Transactions.Queries
{
	public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string>
	{
		private readonly ITransactionsService transactionsService;

		public CreateTransactionCommandHandler(ITransactionsService transactionsService) => this.transactionsService = transactionsService;
		

		public async Task<string> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
		{
			return await transactionsService.Create(request.TransactionRequest);
		}
	}
}
