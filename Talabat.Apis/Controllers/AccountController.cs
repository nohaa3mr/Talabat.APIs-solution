using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Apis.ErrorsHandler;
using Talabat.Apis.Helpers;
using Talabat.Core.DTOs;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IServices;

namespace Talabat.Apis.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, IMapper mapper,  SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
            {
                return BadRequest(new ErrorApiResponse(400));
            }
            var user = new AppUser()
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0],
                DisplayName = model.DisplayName,
            };
            var Result = await _userManager.CreateAsync(user, model.Password);

            if (!Result.Succeeded) return BadRequest(new ErrorApiResponse(400));
            var ResultDto = new UserDTO()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Token = await _tokenService.GetTokenAsync(user, _userManager)

            };
            return Ok(ResultDto);

        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            var obj = new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = Email,
                Token = await _tokenService.GetTokenAsync(user, _userManager)
            };
            return Ok(obj);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getUserAddress")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            //var user = await _userManager.GetAdressByEmailAsync(User);
            //var MappedAddress = _mapper.Map<Address, AddressDTO>(user.Address); 
            //return Ok(MappedAddress);
            var user = await _userManager.GetAdressByEmailAsync(User);
            if (user is null) return BadRequest(new ErrorApiResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<AddressDTO>(user.Address));
        }
        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
