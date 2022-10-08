using System.Net;
using Client.Infrastructure.Routes;
using Client.Infrastructure.Client;
using Client.Infrastructure.ViewModels;
using Client.Infrastructure.Exceptions;
using Client.Infrastructure.Configuration;
using Client.Infrastructure.Authentication.Interfaces;

namespace Client.Infrastructure.ApiClientManagers
{
    public class UserApiClientManager : ApiClientBase
    {
        public UserApiClientManager(IClientConfig config, IAuthHandler authHandler) : base(config, authHandler)
        {
            if (string.IsNullOrWhiteSpace(config?.BaseUrl))
            {
                throw new ArgumentNullException(nameof(config.BaseUrl));
            }
        }

        public async Task<Response<UserViewModel>?> GetAllAsync()
        {
            Response<UserViewModel>? response = null;
            try
            {
                await Send(new Uri(UserEndpoints.GetAll, UriKind.Relative),
                        HttpMethod.Get,
                        CancellationToken.None,
                        result =>
                        {
                            if (result.StatusCode == (int)HttpStatusCode.OK)
                                response = new Response<UserViewModel>(result, result.StatusCode);
                            else
                                throw new ApiException("Get User Failed", 400, result.StatusCode);
                        });

                return response;
            }
            catch (Exception ex)
            {
                throw new GeneralApplicationException("Get Users Failed!", ex);
            }
        }
    }
}
