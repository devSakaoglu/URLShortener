using Microsoft.EntityFrameworkCore;
using URLShortener.Data.Contexts;
using URLShortener.Domain.Entities;
using URLShortener.Services.Implementations;
using URLShortener.Shared.Models.Link;
using URLShortener.Shared.Services.Interfaces;

namespace URLShortener.UnitTests.Services;

public class LinkServiceTests
{
    private static readonly Guid DefaultUserId = Guid.Parse("fc28d720-4364-4cfc-9ee3-72983d433489");

    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions =
        new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InMemoryDb" + Guid.NewGuid())
            .Options;

    private readonly List<Link> _defaultLinkModels = new()
    {
        new Link
        {
            Id = 1,
            ShortAddress = "ShortAddress1",
            FullAddress = "FullAddress1",
            UserId = DefaultUserId
        },
        new Link
        {
            Id = 2,
            ShortAddress = "ShortAddress2",
            FullAddress = "FullAddress2",
            UserId = DefaultUserId
        }
    };

    private async Task<ILinkService> SetupService()
    {
        var context = new ApplicationDbContext(_dbContextOptions);

        await context.AddAsync(new AppUser
        {
            Id = DefaultUserId
        });

        await context.AddRangeAsync(_defaultLinkModels);

        await context.SaveChangesAsync();

        return new LinkService(context);
    }

    [Fact]
    public async Task CreateAsync_Success_Test()
    {
        // Arrange
        var model = new CreateLinkModel
        {
            ShortAddress = "Short",
            FullAddress = "Full",
            UserId = DefaultUserId
        };

        var linkService = await SetupService();

        // Act
        var result = await linkService.CreateAsync(model);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Short", result.ShortAddress);
        Assert.Equal("Full", result.FullAddress);
        Assert.Equal(DefaultUserId, result.UserId);
        Assert.True(result.IsEnabled);
    }

    [Fact]
    public async Task UpdateShortAddress_Success_Test()
    {
        // Arrange
        var model = new UpdateShortAddressModel
        {
            ShortAddress = "NewShortAddress",
            UserId = DefaultUserId,
            Id = 1
        };

        var linkService = await SetupService();

        // Act
        var result = await linkService.UpdateShortAddressAsync(model);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.ShortAddress, result.ShortAddress);
    }


    [Fact]
    public async Task UpdateFullAddress_Success_Test()
    {
        // Arrange
        var model = new UpdateFullAddressModel
        {
            FullAddress = "NewFullAddress",
            UserId = DefaultUserId,
            Id = 1
        };

        var linkService = await SetupService();

        // Act
        var result = await linkService.UpdateFullAddressAsync(model);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.FullAddress, result.FullAddress);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task SetEnabled_Success_Test(bool isEnabled)
    {
        // Arrange
        var model = new SetEnabledModel
        {
            IsEnabled = isEnabled,
            UserId = DefaultUserId,
            Id = 1
        };

        var linkService = await SetupService();

        // Act
        var result = await linkService.SetEnabledAsync(model);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.IsEnabled, result.IsEnabled);
    }

    [Fact]
    public async Task GetAllByUserIdAsync_Success_Test()
    {
        // Arrange
        var linkService = await SetupService();

        // Act
        var resultList = await linkService.GetAllByUserIdAsync(DefaultUserId);

        // Assert
        Assert.NotNull(resultList);
        Assert.Equal(_defaultLinkModels.Count, resultList.Count);
    }

    [Fact]
    public async Task GetByUserIdAsync_Success_Test()
    {
        // Arrange
        var model = new GetByIdModel
        {
            UserId = DefaultUserId,
            Id = 1
        };
        var linkService = await SetupService();

        // Act
        var result = await linkService.GetByIdAsync(model);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.UserId, result.UserId);
        Assert.Equal(model.Id, result.Id);
    }

    [Fact]
    public async Task GetByShortAddressAsync_Success_Test()
    {
        // Arrange
        var model = new GetByShortAddressModel
        {
            ShortAddress = _defaultLinkModels.First().ShortAddress
        };
        var linkService = await SetupService();

        // Act
        var result = await linkService.GetByShortAddressAsync(model);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(_defaultLinkModels.First().ShortAddress, result.ShortAddress);
    }

    [Fact]
    public async Task DeleteAsync_Success_Test()
    {
        // Arrange
        var deleteModel = new DeleteModel
        {
            UserId = DefaultUserId,
            Id = _defaultLinkModels.First().Id
        };

        var getByIdModel = new GetByIdModel
        {
            UserId = DefaultUserId,
            Id = _defaultLinkModels.First().Id
        };
        var linkService = await SetupService();

        // Act
        await linkService.DeleteAsync(deleteModel);
        var deletedLink = await linkService.GetByIdAsync(getByIdModel);

        // Assert
        Assert.True(deletedLink.IsDeleted);
    }
}