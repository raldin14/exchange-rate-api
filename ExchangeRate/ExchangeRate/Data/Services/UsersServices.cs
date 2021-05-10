using ExchangeRate.Data.Models;
using ExchangeRate.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Data.Services
{
    public class UsersServices
    {
        private AppDbContext _context;

        public UsersServices(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(UserVM user)
        {
            var _user = new User()
            {
                Name = user.Name
            };

            _context.Users.Add(_user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers() => _context.Users.ToList();
        public List<User> GetUserById(int id) => _context.Users.Where(n => n.Id == id).ToList();
    }
}
