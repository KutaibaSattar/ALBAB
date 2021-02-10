using System.Collections.Generic;
using System.Threading.Tasks;
using ALBaB.Data;
using ALBaB.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ALBaB.Entities.DTOs;

namespace ALBaB.Controllers
{
    public class UsersController : BaseController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UsersController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

       
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
          var users = await _context.Users.ToListAsync();
            
         var result = _mapper.Map<List<AppUser>,List<AppUserDto>>(users);
           
          return Ok(result); 


        }  
   
       [HttpGet("{id}")]

      public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
           
            var user = await _context.Users.FindAsync(id);
            var result = _mapper.Map<AppUser,AppUserDto>(user);
            return Ok(result);


        } 


    }
}