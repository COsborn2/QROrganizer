using System;
using System.Linq;
using System.Threading.Tasks;
using IntelliTect.TestTools.Data;
using QROrganizer.Data.Models;
using QROrganizer.Data.Services.Implementation;
using Xunit;

namespace QROrganizer.Data.Tests;

public class AccessServiceTests : IClassFixture<DatabaseFixture<AppDbContext>>
{
    private readonly DatabaseFixture<AppDbContext> _dbFixture;

    public AccessServiceTests(DatabaseFixture<AppDbContext> dbFixture)
    {
        _dbFixture = dbFixture ?? throw new ArgumentNullException(nameof(dbFixture));
    }

    [Fact]
    public async Task ValidateAndUseAccessCode_AccessCodeNotInDb()
    {
        await _dbFixture.PerformDatabaseOperation(async db =>
        {
            var service = new AccessCodeService(db);
            var error = await service.ValidateAndUseAccessCode("fakeAccessCode");

            Assert.NotNull(error);
            Assert.Equal("Access code could not be found", error.Errors.First());
        });
    }

    [Fact]
    public async Task ValidateAndUseAccessCode_AccessCodeIsNull()
    {
        await _dbFixture.PerformDatabaseOperation(async db =>
        {
            var service = new AccessCodeService(db);
            var error = await service.ValidateAndUseAccessCode(null);

            Assert.NotNull(error);
            Assert.Equal("App is in restricted state - obtain access code", error.Errors.First());
        });
    }

    [Fact]
    public async Task ValidateAndUseAccessCode_AccessCodeHasNoRemainingUses()
    {
        var code = Guid.NewGuid().ToString("N");

        await _dbFixture.PerformDatabaseOperation(async db =>
        {
            db.AccessCodes.Add(new RestrictedAccessCode
            {
                AccessCode = code,
                IsLimitedKey = true,
                NumberOfUsesRemaining = 0
            });
            await db.SaveChangesAsync();
        });

        await _dbFixture.PerformDatabaseOperation(async db =>
        {
            var service = new AccessCodeService(db);
            var error = await service.ValidateAndUseAccessCode(code);

            Assert.NotNull(error);
            Assert.Equal("Given access code has no uses remaining", error.Errors.First());
        });
    }

    [Fact]
    public async Task ValidateAndUseAccessCode_AccessCodeHasOneUse()
    {
        var code = Guid.NewGuid().ToString("N");

        await _dbFixture.PerformDatabaseOperation(async db =>
        {
            db.AccessCodes.Add(new RestrictedAccessCode
            {
                AccessCode = code,
                IsLimitedKey = true,
                NumberOfUsesRemaining = 1
            });
            await db.SaveChangesAsync();
        });

        await _dbFixture.PerformDatabaseOperation(async db =>
        {
            var service = new AccessCodeService(db);
            var error = await service.ValidateAndUseAccessCode(code);

            Assert.Null(error);

            var accessCode = db.AccessCodes.First(x => x.AccessCode == code);
            Assert.Equal(0, accessCode.NumberOfUsesRemaining);
        });
    }

    [Fact]
    public async Task ValidateAndUseAccessCode_UnlimitedAccessKeyUsed()
    {
        var code = Guid.NewGuid().ToString("N");

        await _dbFixture.PerformDatabaseOperation(async db =>
        {
            db.AccessCodes.Add(new RestrictedAccessCode
            {
                AccessCode = code,
                IsLimitedKey = false,
                NumberOfUsesRemaining = 0
            });
            await db.SaveChangesAsync();
        });

        await _dbFixture.PerformDatabaseOperation(async db =>
        {
            var service = new AccessCodeService(db);
            var error = await service.ValidateAndUseAccessCode(code);

            Assert.Null(error);

            var accessCode = db.AccessCodes.First(x => x.AccessCode == code);
            Assert.Equal(0, accessCode.NumberOfUsesRemaining);
        });
    }
}
