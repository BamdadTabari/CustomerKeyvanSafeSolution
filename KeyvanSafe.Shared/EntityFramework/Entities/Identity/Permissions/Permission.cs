﻿using Dayana.Shared.Basic.ConfigAndConstants.Constants;
using Dayana.Shared.Basic.MethodsAndObjects.Models;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeyvanSafe.Shared.EntityFramework.Entities.Identity.Permissions;

public class Permission : BaseEntity
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Value { get; set; }

    #region Navigations

    public ICollection<RolePermission> Roles { get; set; }

    #endregion
}

public class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);

        #region Mappings

        builder.Property(b => b.Value)
            .HasMaxLength(Defaults.NameLength)
            .IsRequired();

        builder.Property(b => b.Name)
            .HasMaxLength(Defaults.MaxTitleLength);

        #endregion
    }
}