using MediatR;
using Domain.Entities;
using Shared.Wrapper;
using Application.Interfaces.Repositories;
using AutoMapper;
using Application.Features.Users.Queries.ViewModels;

namespace Application.Features.Users.Queries.GetById
{
    public class GetUserByIdQuery : IRequest<Result<UserViewModel>>
    {
        public Guid Id { get; set; }
    }

    internal class GetUserQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserViewModel>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
                var mappeduser = _mapper.Map<UserViewModel>(user);
                return await Result<UserViewModel>.SuccessAsync(mappeduser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get User data! Error: {ex.Message}");
            }
        }
    }
}
