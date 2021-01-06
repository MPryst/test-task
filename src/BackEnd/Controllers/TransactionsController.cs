using BackEnd.DTOs;
using BackEnd.Transactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionsController : ControllerBase
	{
		private readonly ILogger<TransactionsController> logger;
		private readonly IMediator mediator;

		public TransactionsController(ILogger<TransactionsController> logger, IMediator mediator)
		{
			this.logger = logger;
			this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpGet("/api/default")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<BalanceResponse> GetBalance()
		{
			return await mediator.Send(new GetUserBalanceQuery());
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest transactionRequest)
		{			
			try
			{
				string newID = await mediator.Send(new CreateTransactionCommand(transactionRequest));
				return Created($"/api/Transactions/{newID}", new {uuid = newID });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IList<TransactionResponse>> GetAll()
		{
			return await mediator.Send(new GetAllTransactionsQuery());
		}

		[HttpGet("/api/Transactions/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetByID(string id)
		{
			try
			{
				return Ok(await mediator.Send(new GetTransactionByIDQuery(id)));
			}
			catch (Exception e) {
				return NotFound(e.Message);
			}			
		}
	}
}
