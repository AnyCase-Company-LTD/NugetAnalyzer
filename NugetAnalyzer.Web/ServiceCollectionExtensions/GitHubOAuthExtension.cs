﻿using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using NugetAnalyzer.Web.Models;
using NugetAnalyzer.Web.Options;

namespace NugetAnalyzer.Web.ServiceCollectionExtensions
{
    public static class GitHubOAuthExtension
    {
        public static void AddGitHubOAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var secretsSection = configuration.GetSection("GitHubAppSettings");
            var endPointsSection = configuration.GetSection("GitHubEndPoints");
            services.Configure<GitHubSecretsOptions>(endPointsSection);
            services.Configure<GitHubEndPointsOptions>(secretsSection);

            var endPoints = endPointsSection.Get<GitHubEndPointsOptions>();
            var secrets = secretsSection.Get<GitHubSecretsOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OAuthSourceNames.GitHubSourceName;
            })
           .AddCookie()
           .AddOAuth(OAuthSourceNames.GitHubSourceName, options =>
           {
               options.ClientId = secrets.ClientId;
               options.ClientSecret = secrets.ClientSecret;
               options.CallbackPath = secrets.CallbackPath;

               options.AuthorizationEndpoint = endPoints.AuthorizationEndpoint;
               options.TokenEndpoint = endPoints.TokenEndpoint;
               options.UserInformationEndpoint = endPoints.UserInformationEndpoint;

               options.ClaimActions.MapJsonKey(NugetAnalyzerClaimTypes.ExternalIdClaimType, GitHubUserClaimTypes.Id);
               options.ClaimActions.MapJsonKey(NugetAnalyzerClaimTypes.UserNameClaimType, GitHubUserClaimTypes.UserName);
               options.ClaimActions.MapJsonKey(NugetAnalyzerClaimTypes.GithubUrlClaimType, GitHubUserClaimTypes.Url);
               options.ClaimActions.MapJsonKey(NugetAnalyzerClaimTypes.AvatarUrlClaimType, GitHubUserClaimTypes.AvatarUrl);
               options.ClaimActions.MapJsonKey(NugetAnalyzerClaimTypes.SourceNameClaimType, NugetAnalyzerClaimTypes.SourceNameClaimType);

               options.Scope.Add(GitHubScopes.Repo);

               options.SaveTokens = true;

               options.Events = new OAuthEvents
               {
                   OnCreatingTicket = async context =>
                   {
                       var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                       request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                       request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                       var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                       response.EnsureSuccessStatusCode();
                       var jObjectResponse = await response.Content.ReadAsStringAsync();
                       var user = JObject.Parse(jObjectResponse);
                       user.Add(NugetAnalyzerClaimTypes.SourceNameClaimType, OAuthSourceNames.GitHubSourceName);
                       context.RunClaimActions(user);
                   }
               };
           });
        }
    }
}
