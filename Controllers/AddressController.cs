using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALBAB.Entities;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.DB;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Controllers
{
    public class AddressController : BaseController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AddressController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

         [HttpGet]

      public async Task<ActionResult<IEnumerable<AddressRes>>> GetAddress()
        {

            var address = await _context.Address.ToListAsync();

            var result = _mapper.Map<List<Address>,List<AddressRes>>(address);

            return Ok(result);


        }

       [HttpGet("getmemberAddresses/{id}")]

      public async Task<ActionResult<IEnumerable<AddressRes>>> GetmemberAddresses(int id)
        {

            var address = await _context.Address.Where(user => user.AppUserId == id ).ToListAsync();

            var result = _mapper.Map<List<Address>,List<AddressRes>>(address);

            return Ok(result);


        }
       [HttpPost]
            public async Task<ActionResult<AddressRes>> newAddress( AddressRes addressRes)
        {

            var address = _mapper.Map<AddressRes,Address>(addressRes);

            _context.Address.Add(address);

           await _context.SaveChangesAsync();

            _mapper.Map<Address,AddressRes>(address,addressRes);

           return addressRes;
       }




    }
}