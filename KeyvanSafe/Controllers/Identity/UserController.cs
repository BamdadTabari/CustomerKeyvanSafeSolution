using AutoMapper;
using KeyvanSafe.Domain.EntityFramework.Interfaces.IUnitOfWorks;
using KeyvanSafe.Shared.Assistant.Helpers;
using KeyvanSafe.Shared.Certain.Constants;
using KeyvanSafe.Shared.EntityFramework.Entities.Identity.Users;
using KeyvanSafe.Shared.Infrastructure.Operations;
using KeyvanSafe.Shared.Infrastructure.Routes;
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
    [CreateUserResultFilter]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var isExist = await _unitOfWorkIdentity.Users
            .ExistsAsync(new DuplicateUserSpecificationFile(request.Username).ToExpression());

        if (isExist)
            return new OperationResult(OperationResultStatus.UnProcessable, value: GenericErrors<User>.DuplicateError("user name"));

        var entity = new User()
        {
            Username = request.Username,
            Mobile = request.Mobile,
            Email = request.Email,
            PasswordHash = PasswordHasher.Hash(request.Password),
            State = request.State,
            CreatedAt = request.CreatedAt,
            UpdatedAt = request.UpdatedAt,
            ConcurrencyStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength),
            SecurityStamp = StampGenerator.CreateSecurityStamp(Defaults.SecurityStampLength),
        };

        _unitOfWork.Users.Add(entity);

        return new OperationResult(OperationResultStatus.Ok, isPersistAble: true, value: entity);

        return this.ReturnResponse(operation);
    }

    //[HttpPut(IdentityRoutes.Users + "{ueid}")]
    //[UpdateUserResultFilter]
    //public async Task<IActionResult> UpdateUser([FromRoute] string ueid, [FromBody] UpdateUserRequest request)
    //{
    //    var userId = ueid.DecodeInt();

    //    var operation = await _mediator.Send(new UpdateUserCommand(Request.GetRequestInfo())
    //    {
    //        UserId = userId,
    //        Username = request.Username,
    //        Email = request.Email,
    //        Password = request.Password,
    //        Mobile = request.Mobile,
    //    });

    //    return this.ReturnResponse(operation);
    //}

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
