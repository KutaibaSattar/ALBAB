using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ALBAB.Data;
using ALBAB.Token;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities;

namespace ALBAB.Controllers
{
    public class MembersController :BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;
       
        public MembersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
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
           
          /* if (registerDto.PhoneNumber != null){
               if (await CheckEmailExistsAsync(registerDto.PhoneNumber))
            {
                  return BadRequest("Email is taken");   
            }
           } */
            
           /*  var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("Could not find user");
           var userRoles = await _userManager.GetRolesAsync(user); */
          
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