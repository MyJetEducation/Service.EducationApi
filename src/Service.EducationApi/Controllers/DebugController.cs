using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Service.EducationApi;
using Service.EducationApi.Controllers.Models;

namespace Service.Verification.Api.Controllers
{
    [ApiController]
    [Route("/api/Debug")]
    public class DebugController : ControllerBase
    {
        [HttpGet("who")]
        [Authorize()]
        public Response<WhoResponse> Who()
        {
            var clientId = User.Identity.Name;
            
            return Response<WhoResponse>.Ok(new WhoResponse(){ClientId = clientId});
        }
        
        [HttpPost("login")]
        public ActionResult<LoginResult> Login([FromBody] LoginRequest request)
        {
            if (request.Login != request.Password)
            {
                return StatusCode(401);
            }
            
            var key = Encoding.ASCII.GetBytes(Program.JwtSecret);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Aud, "education-api")
            };

            var clientId = request.Login;
            var identity = new GenericIdentity(clientId);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(identity, claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenStr =  tokenHandler.WriteToken(token);

            return Ok(new LoginResult() {Token = tokenStr});
        }
    }

    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    
    public class LoginResult
    {
        public string Token { get; set; }
    }

    public class WhoResponse
    {
        public string ClientId { get; set; }
        
    }

    public enum ErrorCodes
    {
        Ok,
        IncorectClient
    }
}
