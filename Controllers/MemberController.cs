using System.Collections.Generic;
using System.Threading.Tasks;
using ALBAB.Entities.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities;
 

namespace ALBAB.Controllers
{
   [Authorize(Policy = "RequiredUserRole")]
    public class MemberController : BaseController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;



        public MemberController(DataContext context, IMapper mapper,UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _context = context;
             _userManager = userManager;

        }

      

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberRes>>> GetUsers()
        {
          var users = await _context.Users.ToListAsync();

         var result = _mapper.Map<List<AppUser>,List<MemberRes>>(users);

          return Ok(result);


        }

       [HttpGet("getmember/{id}")]

      public async Task<ActionResult<AppUser>> GetMember(int id)
        {

            var user = await _context.Users.Include(a => a.Address).FirstAsync(i => i.Id == id);
            var result = _mapper.Map<AppUser,AppUserRes>(user);
            return Ok(result);


        }

        [Authorize]
        [HttpGet("getcurrentuser")]

        public async Task<ActionResult<AppUserRes>> GetCurrentUser()
        {


            var role = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            var user = await _userManager.FindByEmailAsync(role);


            var userToReturn = _mapper.Map<AppUserRes>(user);

            return Ok(userToReturn);
        }

          private async Task<bool> UserExists(string keyId)
        {
          return await _userManager.FindByNameAsync(keyId)  != null;


        }

      [HttpPut("updatemember")]

      public async Task<ActionResult<MemberUpdateRes>> updateMember ( MemberUpdateRes memberUpdate )
      {
        var member = await _context.Users.FirstOrDefaultAsync(i => i.Id == memberUpdate.Id);

        _mapper.Map<MemberUpdateRes,AppUser>(memberUpdate,member);

        _context.SaveChanges();

         _mapper.Map<AppUser,MemberUpdateRes>(member,memberUpdate);

        return memberUpdate;

      }


    }
}