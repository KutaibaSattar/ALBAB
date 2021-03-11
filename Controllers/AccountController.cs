using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ALBaB.Controllers;
using ALBaB.Data;
using ALBaB.Entities;
using ALBaB.Entities.DTOs;
using ALBaB.Errors;
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

       [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(LoginDto loginDto)
        {
              
          
           var user = await _userManager.FindByNameAsync(loginDto.UserId);

          
           if (user == null) return Unauthorized(new ApiResponse(401));
            
            var result =  await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
            
            user.LastActive = DateTime.Now; 
            await _userManager.UpdateAsync(user); 
           
            if (!result.Succeeded) return Unauthorized();
             
           
         
            return new AppUserDto
            {
                UserId = user.UserName,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
               
                
            };

               

        }
       
       
       
        [HttpPost("register")]

        public async Task<ActionResult<AppUserDto>> Register(RegisterDto registerDto)
       
        {
            
        
            
            
            if(await UserExists(registerDto.UserId)) return BadRequest("User Name is taken");

            var user = _mapper.Map<AppUser>(registerDto);

  
            //user.UserName = registerDto.UserName.ToLower();
          
           var result = await _userManager.CreateAsync(user, registerDto.Password);

          var roleResult = await _userManager.AddToRoleAsync(user, "Member");
                       
           if (!roleResult.Succeeded) return BadRequest(result.Errors);

           if (!result.Succeeded) return BadRequest(result.Errors);
               
            return new AppUserDto
            {
                DisplayName = user.DisplayName,
                UserId = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email,
                PhoneNumber = user.UserName
            
            }; 


       }

        private async Task<bool> UserExists(string userId)
        {
          return await _userManager.FindByNameAsync(userId)  != null;
           
                  
        }

        [HttpGet("emailexists")]

        public async Task<bool> CheckEmailExistsAsync ([FromQuery] string email)
        {
          
           return await _userManager.FindByNameAsync(email) != null;

           

           
        }



    }
}