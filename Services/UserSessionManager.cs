using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace BlazorApp1.Services
{
    public class UserSessionManager
    {

       
        public string? Username { get; set; }

        public Role? Role { get; set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(Username);

        public event Action? OnUserChanged;

        public void SetUser(string? username, Role? role)
        {

            Username = username;
            Role = role;
            OnUserChanged?.Invoke();
        }

        public void SignOut()
        {

            Username = null;
            Role = null;
            OnUserChanged?.Invoke();
        }

        

    }
}
