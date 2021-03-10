using System.Collections.Generic;
using System.Threading.Tasks;
using ALBaB.Data;
using ALBaB.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ALBaB.Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using Entities.DTOs;

namespace ALBaB.Controllers
{
   //[Authorize(Policy = "RequiredUserRole")]
    public class UsersController : BaseController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

      

        public UsersController(DataContext context, IMapper mapper,UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _context = context;
             _userManager = userManager;

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
                Email = user.Email,
                PhoneNumber = user.UserName
            
            }; 


       }

       
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
          var users = await _context.Users.ToListAsync();
            
         var result = _mapper.Map<List<AppUser>,List<MemberDto>>(users);
           
          return Ok(result); 


        }  
   
       [HttpGet("{id}")]

      public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
           
            var user = await _context.Users.FindAsync(id);
            var result = _mapper.Map<AppUser,AppUserDto>(user);
            return Ok(result);


        } 

        [Authorize]
        [HttpGet("getcurrentuser")]

        public async Task<ActionResult<AppUserDto>> GetCurrentUser()
        {
           
           
            var role = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            
            var user = await _userManager.FindByEmailAsync(role);

            // calling extension userManager from  UserManagerExtensions, HttpContext can reached only inside controller
           
            //var user = await _userManager.FindByEmailFromClaimPrinciple(HttpContext.User); 


            var userToReturn = _mapper.Map<AppUserDto>(user);

            return Ok(userToReturn);
        }
      
          private async Task<bool> UserExists(string userId)
        {
          return await _userManager.FindByNameAsync(userId)  != null;
           
                  
        }




    }
}