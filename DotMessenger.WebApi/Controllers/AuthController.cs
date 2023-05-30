using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DotMessenger.Core.Interactors;
using DotMessenger.Shared.DataTransferObjects;
using DotMessenger.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DotMessenger.WebApi.Controllers
{
    [ApiController]
    [Route("api/AuthController")]
    public class AuthController : Controller
    {
        private readonly AuthenticationOptions authOptions;
        private readonly AccountsInteractor accountsInteractor;
        private readonly AppRolesInteractor appRolesInteractor;

        public AuthController(AuthenticationOptions authOptions, AccountsInteractor accountsInteractor, AppRolesInteractor appRolesInteractor)
        {
            this.authOptions = authOptions;
            this.accountsInteractor = accountsInteractor;
            this.appRolesInteractor = appRolesInteractor;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<string> GetToken(string username, string password)
        {
            var identity = GetIdentity(username, password).Value;
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: authOptions.Issuer,
                    audience: authOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(authOptions.LifeTime),
                    signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return encodedJwt;
        }

        private ActionResult<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var loginResponse = accountsInteractor.Login(username, password);

            if(loginResponse.Error)
            {
                return NotFound();
            }

            var account = loginResponse.Value;
            var roleResponse = appRolesInteractor.FindByAccount(account!.Id);

            if(roleResponse.Error)
            {
                return NotFound();
            }

            var role = roleResponse.Value;

            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Nickname),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name)
            };

            ClaimsIdentity claimsIdentity = new
                ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        [Authorize]
        [HttpGet("getAccount")]
        public Response<SharedAccountDto> GetAccount()
        {
            if(User.Identity == null)
            {
                return new Response<SharedAccountDto>()
                {
                    Error = true,
                    ErrorCode = 404,
                    ErrorMessage = "Could not find authorized user",
                };
            }

            string nickname = User.Identity.Name!;

            return accountsInteractor.FindByNickname(nickname);
        }
    }
}
