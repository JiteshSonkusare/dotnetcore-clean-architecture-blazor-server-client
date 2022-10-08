using MediatR;
using Shared.Wrapper;
using Domain.Entities;
using Application.Interfaces.Repositories;

namespace Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IUserRepository _userRepository;
        

        public DeleteUserCommandHandler(IUnitOfWork<Guid> unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
            if (user != null)
            {
                await _unitOfWork.Repository<User>().DeleteAsync(user);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(user.Id, "User Deleted");
            }
            else
            {
                return await Result<Guid>.FailAsync(request.Id,"User Not Found!");
            }
        }
    }
}
