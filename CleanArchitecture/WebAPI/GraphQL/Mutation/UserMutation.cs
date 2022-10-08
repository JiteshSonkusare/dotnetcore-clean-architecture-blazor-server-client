using MediatR;
using Wrapper = Shared.Wrapper;
using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.UpsertUser;

namespace WebAPI.GraphQL.Mutation
{
    public class UserMutation
    {
        public async Task<Wrapper.Result<Guid>> UpsertUser(UpsertUserCommand command, [Service] IMediator _mediator)
        {
            return await _mediator.Send(command, CancellationToken.None);
        }

        public async Task<Wrapper.Result<Guid>> DeleteUser(DeleteUserCommand command, [Service] IMediator _mediator)
        {
            return await _mediator.Send(command, CancellationToken.None);
        }
    }
}