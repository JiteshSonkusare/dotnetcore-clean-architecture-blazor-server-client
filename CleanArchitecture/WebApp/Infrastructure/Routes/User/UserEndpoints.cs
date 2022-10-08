namespace WebApp.Infrastructure.Routes.User
{
	public class UserEndpoints
	{
		private static string version = "v1";

		public static string GetAll = $"api/{version}/User";

		public static string GetById(Guid id)
		{
			return $"api/{version}/User/{id}";
		}

		public static string Upsert = $"api/{version}/User";

		public static string Delete(Guid id)
		{
			return $"api/{version}/user/{id}";
		}
	}
}
