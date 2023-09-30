using Application.CQRS.User.Forms;
using Common.Extensions;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginForm cmd)
        {
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenForm cmd)
        {
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

    }
}
