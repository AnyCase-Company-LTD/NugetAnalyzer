﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NugetAnalyzer.BLL.Interfaces;
using NugetAnalyzer.Dtos.Models;
using NugetAnalyzer.Web.Infrastructure.HttpAccessors;
using NugetAnalyzer.Web.Infrastructure.Models;

namespace NugetAnalyzer.Web.Controllers
{
    public class GitHubAuthController : Controller
    {
        private readonly ISourceService sourceService;
        private readonly HttpContextInfoProvider httpContextInfoProvider;

        public GitHubAuthController(ISourceService sourceService, HttpContextInfoProvider httpContextInfoProvider)
        {
            this.sourceService = sourceService ?? throw new ArgumentNullException(nameof(sourceService));
            this.httpContextInfoProvider = httpContextInfoProvider ?? throw new ArgumentNullException(nameof(httpContextInfoProvider));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/GitHubAuth/Authenticate" }, OAuthSourceNames.GitHubSourceName);
        }

        [HttpGet]
        public async Task<IActionResult> Authenticate()
        {
            var sourceId = await sourceService.GetSourceIdByName(OAuthSourceNames.GitHubSourceName);

            var accessToken = await httpContextInfoProvider.GetAccessTokenAsync();

            var user = new UserRegisterModel
            {
                UserName = httpContextInfoProvider.GetUsername(),
                AvatarUrl = httpContextInfoProvider.GetAvatarUrl(),
                Url = httpContextInfoProvider.GetExternalUrl(),
                ExternalId = httpContextInfoProvider.GetExternalId(),
                AccessToken = accessToken,
                SourceId = sourceId
            };

            return RedirectToAction("GitHubLogin", "Account", user);
        }
    }
}