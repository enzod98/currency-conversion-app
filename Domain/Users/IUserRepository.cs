using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Users;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
    Task<bool> ExistsByEmailAsync(string email);
}
