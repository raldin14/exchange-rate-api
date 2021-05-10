using ExchangeRate.Data.Models;
using ExchangeRate.Data.Services;
using ExchangeRate.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("get-all-transactions")]
        public IActionResult GetAllTransactions()
        {
            var allTransactions = _transactionService.GetAllTransactions();
           
            return Ok(allTransactions);
        }        

        [HttpGet("get-all-transactions-by-user/{id}")]
        public IActionResult GetAllTransactionsByUser(int id)
        {
            var allTransactionsByUser = _transactionService.GetAllTransactionsByUser(id);

            if(allTransactionsByUser.Count != 0)
            {
                return Ok(allTransactionsByUser);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-all-transactions-by-date/{date}")]
        public IActionResult GetAllTransactionsByDate(DateTime date)
        {
            var allTransactionsByDate = _transactionService.GetAllTransactionsByDate(date);

            if (allTransactionsByDate.Count != 0)
            {
                return Ok(allTransactionsByDate);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-all-transactions-by-user-and-date/{id}/{date}")]
        public IActionResult GetAllTransactionsByUserAndDate(int id, DateTime date)
        {
            var allTransactionsByUserAndDate = _transactionService.GetAllTransactionsByUserAndDate(id, date);

            if (allTransactionsByUserAndDate.Count != 0)
            {
                return Ok(allTransactionsByUserAndDate);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("add-transaction")]
        public IActionResult AddTransaction([FromBody] TransactionVM transaction)
        {
            try
            {
                _transactionService.AddTransaction(transaction);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            ;
        }
    }
}
