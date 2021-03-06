﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NugetAnalyzer.Domain;

namespace NugetAnalyzer.DAL.EntityConfigurations
{
    public class SolutionConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.ToTable("Solutions");

            builder
                .Property(solution => solution.Name)
                .IsRequired()
                .HasMaxLength(4096);

            builder
                .Property(solution => solution.RepositoryId)
                .IsRequired();
        }
    }
}