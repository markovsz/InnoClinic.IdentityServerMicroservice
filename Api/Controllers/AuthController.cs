using Api.FilterAttributes;
using Application.DTOs.Incoming;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogInAsync(LoginIncomingDto incomingDto)
        {
            var tokens = await _authService.LogInAsync(incomingDto);
            return Ok(tokens);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ServiceFilter(typeof(ExtractAccountIdAttribute))]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOutAsync([FromBody] RefreshTokenIncomingDto incomingDto)
        {
            await _authService.LogOutAsync(incomingDto);
            return NoContent();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpIncomingDto incomingDto)
        {
            var accountId = await _authService.SignUpAsync(incomingDto);
            var tokens = await _authService.LogInAsync(new LoginIncomingDto { Email = incomingDto.Email, Password = incomingDto.Password });

            var signUpResult = new {
                AccountId = accountId,
                Tokens = tokens
            };
            return Created("", signUpResult);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("refresh")]
        public async Task<IActionResult> GenerateAccessTokenAsync([FromBody] RefreshTokenIncomingDto incomingDto)
        {
            var accessToken = await _authService.GenerateAccessTokenAsync(incomingDto);
            return Ok(accessToken);
        }
    }
}
