﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NugetAnalyzer.BLL.Interfaces;
using NugetAnalyzer.Common;

namespace NugetAnalyzer.BLL.Services
{
    public class NugetHttpService : INugetHttpService
    {
        private readonly HttpClient httpClient;
        private readonly NugetSettings nugetSettings;

        public NugetHttpService(HttpClient httpClient, IOptions<NugetSettings> nugetSettings)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            if (nugetSettings == null)
            {
                throw new ArgumentNullException(nameof(nugetSettings));
            }
            
            this.nugetSettings = nugetSettings.Value;
        }

        public async Task<string> GetPackageVersionMetadataAsync(string packageName, string version)
        {
            var url = $"{nugetSettings.PackageMetadata}/v3/registration3/{packageName.ToLowerInvariant()}/{version}.json";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetPackageMetadataAsync(string query)
        {
            var url = $"{nugetSettings.Search}/query?q=PackageId:{WebUtility.UrlEncode(query ?? string.Empty)}&prerelease=false";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}