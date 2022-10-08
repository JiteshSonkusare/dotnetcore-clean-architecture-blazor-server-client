using MediatR;
using AutoMapper;
using Shared.Wrapper;
using Domain.Entities;
using Application.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.Commands.UpsertUser
{
    public class UpsertUserCommand : IRequest<Result<Guid>>
    {
        public Guid? Id          { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName  { get; set; }
        [Required]
        public string? UserId    { get; set; }
        public int? Mobile       { get; set; }
        public int? Phone        { get; set; }
        public string? Address   { get; set; }
        [Required]
        public string? Status    { get; set; }
    }

    public class UpsertUserCommandHandler : IRequestHandler<UpsertUserCommand, Result<Guid>>
    {
        private readonly IUnitOfWork<Guid> _unitOfWork;
        private readonly IMapper _mapper;

        public UpsertUserCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(UpsertUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty || request.Id == null)
            {
                var entity = _mapper.Map<User>(request);
                await _unitOfWork.Repository<User>().AddAsync(entity);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<Guid>.SuccessAsync(entity.Id, "User Created Successfully!");
            }
            else
            {
                var entity = await _unitOfWork.Repository<User>().GetByIdAsync((Guid)request.Id);
                if (entity != null)
                {
                    entity.FirstName = request.FirstName ?? entity.FirstName;
                    entity.LastName  = request.LastName  ?? entity.LastName;
                    entity.UserId    = request.UserId    ?? entity.UserId;
                    entity.Phone     = request.Phone     ?? entity.Phone;
                    entity.Mobile    = request.Mobile    ?? entity.Mobile;
                    entity.Address   = request.Address   ?? entity.Address;
                    entity.Status    = request.Status    ?? entity.Status;

                    await _unitOfWork.Repository<User>().UpdateAsync(entity);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<Guid>.SuccessAsync(entity.Id, "User Updated Successfully!");
                }
                else
                {
                    return await Result<Guid>.FailAsync((Guid)request.Id, "User Not Found!");
                }
            }
        }
    }
}
