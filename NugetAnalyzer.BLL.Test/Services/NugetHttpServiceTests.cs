﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NugetAnalyzer.BLL.Services;
using NugetAnalyzer.Common;
using NUnit.Framework;

namespace NugetAnalyzer.BLL.Test.Services
{
    [TestFixture(Category = "UnitTests")]
    public class NugetHttpServiceTests
    {
        private NugetHttpService nugetHttpService;
        private Mock<HttpMessageHandler> handlerMock;
        private NugetSettings nugetSettings;

        [OneTimeSetUp]
        public void Init()
        {
            nugetSettings = new NugetSettings
            {
                PackageMetadata = "https://api/test/metadata.nuget.org",
                Search = "https://api/test/search.nuget.org",
            };

            Mock<IOptions<NugetSettings>> optionsMock = new Mock<IOptions<NugetSettings>>();
            optionsMock
                .Setup(options => options.Value)
                .Returns(nugetSettings);

            handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{'id':1,'value':'1'}]"),
                });

            HttpClient httpClient = new HttpClient(handlerMock.Object);

            nugetHttpService = new NugetHttpService(httpClient, optionsMock.Object);
        }

        [Test]
        public async Task GetPackageMetadataAsync_Should_Invoke_SendAsync()
        {
            string packageName = "NUnit";
            string version = "4.0.1";

            await nugetHttpService.GetPackageVersionMetadataAsync(packageName, version);

            Uri expectedUri = new Uri($"{nugetSettings.PackageMetadata}/v3/registration3/{packageName.ToLowerInvariant()}/{version}.json");

            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(httpRequestMessage =>
                        httpRequestMessage.Method == HttpMethod.Get 
                            && httpRequestMessage.RequestUri == expectedUri
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public async Task GetDataOfPackageVersionAsync_Should_Invoke_SendAsync()
        {
            string packageName = "NUnit";

            await nugetHttpService.GetPackageMetadataAsync(packageName);

            Uri expectedUri = new Uri($"{nugetSettings.Search}/query?q=PackageId:{WebUtility.UrlEncode(packageName)}&prerelease=false");

            handlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(httpRequestMessage =>
                        httpRequestMessage.Method == HttpMethod.Get
                        && httpRequestMessage.RequestUri == expectedUri
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}