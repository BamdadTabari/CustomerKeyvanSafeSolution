using AutoMapper;
using KeyvanSafe.Shared.Assistant.Helpers;
using KeyvanSafe.Shared.Certain.Constants;
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


        #region User-UserDto

        CreateMap<User, UserDto>();

        CreateMap<UserDto, User>().AfterMap((src, dest) =>
            {
                dest.SecurityStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength);
                dest.ConcurrencyStamp = StampGenerator.CreateSecurityStamp(Defaults.ConcurrencyStampLength);
                dest.PasswordHash = PasswordHasher.Hash(src.Password);
            }
        );

        #endregion


        CreateMap<UserRoleDto, UserRole>().ReverseMap();
        CreateMap<RolePermissionDto, RolePermission>().ReverseMap();
        CreateMap<ClaimDto, Claim>().ReverseMap();
    }
}