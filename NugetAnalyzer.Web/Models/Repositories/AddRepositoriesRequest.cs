﻿using System.Collections.Generic;

namespace NugetAnalyzer.Web.Models.Repositories
{
    public class AddRepositoriesRequest
    {
        public Dictionary<string, string> Repositories { get; set; }

        public bool IsFromLayout { get; set; }
    }
}
