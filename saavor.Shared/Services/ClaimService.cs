using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using saavor.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace saavor.Shared.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimService(IHttpContextAccessor httpContextAccessorInstance)
        {
            _httpContextAccessor = httpContextAccessorInstance;
        }
        /// <summary>
        /// Add new Claim in Sign In User
        /// </summary>
        /// <param name="claimsIdentity">Pass Claim Identiy</param>
        /// <param name="claims">Pass List of claims</param>
        public void AddClaim(ClaimsIdentity claimsIdentity, List<Claim> claims)
        {
            foreach (Claim claim in claims)
            {
                if (claimsIdentity.FindFirst(claim.Type) != null)
                    claimsIdentity.RemoveClaim(claimsIdentity.FindFirst(claim.Type));

                claimsIdentity.AddClaim(new Claim(claim.Type, claim.Value));
            }
            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                                         , new ClaimsPrincipal(claimsIdentity)
                                         , new AuthenticationProperties
                                         {
                                             IsPersistent = true
                                         });

        }
        /// <summary>
        /// Method to get claim by type.
        /// </summary>
        /// <param name="type">Pass the claim type</param>
        /// <returns></returns>
        public string GetClaim(string type)
        {
            try
            {
                var claim = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == type);
                if(claim != null && !string.IsNullOrEmpty(claim.Value))
                    return claim.Value;
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// Method to remove the claim
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool RemoveClaim(string type)
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User as ClaimsPrincipal;
                var claimsIdentity = user.Identity as ClaimsIdentity;
                var claim = (from ctx in user.Claims
                             where ctx.Type == type
                             select ctx).FirstOrDefault();
                claimsIdentity.RemoveClaim(claim);
                _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                                       , new ClaimsPrincipal(claimsIdentity)
                                       , new AuthenticationProperties
                                       {
                                           IsPersistent = true
                                       });
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
