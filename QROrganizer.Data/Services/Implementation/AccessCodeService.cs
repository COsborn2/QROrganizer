using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QROrganizer.Data.Models;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Data.Services.Implementation;

public class AccessCodeService : IAccessCodeService
{
    private readonly AppDbContext _dbContext;

    public AccessCodeService(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<ErrorResponse> ValidateAndUseAccessCode(string code)
    {
        var error = CheckAccessCodeValidAndRetrieve(code, out var accessCode);

        if (error is not null) return error;

        if (!accessCode.IsLimitedKey) return null;

        accessCode.NumberOfUsesRemaining--;
        await _dbContext.SaveChangesAsync();

        return null;
    }

    public ErrorResponse CheckAccessCodeValidAndRetrieve(string code, out RestrictedAccessCode accessCode)
    {
        accessCode = null;
        if (string.IsNullOrWhiteSpace(code))
        {
            return new ErrorResponse
            {
                Errors = new List<string> { "App is in restricted state - obtain access code" }
            };
        }

        accessCode = _dbContext.AccessCodes
            .FirstOrDefault(x => x.AccessCode == code);

        if (accessCode is null)
        {
            return new ErrorResponse
            {
                Errors = new List<string> { "Access code could not be found" }
            };
        }

        switch (accessCode.IsLimitedKey)
        {
            case false:
                return null;
            case true when accessCode.NumberOfUsesRemaining < 1:
                return new ErrorResponse
                {
                    Errors = new List<string> { "Given access code has no uses remaining" }
                };
        }

        return null;
    }

    public bool IsAccessCodeValid(string code)
    {
        return CheckAccessCodeValidAndRetrieve(code, out _) is null;
    }
}
