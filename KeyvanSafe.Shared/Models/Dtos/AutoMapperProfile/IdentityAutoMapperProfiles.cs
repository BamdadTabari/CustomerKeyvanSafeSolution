﻿using AutoMapper;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Permissions;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Roles;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using KeyvanSafe.Shared.Models.Dtos.Identity.ClaimDtos;
using KeyvanSafe.Shared.Models.Dtos.Identity.PermissionDtos;
using KeyvanSafe.Shared.Models.Dtos.Identity.RoleDtos;
using KeyvanSafe.Shared.Models.Dtos.Identity.UserDtos;
using System.Security.Claims;

namespace KeyvanSafe.Shared.Models.Dtos.AutoMapperProfile;

public class IdentityAutoMapperProfiles : Profile
{
    public IdentityAutoMapperProfiles()
    {
        CreateMap<PermissionDto, Permission>().ReverseMap();

        CreateMap<RoleDto, Role>().ReverseMap();

        CreateMap<UserDto, User>().ReverseMap();

        CreateMap<UserRoleDto, UserRole>().ReverseMap();

        CreateMap<RolePermissionDto, RolePermission>().ReverseMap();

        CreateMap<ClaimDto, Claim>().ReverseMap();
    }
}