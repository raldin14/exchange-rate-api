using ExchangeRate.Data.Models;
using ExchangeRate.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Services
{
    public class TransactionService
    {
        private AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public void AddTransaction(TransactionVM transaction)
        {
            var _transaction = new Transaction()
            {
                UserId = transaction.UserId,
                Monto = transaction.Monto,
                Exchange_rate = transaction.Exchange_rate,
                Iso_Code = transaction.Iso_Code,
                Purchased_Date = DateTime.Now.Date
            };

            _context.Transactions.Add(_transaction);
            _context.SaveChanges();
        }

        public List<Transaction> GetAllTransactions() => _context.Transactions.ToList();

        public List<Transaction> GetAllTransactionsByUser(int id) => _context.Transactions.Where(n => n.UserId == id).ToList();
        public List<Transaction> GetAllTransactionsByUserAndDate(int id, DateTime date) => _context.Transactions.Where(i => i.UserId.Equals(id)).Where( n => n.Purchased_Date == date).ToList();
        public List<Transaction> GetAllTransactionsByDate(DateTime date) => _context.Transactions.Where(n => n.Purchased_Date == date).ToList();

    }
}
