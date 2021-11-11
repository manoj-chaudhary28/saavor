using System.Collections.Generic;
using System.Security.Claims;

namespace saavor.Shared.Interfaces
{
    public interface IClaimService
    {
        void AddClaim(ClaimsIdentity claimsIdentity, List<Claim> claims);
        string GetClaim(string type);
        bool RemoveClaim(string type);
    }
}
