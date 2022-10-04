using AutoMapper;
using DateApp.Data.DTOs;
using DateApp.Data.Interfaces;
using DateApp.Entity.DataContext;
using DateApp.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DateApp.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContextModel _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(DataContextModel context,ITokenService tokenService,IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(registerDto);




         

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.Include(p=>p.Photos).SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null) return Unauthorized("Invalid Username");



            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url,
                KnownAs=user.KnownAs,
                
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
