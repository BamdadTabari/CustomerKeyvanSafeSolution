﻿using AutoMapper;
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
using KeyvanSafe.Shared.Models.Results.Identity;
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

    #region User

    [HttpPost(IdentityRoutes.Users)]
    //[CreateUserResultFilter]
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

            var operation = new OperationResult(OperationResultStatusEnum.Ok, isPersistAble: true, value: entity);
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
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserRequest request)
    {
        var user = await _unitOfWorkIdentity.Users.GetUserByIdAsync(id);

        if (user == null)
        {
            var operation = new OperationResult(OperationResultStatusEnum.UnProcessable,
                value: GenericResponses.SendResponse("کاربری با این آی دی یافت نشد", OperationResultStatusEnum.UnProcessable));
            return this.ReturnResponse(operation);
        }
             

        if (user.PasswordHash != PasswordHasher.Hash(request.Password))
            return new OperationResult(OperationResultStatus.UnProcessable, value: GenericErrors<User>.InvalidVariableError("password"));

        // Update
        user.Mobile = request.Mobile;
        user.Email = request.Email;
        user.Username = request.Username;
        if (user.Email != request.Email)
            user.IsEmailConfirmed = false;
        if (user.Mobile != request.Mobile)
            user.IsMobileConfirmed = false;

        _unitOfWork.Users.Update(user);

        return new OperationResult(OperationResultStatus.Ok, isPersistAble: true, value: user);
        return this.ReturnResponse(operation);
    }

    //[HttpGet(IdentityRoutes.Users + "{ueid}")]
    //[GetUserByIdResultFilter]
    //public async Task<IActionResult> GetUserById([FromRoute] string ueid)
    //{
    //    var userId = ueid.DecodeInt();

    //    var operation = await _mediator.Send(new GetUserByIdQuery(Request.GetRequestInfo())
    //    {
    //        UserId = userId
    //    });

    //    return this.ReturnResponse(operation);
    //}


    //// Patch Password
    //[HttpPatch(IdentityRoutes.Users + "{ueid}/password")]
    //[UpdateUserResultFilter]
    //public async Task<IActionResult> UpdateUserPassword([FromRoute] string ueid, [FromBody] UpdateUserPasswordRequest request)
    //{
    //    var userId = ueid.DecodeInt();

    //    var operation = await _mediator.Send(new UpdateUserPasswordCommand(Request.GetRequestInfo())
    //    {
    //        UserId = userId,
    //        NewPassword = request.NewPassword
    //    });

    //    return this.ReturnResponse(operation);
    //}

    #endregion

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
