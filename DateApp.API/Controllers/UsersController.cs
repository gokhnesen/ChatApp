﻿using DateApp.Entity.DataContext;
using DateApp.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateApp.API.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly DataContextModel _context;
        public UsersController(DataContextModel context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task< ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        //api/users/3
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
    }
}
