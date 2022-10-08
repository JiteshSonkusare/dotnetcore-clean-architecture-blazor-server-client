using System.Net;
using WebApp.Models;
using WebApp.Exceptions;
using WebApp.Models.Authentication;
using WebApp.Infrastructure.Routes.User;
using WebApp.Infrastructure.Authentication.Client;
using WebApp.Infrastructure.Authentication.Security.Interfaces;

namespace WebApp.Infrastructure.ApiClient
{
	public class UserApiClient : ApiClientBase
	{
		public UserApiClient(IApiClientConfig config, IAuthHandler authHandler) : base(config, authHandler)
		{
			if (string.IsNullOrWhiteSpace(config?.BaseUrl))
			{
				throw new ArgumentNullException(nameof(config.BaseUrl));
			}
		}

		public async Task<Response<UserViewModel>?> GetAllAsync()
		{
			Response<UserViewModel>? response = null;
			string endpointUri = UserEndpoints.GetAll;

			try
			{
				await Send(new Uri(endpointUri, UriKind.Relative),
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
