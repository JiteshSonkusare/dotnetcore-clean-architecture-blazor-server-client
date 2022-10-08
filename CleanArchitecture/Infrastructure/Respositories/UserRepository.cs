using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Infrastructure.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepositoryAsync<User, Guid> _repository;

        public UserRepository(IRepositoryAsync<User, Guid> repository)
        {
            _repository = repository;
        }
    }
}
