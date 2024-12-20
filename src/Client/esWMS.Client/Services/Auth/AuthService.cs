﻿using esWMS.Client.Services.LocalStorage;
using esWMS.Client.ViewModels.SystemActors;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace esWMS.Client.Services.Auth
{
    public interface IAuthService
    {
        Task Login(LogedEmployee logedEmployee);
        Task Logout();
        Task<bool> CheckLogin();
        Task<ClaimsPrincipal> GetAuthenticationStateAsync();
        Task<string> GetJwtToken();
    }

    public class AuthService : IAuthService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;

        public AuthService(AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }

        public async Task<ClaimsPrincipal> GetAuthenticationStateAsync()
        {
            var authState = await ((CustomAuthStateProvider)_authenticationStateProvider)
                .GetAuthenticationStateAsync();
            return authState.User;
        }

        public async Task<bool> CheckLogin()
        {
            var logedEmployee = await _localStorageService.GetItemAsync<LogedEmployee>("jwt");
            if (logedEmployee != null)
            {
                if (JwtHelper.IsTokenExpired(logedEmployee.JwtToken))
                {
                    await _localStorageService.RemoveItemAsync("jwt");
                    return false;
                }

                ((CustomAuthStateProvider)_authenticationStateProvider)
                    .AuthenticateUser(logedEmployee.EmployeeId, logedEmployee.RoleId);

                return true;
            }

            return false;
        }

        public async Task Login(LogedEmployee logedEmployee)
        {
            ((CustomAuthStateProvider)_authenticationStateProvider)
                .AuthenticateUser(logedEmployee.EmployeeId, logedEmployee.RoleId);

            await _localStorageService.SetItemAsync("jwt", logedEmployee);
        }

        public async Task Logout()
        {
            ((CustomAuthStateProvider)_authenticationStateProvider)
                .LogoutUser();

            await _localStorageService.RemoveItemAsync("jwt");
        }

        public async Task<string> GetJwtToken()
        {
            var logedData = await _localStorageService.GetItemAsync<LogedEmployee>("jwt");
            return logedData.JwtToken;
        }
    }
}
