﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NugetAnalyzer.Domain;

namespace NugetAnalyzer.DAL.Interfaces
{
    public interface IVersionRepository
    {
        Task<IReadOnlyCollection<PackageVersion>> GetLatestPackageVersionsAsync(ICollection<int> packageIds);
        Task<IReadOnlyCollection<PackageVersion>> GetLatestVersionsAsync(Expression<Func<PackageVersion, bool>> predicate);

        Task<IReadOnlyCollection<PackageVersion>> GetAllLatestVersionsAsync();
    }
}
