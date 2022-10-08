using MediatR;
using Application.Features.Users.Queries.GetAll;
using Application.Features.Users.Queries.ViewModels;

namespace WebAPI.GraphQL.Queries
{
    public class UserQuery
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<UserViewModel>> GetUsers([Service] IMediator _mediator)
        {
            var data = await _mediator.Send(new GetAllUserQuery());
            return data.Data.AsQueryable();
        }
    }
}