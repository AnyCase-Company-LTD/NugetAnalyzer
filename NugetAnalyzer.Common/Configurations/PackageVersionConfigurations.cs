﻿using NugetAnalyzer.DTOs.Models.Enums;

namespace NugetAnalyzer.Common.Configurations
{
    public class PackageVersionConfigurations
    {
        public VersionConfigs VersionStatus { get; set; }
        public DateBordersInMonthsConfigs DateBordersInMonths { get; set; }
        public int ObsoleteBorderInMonths { get; set; }
    }

    public class DateBordersInMonthsConfigs
    {
        public int WarningBottomBorder { get; set; }
        public int ErrorBottomBorder { get; set; }
    }

    public class VersionConfigs
    {
        public PackageVersionStatus Major { get; set; }
        public PackageVersionStatus Minor { get; set; }
        public PackageVersionStatus Build { get; set; }
        public PackageVersionStatus Revision { get; set; }
    }
}
