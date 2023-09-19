using AutoMapper;
using KeyvanSafe.Domain.EntityFramework.Interfaces.IUnitOfWorks;
using KeyvanSafe.Domain.Specifications.Identity;
using KeyvanSafe.Shared.Assistant.Extension;
using KeyvanSafe.Shared.Assistant.Helpers;
using KeyvanSafe.Shared.Certain.Constants;
using KeyvanSafe.Shared.Certain.Enums;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using KeyvanSafe.Shared.Infrastructure.Errors;
using KeyvanSafe.Shared.Infrastructure.Operations;
using KeyvanSafe.Shared.Infrastructure.Routes;
using KeyvanSafe.Shared.Models.Dtos.Identity.UserDtos;
using KeyvanSafe.Shared.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace KeyvanSafe.Server.Controllers.Identity;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUnitOfWorkIdentity _unitOfWorkIdentity;
    private readonly IMapper _mapper;
    public UserController(IUnitOfWorkIdentity unitOfWorkIdentity, IMapper mapper)
    {
        _unitOfWorkIdentity = unitOfWorkIdentity;
        _mapper = mapper;
    }

    [HttpPost(IdentityRoutes.Users)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var isExist = await _unitOfWorkIdentity.Users
            .ExistsAsync(new DuplicateUserSpecificationFile(request.UserName).ToExpression());
        if (isExist)
        {
            var response = new OperationResult(OperationResultStatusEnum.UnProcessable,
               value: GenericResponses.SendResponse("نام کاربری تکراری است", OperationResultStatusEnum.Invalidated));
            return this.ReturnResponse(response);
        }

        var dto = new UserDto
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            FullName = request.FirstName + " " + request.LastName,
            Password = request.Password,
            State = UserStateEnum.Suspended,
            Email = request.Email,
            Mobile = request.Mobile,
            IsMobileConfirmed = false,
            FailedLoginCount = 0,
            IsLockedOut = false,
            LastLoginDate = DateTime.UtcNow,
            LastPasswordChangeTime = DateTime.UtcNow,
            LockoutEndTime = DateTime.UtcNow,
            PasswordHash = PasswordHasher.Hash(request.Password),
            ConcurrencyStamp = StampGenerator.CreateSecurityStamp(Defaults.ConcurrencyStampLength),
            SecurityStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        try
        {
            var entity = _mapper.Map<User>(dto);
           
            await _unitOfWorkIdentity.Users.AddAsync(entity);
            await _unitOfWorkIdentity.CommitAsync();

            var operation = new OperationResult(OperationResultStatusEnum.Ok, isPersistAble: true,
                value: GenericResponses.SendResponse("عملیات با موفقیت انجام شد", OperationResultStatusEnum.Ok));
            return this.ReturnResponse(operation);
        }
        catch
        {
            var operation = new OperationResult(OperationResultStatusEnum.UnProcessable, isPersistAble: true,
                value: GenericResponses.SendResponse("خطایی در سمت سرور رخ داده است", OperationResultStatusEnum.UnProcessable));
            return this.ReturnResponse(operation);
        }
    }

    [HttpPut(IdentityRoutes.Users + "{id}")]
    public async Task<IActionResult> UpdateUserInfo([FromRoute] int id, [FromBody] UpdateUserRequest request)
    {
        var user = await _unitOfWorkIdentity.Users.GetUserByIdAsync(id);

        if (user == null)
        {
            var operation = new OperationResult(OperationResultStatusEnum.NotFound,
                value: GenericResponses.SendResponse("کاربری با این آی دی یافت نشد", OperationResultStatusEnum.NotFound));
            return this.ReturnResponse(operation);
        }
             

        if (user.PasswordHash != PasswordHasher.Hash(request.Password))
        {
            var operation = new OperationResult(OperationResultStatusEnum.Invalidated,
               value: GenericResponses.SendResponse(" رمز عبور اشتباه است ", OperationResultStatusEnum.Invalidated));
            return this.ReturnResponse(operation);
        }
        // Update
        user.Email = request.Email;
        user.UserName = request.UserName;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.FullName = request.FirstName + " " + request.LastName;
        user.Email = request.Email;
        user.UpdatedAt = DateTime.UtcNow;

        try
        {
            _unitOfWorkIdentity.Users.Update(user);
            await _unitOfWorkIdentity.CommitAsync();

            var operation = new OperationResult(OperationResultStatusEnum.Ok, isPersistAble: true,
                value: GenericResponses.SendResponse("عملیات با موفقیت انجام شد", OperationResultStatusEnum.Ok));
            return this.ReturnResponse(operation);
        }
        catch
        {
            var operation = new OperationResult(OperationResultStatusEnum.UnProcessable, isPersistAble: true,
                value: GenericResponses.SendResponse("خطایی در سمت سرور رخ داده است", OperationResultStatusEnum.UnProcessable));
            return this.ReturnResponse(operation);
        }
    }

    [HttpGet(IdentityRoutes.Users + "{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        var user = await _unitOfWorkIdentity.Users.GetUserByIdAsync(id);

        if (user == null)
        {
            var operation = new OperationResult(OperationResultStatusEnum.NotFound,
                value: GenericResponses.SendResponse("کاربری با این آی دی یافت نشد", OperationResultStatusEnum.NotFound));
            return this.ReturnResponse(operation);
        }

        try
        {
            var dto = _mapper.Map<UserDto>(user);
            var operation = new OperationResult(OperationResultStatusEnum.Ok, isPersistAble: true,
                value: dto);
            return this.ReturnResponse(operation);
        }
        catch
        {
            var operation = new OperationResult(OperationResultStatusEnum.UnProcessable, isPersistAble: true,
                value: GenericResponses.SendResponse("خطایی در سمت سرور رخ داده است", OperationResultStatusEnum.UnProcessable));
            return this.ReturnResponse(operation);
        }
    }

    [HttpPatch(IdentityRoutes.Users + "{id}/update-mobile")]
    public async Task<IActionResult> UpdateUserMobileNumber([FromRoute] int id, [FromBody] UpdateUserMobileRequest request)
    {
        var user = await _unitOfWorkIdentity.Users.GetUserByIdAsync(id);

        if (user == null)
        {
            var operation = new OperationResult(OperationResultStatusEnum.NotFound,
                value: GenericResponses.SendResponse("کاربری با این آی دی یافت نشد", OperationResultStatusEnum.NotFound));
            return this.ReturnResponse(operation);
        }


        if (user.PasswordHash != PasswordHasher.Hash(request.Password))
        {
            var operation = new OperationResult(OperationResultStatusEnum.Invalidated,
               value: GenericResponses.SendResponse(" رمز عبور اشتباه است ", OperationResultStatusEnum.Invalidated));
            return this.ReturnResponse(operation);
        }
        // Update Mobile Number
        user.Mobile = request.NewMobileNumber;
        if (user.Mobile != request.NewMobileNumber)
            user.IsMobileConfirmed = false;

        try
        {
            _unitOfWorkIdentity.Users.Update(user);
            await _unitOfWorkIdentity.CommitAsync();

            var operation = new OperationResult(OperationResultStatusEnum.Ok, isPersistAble: true,
                value: GenericResponses.SendResponse("عملیات با موفقیت انجام شد", OperationResultStatusEnum.Ok));
            return this.ReturnResponse(operation);
        }
        catch
        {
            var operation = new OperationResult(OperationResultStatusEnum.UnProcessable, isPersistAble: true,
                value: GenericResponses.SendResponse("خطایی در سمت سرور رخ داده است", OperationResultStatusEnum.UnProcessable));
            return this.ReturnResponse(operation);
        }
    }

    // Patch Password
    [HttpPatch(IdentityRoutes.Users + "{id}/update-password")]
    public async Task<IActionResult> UpdateUserPassword([FromRoute] int id, [FromBody] UpdateUserPasswordRequest request)
    {
        var user = await _unitOfWorkIdentity.Users.GetUserByIdAsync(id);

        if (user == null)
        {
            var operation = new OperationResult(OperationResultStatusEnum.NotFound,
                value: GenericResponses.SendResponse("کاربری با این آی دی یافت نشد", OperationResultStatusEnum.NotFound));
            return this.ReturnResponse(operation);
        }

        if (user.PasswordHash != PasswordHasher.Hash(request.LastPassword))
        {
            var operation = new OperationResult(OperationResultStatusEnum.Invalidated,
               value: GenericResponses.SendResponse(" رمز عبور اشتباه است ", OperationResultStatusEnum.Invalidated));
            return this.ReturnResponse(operation);
        }

        if (request.NewPassword != request.RetypeNewPassword)
        {
            var operation = new OperationResult(OperationResultStatusEnum.Invalidated,
               value: GenericResponses.SendResponse(" رمز عبور جدید با تکرار آن مطابقت ندارد ", OperationResultStatusEnum.Invalidated));
            return this.ReturnResponse(operation);
        }

        try
        {
            // Update Password
            user.Mobile = PasswordHasher.Hash(request.NewPassword);

            _unitOfWorkIdentity.Users.Update(user);
            await _unitOfWorkIdentity.CommitAsync();

            var operation = new OperationResult(OperationResultStatusEnum.Ok, isPersistAble: true,
                value: GenericResponses.SendResponse("عملیات با موفقیت انجام شد", OperationResultStatusEnum.Ok));
            return this.ReturnResponse(operation);
        }
        catch
        {
            var operation = new OperationResult(OperationResultStatusEnum.UnProcessable, isPersistAble: true,
                value: GenericResponses.SendResponse("خطایی در سمت سرور رخ داده است", OperationResultStatusEnum.UnProcessable));
            return this.ReturnResponse(operation);
        }
    }

    //#region Role

    //[HttpPatch(IdentityRoutes.Users + "{ueid}/roles")]
    //[UpdateUserRolesResultFilter]
    //public async Task<IActionResult> UpdateUserRoles([FromRoute] string ueid, [FromBody] UpdateUserRolesRequest request)
    //{
    //    var userId = ueid.DecodeInt();
    //    var roleIds = request.RoleEids?.Select(x => x.DecodeInt());

    //    var operation = await _mediator.Send(new UpdateUserRolesCommand(Request.GetRequestInfo())
    //    {
    //        UserId = userId,
    //        RoleIds = roleIds ?? Array.Empty<int>()
    //    });

    //    return this.ReturnResponse(operation);
    //}

    //#endregion

    //#region Permission

    //[HttpPost(IdentityRoutes.Users + "{ueid}/permissions/{peid}")]
    //[CreateUserPermissionResultFilter]
    //public async Task<IActionResult> CreateUserPermission([FromRoute] string ueid, [FromRoute] string peid)
    //{
    //    var userId = ueid.DecodeInt();
    //    var permissionId = peid.DecodeInt();

    //    var operation = await _mediator.Send(new CreateUserPermissionCommand(Request.GetRequestInfo())
    //    {
    //        UserId = userId,
    //        PermissionId = permissionId
    //    });

    //    return this.ReturnResponse(operation);
    //}

    //[HttpDelete(IdentityRoutes.Users + "permission/{ceid}")]
    //[DeleteUserPermissionResultFilter]
    //public async Task<IActionResult> DeleteUserPermission([FromRoute] string ceid)
    //{
    //    var claimId = ceid.DecodeInt();

    //    var operation = await _mediator.Send(new DeleteUserPermissionCommand(Request.GetRequestInfo())
    //    {
    //        ClaimId = claimId
    //    });

    //    return this.ReturnResponse(operation);
    //}

    //#endregion
}
