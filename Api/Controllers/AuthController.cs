using Api.FilterAttributes;
using Application.DTOs.Incoming;
using Application.Interfaces;
using Domain.Enums;
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("patient/signup")]
        public async Task<IActionResult> SignUpPatientAsync([FromBody] SignUpIncomingDto incomingDto)
        {
            await _authService.SignUpAsync(incomingDto, UserRoles.Patient);
            return Ok();
        }

        [Authorize(Roles = nameof(UserRoles.Receptionist), AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("admin/patient/signup")]
        public async Task<IActionResult> SignUpPatientAsync([FromBody] SignUpWithoutPasswordIncomingDto incomingDto)
        {
            var outgoingDto = await _authService.SignUpWithoutPasswordAsync(incomingDto, UserRoles.Patient);
            return Ok(outgoingDto);
        }

        [Authorize(Roles = nameof(UserRoles.Receptionist), AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("admin/doctor/signup")]
        public async Task<IActionResult> SignUpDoctorAsync([FromBody] SignUpWithoutPasswordIncomingDto incomingDto)
        {
            var outgoingDto = await _authService.SignUpWithoutPasswordAsync(incomingDto, UserRoles.Doctor);
            return Ok(outgoingDto);
        }

        [Authorize(Roles = nameof(UserRoles.Receptionist), AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("admin/receptionist/signup")]
        public async Task<IActionResult> SignUpReceptionistAsync([FromBody] SignUpWithoutPasswordIncomingDto incomingDto)
        {
            var outgoingDto = await _authService.SignUpWithoutPasswordAsync(incomingDto, UserRoles.Receptionist);
            return Ok(outgoingDto);
        }

        [AllowAnonymous]
        [HttpPost("email/confirm")]
        public async Task<IActionResult> ConfirmEmailAsync(string email, string token)
        {
            await _authService.ConfirmEmailAsync(email, token);
            return Ok();
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
