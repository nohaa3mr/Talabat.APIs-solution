using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.ErrorsHandler;
using Talabat.Core.DTOs;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IServices;

namespace Talabat.Apis.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager , ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            var user = new AppUser()
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0],
                DisplayName = model.DisplayName,
                Address = model.Address
            };
            var Result = await _userManager.CreateAsync(user, model.Password);

            if (!Result.Succeeded) return BadRequest(new ErrorApiResponse(400));
            var ResultDto = new UserDTO()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Token =await _tokenService.GetTokenAsync(user, _userManager)

            };
            return Ok(ResultDto);

        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var User =await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ErrorApiResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if (!Result.Succeeded) return BadRequest(new ErrorApiResponse(400));
            var ResultDto = new UserDTO()
            {
                Email = User.Email,
                DisplayName = User.DisplayName,
                Token = await _tokenService.GetTokenAsync(User, _userManager)

            };
            return Ok(ResultDto);
        }
    }
}
