using LegacyAdmin_Asp.Config;
using LegacyAdmin_Asp.Models;
using LegacyAdmin_Asp.Models.Auth;
using LegacyAdmin_Asp.Services.Interface;

using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Text;

namespace LegacyAdmin_Asp.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient();

            _httpClient.Timeout =
                TimeSpan.FromSeconds(30);
        }


        // =====================================================
        // LOGIN
        // POST /api/auth/login
        // =====================================================

        public LoginResponse Login(
            string username,
            string password)
        {
            var request =
                new LoginRequest
                {
                    Username = username,
                    Password = password
                };

            string json =
                JsonConvert.SerializeObject(request);

            using (var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"))
            {
                HttpResponseMessage response =
                     _httpClient
                         .PostAsync(
                             SpringBootConfig.AuthLoginUrl,
                             content
                         )
                         .GetAwaiter()
                         .GetResult();

                string responseBody =
                    response.Content
                        .ReadAsStringAsync()
                        .GetAwaiter()
                        .GetResult();

                var apiResponse =
                    JsonConvert.DeserializeObject<
                        ApiResponse<LoginResponse>
                    >(responseBody);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(
                        apiResponse?.Message
                        ?? "Login failed."
                    );
                }

                if (apiResponse == null)
                {
                    throw new Exception(
                        "Invalid authentication response."
                    );
                }

                if (!apiResponse.Success)
                {
                    throw new Exception(
                        apiResponse.Message
                        ?? "Login failed."
                    );
                }

                if (apiResponse.Data == null)
                {
                    throw new Exception(
                        "Login data was not returned."
                    );
                }

                return apiResponse.Data;
            }
        }


        // =====================================================
        // VALIDATE SESSION
        // POST /api/sso/validate
        // =====================================================

        public bool ValidateSession(
           string sessionId)
        {
            if (
                string.IsNullOrWhiteSpace(
                    sessionId
                )
            )
            {
                return false;
            }


            var request =
                new
                {
                    sessionId = sessionId
                };


            string json =
                JsonConvert.SerializeObject(
                    request
                );


            using (var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"))
            {
                HttpResponseMessage response =
                    _httpClient
                        .PostAsync(
                            SpringBootConfig.SsoValidateUrl,
                            content
                        )
                        .GetAwaiter()
                        .GetResult();


                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }


                string responseBody =
                    response.Content
                        .ReadAsStringAsync()
                        .GetAwaiter()
                        .GetResult();


                var apiResponse =
                    JsonConvert.DeserializeObject<
                        ApiResponse<bool>
                    >(
                        responseBody
                    );


                return
                    apiResponse != null
                    &&
                    apiResponse.Success
                    &&
                    apiResponse.Data;
            }
        }


        // =====================================================
        // REFRESH TOKEN
        // POST /api/auth/refresh
        // =====================================================

        public LoginResponse RefreshToken(
            string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new Exception(
                    "Refresh token is missing."
                );
            }

            var request =
                new
                {
                    refreshToken = refreshToken
                };

            string json =
                JsonConvert.SerializeObject(request);

            using (var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"))
            {
                HttpResponseMessage response =
                        _httpClient
                            .PostAsync(
                                SpringBootConfig.AuthRefreshUrl,
                                content
                            )
                            .GetAwaiter()
                            .GetResult();

                string responseBody =
                    response.Content
                        .ReadAsStringAsync()
                        .GetAwaiter()
                        .GetResult();

                var apiResponse =
                    JsonConvert.DeserializeObject<
                        ApiResponse<LoginResponse>
                    >(responseBody);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(
                        apiResponse?.Message
                        ?? "Token refresh failed."
                    );
                }

                if (apiResponse == null ||
                    !apiResponse.Success ||
                    apiResponse.Data == null)
                {
                    throw new Exception(
                        apiResponse?.Message
                        ?? "Token refresh failed."
                    );
                }

                return apiResponse.Data;
            }
        }


        // =====================================================
        // LOGOUT
        // POST /api/auth/logout
        // =====================================================

        public void Logout(
            string refreshToken)
        {
            if (
                string.IsNullOrWhiteSpace(
                    refreshToken
                )
            )
            {
                return;
            }


            var request =
                new RefreshTokenRequest
                {
                    RefreshToken =
                        refreshToken
                };


            string json =
                JsonConvert.SerializeObject(
                    request
                );


            using (var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"))
            {
                HttpResponseMessage response =
                    _httpClient
                        .PostAsync(
                            SpringBootConfig.AuthLogoutUrl,
                            content
                        )
                        .GetAwaiter()
                        .GetResult();


                string responseBody =
                    response.Content
                        .ReadAsStringAsync()
                        .GetAwaiter()
                        .GetResult();


                var apiResponse =
                    JsonConvert.DeserializeObject<
                        ApiResponse<object>
                    >(
                        responseBody
                    );


                if (
                    !response.IsSuccessStatusCode
                    ||
                    apiResponse == null
                    ||
                    !apiResponse.Success
                )
                {
                    throw new Exception(
                        apiResponse?.Message
                        ??
                        "Logout failed."
                    );
                }
            }
        }
    }
}