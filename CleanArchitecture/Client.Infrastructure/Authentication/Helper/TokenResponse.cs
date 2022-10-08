using Client.Infrastructure.Authentication.Interfaces;

namespace Client.Infrastructure.Authentication.Helper
{
    internal sealed class TokenResponse : IAuthToken
    {
        /// <summary>
        /// Token value
        /// </summary>
        public string Access_Token { get; set; } = string.Empty;

        /// <summary>
        /// Token type
        /// </summary>
        public string Token_Type { get; set; } = string.Empty;

        string IAuthToken.Scheme
        {
            get => Token_Type;
            set => Token_Type = value;
        }

        string IAuthToken.Value
        {
            get => Access_Token;
            set => Access_Token = value;
        }
    }
}
