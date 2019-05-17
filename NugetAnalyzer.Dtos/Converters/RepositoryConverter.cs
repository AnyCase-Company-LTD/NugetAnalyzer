﻿using System;
using System.Linq;
using NugetAnalyzer.Domain;
using NugetAnalyzer.DTOs.Models.Reports;

namespace NugetAnalyzer.DTOs.Converters
{
	public static class RepositoryConverter
	{
		public static RepositoryVersionReport RepositoryToRepositoryVersionReport(NugetAnalyzer.Domain.Repository repository)
		{
			return repository == null
				? null
				: new RepositoryVersionReport
					{
						Id = repository.Id,
						Name = repository.Name,
						Solutions = repository.Solutions == null
								? throw new ArgumentNullException(nameof(repository.Solutions))
								: repository.Solutions
									.Select(SolutionConverter.SolutionToSolutionVersionReport)
									.ToList()
					};
		}

        public static RepositoryChoice OctokitRepositoryToRepositoryChoice(Octokit.Repository repository)
        {
            return repository == null
                ? null
                : new RepositoryChoice
                {
                    Id = repository.Id,
                    Url = repository.HtmlUrl,
                    Name = repository.Name,
                    DefaultBranch = repository.DefaultBranch
                };
        }
    }
}
