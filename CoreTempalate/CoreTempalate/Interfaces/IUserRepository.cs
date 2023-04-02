using CoreTempalate.Models.EntityModels;

namespace CoreTempalate.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        void Update(User user);
    }
}
