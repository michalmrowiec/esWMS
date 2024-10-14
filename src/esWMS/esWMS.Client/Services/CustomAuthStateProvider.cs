using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace esWMS.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        public void AuthenticateUser(string userIdentifier, string role)
        {
            var identity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, userIdentifier),
                new Claim(ClaimTypes.Role, role)
            ], "Custom Authentication");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(user)));
        }

        public void LogoutUser()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}
