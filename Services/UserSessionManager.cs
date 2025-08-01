using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Telerik.SvgIcons;

namespace BlazorApp1.Services
{
    public class UserSessionManager
    {


        public string? Username { get; set; }

        public Role? Role { get; set; }

        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        // public bool IsAuthenticated => !string.IsNullOrEmpty(Username);
        public bool IsAuthenticated => !string.IsNullOrEmpty(AccessToken);

        public event Action? OnUserChanged;

        public void SetUser(string? username, Role? role)
        {

            Username = username;
            Role = role;
            OnUserChanged?.Invoke();
        }

        public void SetTokens(string? accessToken, string? refreshToken)
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
            OnUserChanged?.Invoke();
        }

        public void SignOut()
        {

            Username = null;
            Role = null;
            AccessToken = null;
            RefreshToken = null;
            OnUserChanged?.Invoke();
        }



    }
}
