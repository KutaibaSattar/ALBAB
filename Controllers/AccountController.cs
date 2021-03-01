using System;
using System.Linq;
using System.Security.Claims;
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

       [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(LoginDto loginDto)
        {
          /*   var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower()); */
        
          
           var user = await _userManager.FindByNameAsync(loginDto.UserId);

            /*  if (await UserExists(loginDto.Username)) return BadRequest("User Name is taken"); */
            
            /* var user = await _userManager.FindByNameAsync(loginDto.Username) ?? await _userManager.FindByEmailAsync(loginDto.Username); */

           /*  var user = await _userManager.FindByEmailAsync(loginDto.Email);
            user = await _userManager.FindByLoginAsync(loginDto.Email); */
           
           if (user == null) return Unauthorized("Unauthorized");
            
            var result =  await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
            
            user.LastActive = DateTime.Now; 
            await _userManager.UpdateAsync(user); 
           
            if (!result.Succeeded) return Unauthorized("Unauthorized");
            
           
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
            
         // because using ActionResult so we can return BadRequest
           
          /* if (registerDto.PhoneNumber != null){
               if (await CheckEmailExistsAsync(registerDto.PhoneNumber))
            {
                  return BadRequest("Email is taken");   
            }
           } */
            
           /*  var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("Could not find user");
           var userRoles = await _userManager.GetRolesAsync(user); */
          var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
           //var user = await _userManager.FindByEmailAsync(email);
         
         // var email = HttpContext.User.RetrieveEmailFromPrincipal();
            
            
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