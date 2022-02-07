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
    public async Task CheckAccessCodeValidAndRetrieve_AccessCodeNotInDb()
    {
        await _dbFixture.PerformDatabaseOperation(db =>
        {
            var service = new AccessCodeService(db);
            var error = service.CheckAccessCodeValidAndRetrieve("fakeAccessCode", out var accessCode);

            Assert.NotNull(error);
            Assert.Null(accessCode);
            Assert.Equal("Access code could not be found", error.Errors.First());
        });
    }

    [Fact]
    public async Task CheckAccessCodeValidAndRetrieve_AccessCodeIsNull()
    {
        await _dbFixture.PerformDatabaseOperation(db =>
        {
            var service = new AccessCodeService(db);
            var error = service.CheckAccessCodeValidAndRetrieve(null, out var accessCode);

            Assert.NotNull(error);
            Assert.Null(accessCode);
            Assert.Equal("App is in restricted state - obtain access code", error.Errors.First());
        });
    }

    [Fact]
    public async Task CheckAccessCodeValidAndRetrieve_AccessCodeHasNoRemainingUses()
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

        await _dbFixture.PerformDatabaseOperation(db =>
        {
            var service = new AccessCodeService(db);
            var error = service.CheckAccessCodeValidAndRetrieve(code, out var accessCode);

            Assert.NotNull(error);
            Assert.NotNull(accessCode);
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

            var accessCode = db.AccessCodes.FirstOrDefault(x => x.AccessCode == code);
            Assert.Equal(0, accessCode!.NumberOfUsesRemaining);
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

            var accessCode = db.AccessCodes.FirstOrDefault(x => x.AccessCode == code);
            Assert.Equal(0, accessCode!.NumberOfUsesRemaining);
        });
    }
}
