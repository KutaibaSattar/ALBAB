using System.Threading.Tasks;
using ALBaB.Controllers;
using ALBaB.Data;
using ALBaB.Entities;
using ALBaB.Entities.DTOs;
using ALBaB.Token;
using AutoMapper;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
            , IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;


        }

        [HttpPost("register")]

        public async Task<ActionResult<AppUserDto>> Register(RegisterDto registorDto)
        {

            var user = _mapper.Map<RegisterDto, AppUser>(registorDto);

            var result = await _userManager.CreateAsync(user, registorDto.Password);
        
              if (!result.Succeeded) return BadRequest(result.Errors);

         var roleResult = await _userManager.AddToRoleAsync(user, "Member");
          
          if (!roleResult.Succeeded) return BadRequest(result.Errors);

        

            return new AppUserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,


            };

        }

         [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(LoginDto loginDto)
        {
          /*   var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower()); */

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
           
           if (user == null) return Unauthorized("Not Authorized"/* new ApiResponse(401) */);
            
            var result =  await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
            
            if (!result.Succeeded) return Unauthorized("Not Authorized"/* new ApiResponse(401) */);
            
           
            return new AppUserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhoneNumber = user.PhoneNumber
                
            };

        }



    }
}