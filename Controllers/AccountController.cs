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



        // private async Task<bool> UserExists(string userKey)
        // {
        //   return await _userManager.FindByNameAsync(userKey)  != null;


        // }

        // [HttpGet("emailexists")]

        // public async Task<bool> CheckEmailExistsAsync ([FromQuery] string email)
        // {

        //    return await _userManager.FindByNameAsync(email) != null;




        // }



    }
}