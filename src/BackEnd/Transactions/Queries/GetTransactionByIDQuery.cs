using BackEnd.DTOs;
using MediatR;

namespace BackEnd.Transactions.Queries
{
	public class GetTransactionByIDQuery : IRequest<TransactionResponse> 
	{
		public string ID { get; }

		public GetTransactionByIDQuery(string ID) {
			this.ID = ID;
		}		
	}
}
