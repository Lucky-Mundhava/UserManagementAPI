using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementAPI.Models;

namespace UserManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<int, User> _users = new();
        private int _nextId = 1;

        public UserRepository()
        {
            AddAsync(new User { Name = "Alice Smith", Email = "alice@example.com", Role = "Admin" }).GetAwaiter().GetResult();
            AddAsync(new User { Name = "Bob Jones", Email = "bob@example.com", Role = "HR" }).GetAwaiter().GetResult();
            AddAsync(new User { Name = "Charlie Brown", Email = "charlie@example.com", Role = "Employee" }).GetAwaiter().GetResult();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            await Task.Delay(50);
            return _users.Values.ToList();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            await Task.Delay(20);
            _users.TryGetValue(id, out var user);
            return user;
        }

        public async Task<User> AddAsync(User user)
        {
            await Task.Delay(30);
            user.Id = _nextId++;
            user.CreatedAt = DateTime.UtcNow;
            _users.TryAdd(user.Id, user);
            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            await Task.Delay(30);
            if (!_users.ContainsKey(user.Id))
                return false;
            
            user.CreatedAt = _users[user.Id].CreatedAt;
            _users[user.Id] = user;
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Task.Delay(30);
            return _users.TryRemove(id, out _);
        }
    }
}
