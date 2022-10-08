namespace Client.Infrastructure.Routes
{
    public class UserEndpoints
    {
        private static readonly string Version = "v1";

        public static readonly string GetAll = $"api/{Version}/User";

        public static string GetById(Guid id) => $"api/{Version}/User/{id}";

        public static readonly string Upsert = $"api/{Version}/User";

        public static string Delete(Guid id) => $"api/{Version}/user/{id}";
    }
}