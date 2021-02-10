using System.Threading.Tasks;
using ALBaB.Controllers;
using ALBaB.Data;
using ALBaB.Entities;
using AutoMapper;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class AccountController : BaseController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AccountController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

      [HttpPost("register")]

        public async Task<ActionResult<AppUser>> Register(RegisterDto registorDto)
        {

            var user = _mapper.Map<RegisterDto, AppUser>(registorDto);

            await _context.Users.AddAsync(user);
            _context.SaveChanges();

            return Ok(user);



        } 


    }
}