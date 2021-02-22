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
using Microsoft.EntityFrameworkCore;

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

        public async Task<ActionResult<AppUserDto>> Register(RegisterDto registerDto)
       
        {
            
         // because using ActionResult so we can return BadRequest
           
           if (registerDto.Email != null){
               if (await CheckEmailExistsAsync(registerDto.Email))
            {
                  return BadRequest("Email is taken");   
            }
           }
            
            
       
           
            
            
            
            if (await UserExists(registerDto.UserName)) return BadRequest("User Name is taken");

            var user = _mapper.Map<AppUser>(registerDto);

  
            user.UserName = registerDto.UserName.ToLower();
          
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
                       
           if (!roleResult.Succeeded) return BadRequest(result.Errors);

           if (!result.Succeeded) return BadRequest(result.Errors);

            return new AppUserDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email

            }; 


       }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());


        }

        [HttpGet("emailexists")]

        public async Task<bool> CheckEmailExistsAsync ([FromQuery] string email)
        {
          
           return await _userManager.FindByEmailAsync(email) != null;

           

           
        }



    }
}