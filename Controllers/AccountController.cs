using System.Runtime.Intrinsics.X86;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Controllers;
using ALBAB.Errors;
using ALBAB.Token;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities;
using ALBAB.Entities.DB;

namespace ALBAB.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

          private readonly DataContext _context;

        private readonly IMapper _mapper;
        public AccountController(DataContext context,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
            , IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
              _context = context;


        }

       [HttpPost("login")]
        public async Task<ActionResult<AppUserRes>> Login(LoginDto loginDto)
        {

           var user = await _userManager.FindByNameAsync(loginDto.KeyId);
           if (user == null) return Unauthorized(new ApiResponse(401));

            var result =  await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

            user.LastActive = DateTime.Now;
            await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return Unauthorized();

            return new AppUserRes
            {
                KeyId = user.UserName,
                Name = user.Name,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };



        }



        [HttpPost("register")]
        public async Task<ActionResult<AppUserRes>> Register(RegisterRes registerDto)

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

            if(await UserExists(registerDto.KeyId)) return BadRequest("User Name is taken");

            var user = _mapper.Map<AppUser>(registerDto);


            //user.UserName = registerDto.UserName.ToLower();

        

           var result = await _userManager.CreateAsync(user, registerDto.Password);

          var roleResult = await _userManager.AddToRoleAsync(user, "Member");

           if (!roleResult.Succeeded) return BadRequest(result.Errors);

           if (!result.Succeeded) return BadRequest(result.Errors);

            return new AppUserRes
            {
                Name = user.Name,
                KeyId = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email,
                PhoneNumber = user.UserName,
                type = user.type,

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