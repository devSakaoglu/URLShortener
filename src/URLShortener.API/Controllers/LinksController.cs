using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using URLShortener.API.Authentication;
using URLShortener.Domain.Entities;
using URLShortener.Domain.Enums;
using URLShortener.Shared.Models.Link;
using URLShortener.Shared.Services.Interfaces;

namespace URLShortener.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LinksController : BaseController
{
    private readonly ILinkService _linkService;

    public LinksController(ILinkService linkService)
    {
        _linkService = linkService;
    }

    [HttpGet]
    [UserType(UserType.User)]
    public async Task<ICollection<Link>> Get()
    {
        var userId = GetUserIdFromJwtClaim();

        return await _linkService.GetAllByUserIdAsync(userId);
    }

    [HttpGet("{linkId:long}")]
    [UserType(UserType.Super)]
    public async Task<Link> Get(long linkId)
    {
        var userId = GetUserIdFromJwtClaim();

        return await _linkService.GetByIdAsync(new GetByIdModel
        {
            Id = linkId,
            UserId = userId
        });
    }

    [HttpGet("{shortAddress}")]
    public async Task<Link> Get(string shortAddress)
    {
        return await _linkService.GetByShortAddressAsync(new GetByShortAddressModel
        {
            ShortAddress = shortAddress,

        });
    }

    [HttpPost]
    [UserType(UserType.User)]
    public async Task<Link> Create([FromBody] CreateLinkModel model)
    {
        model.UserId = GetUserIdFromJwtClaim();

        return await _linkService.CreateAsync(model);
    }

    [HttpPut("/UpdateShortAddress")]
    [UserType(UserType.PremiumUser)]
    public async Task<Link> UpdateShortAddress([FromBody] UpdateShortAddressModel model)
    {
        model.UserId = GetUserIdFromJwtClaim();

        return await _linkService.UpdateShortAddressAsync(model);
    }

    [HttpPut("/UpdateFullAddress")]
    [UserType(UserType.PremiumUser)]
    public async Task<Link> UpdateFullAddress([FromBody] UpdateFullAddressModel model)
    {
        model.UserId = GetUserIdFromJwtClaim();

        return await _linkService.UpdateFullAddressAsync(model);
    }

    [HttpPut("/SetEnabled")]
    [UserType(UserType.User)]
    public async Task<Link> SetEnabled([FromBody] SetEnabledModel model)
    {
        model.UserId = GetUserIdFromJwtClaim();

        return await _linkService.SetEnabledAsync(model);
    }

    [HttpDelete("{linkId:long}")]
    [UserType(UserType.User)]
    public async Task<bool> Delete(long linkId)
    {
        var userId = GetUserIdFromJwtClaim();

        await _linkService.DeleteAsync(new DeleteModel
        {
            Id = linkId,
            UserId = userId
        });

        return true;
    }
}