﻿using System.Threading.Tasks;
using System.IO;

namespace NugetAnalyzer.BLL.Test
{
    public static class ResponseReader
    {
        private const string PathToEndpointPackageMetadata = "Response/EndpointPackageMetadata";
        private const string PathToEndpointSearch = "Response/EndpointSearch";

        public static Task<string> GetNUnitFromEndpointPackageMetadata()
        {
            var path = Path.Combine(PathToEndpointPackageMetadata, "NUnitResponse.json");
            return GetContent(path);
        }

        public static Task<string> GetNotFoundFromEndpointPackageMetadata()
        {
            var path = Path.Combine(PathToEndpointPackageMetadata, "NotFoundResponse.xml");
            return GetContent(path);
        }

        public static Task<string> GetDateFormatIncorrectFromEndpointPackageMetadata()
        {
            var path = Path.Combine(PathToEndpointPackageMetadata, "DateFormatIncorrectResponse.json");
            return GetContent(path);
        }

        public static Task<string> GetNUnitFromEndpointSearch()
        {
            var path = Path.Combine(PathToEndpointSearch, "NUnitResponse.json");
            return GetContent(path);
        }

        public static Task<string> GetIncorrectVersionFromEndpointSearch()
        {
            var path = Path.Combine(PathToEndpointSearch, "IncorrectVersionResponse.json");
            return GetContent(path);
        }

        public static Task<string> GetNotExistPackageFromEndpointSearch()
        {
            var path = Path.Combine(PathToEndpointSearch, "NotExistPackageResponse.json");
            return GetContent(path);
        }

        private static Task<string> GetContent(string path)
        {
            var streamReader = new StreamReader(path, System.Text.Encoding.Default);
            return streamReader.ReadToEndAsync();
        }
    }
}