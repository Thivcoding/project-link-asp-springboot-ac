using LegacyAdmin_Asp.Config;
using LegacyAdmin_Asp.Models;
using LegacyAdmin_Asp.Models.Sso;
using LegacyAdmin_Asp.Services.Interface;

using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Text;

namespace LegacyAdmin_Asp.Services
{
    public class SsoService : ISsoService
    {
        private readonly HttpClient _httpClient;

        public SsoService()
        {
            _httpClient = new HttpClient();

            _httpClient.Timeout =
                TimeSpan.FromSeconds(30);
        }


        // =====================================================
        // ISSUE SSO TICKET
        // =====================================================

        public SsoIssueResponse IssueTicket(
            string sessionId
        )
        {
            var request = new
            {
                sessionId = sessionId
            };


            string json =
                JsonConvert.SerializeObject(
                    request
                );


            using (
                var content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"
                    )
            )
            {
                HttpResponseMessage response =
                    _httpClient
                        .PostAsync(
                            SpringBootConfig.SsoIssueUrl,
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
                        ApiResponse<SsoIssueResponse>
                    >(
                        responseBody
                    );


                if (
                    !response.IsSuccessStatusCode ||
                    apiResponse == null ||
                    !apiResponse.Success
                )
                {
                    throw new Exception(
                        apiResponse?.Message
                        ?? "Failed to issue SSO ticket."
                    );
                }


                return apiResponse.Data;
            }
        }
    }
}