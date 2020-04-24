﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backendapi.Models;
using backendapi.Services;

namespace backendapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly NeedService _needService;

        public UsersController(UserService userService, NeedService needService)
        {
            _userService = userService;
            _needService = needService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() => _userService.Get();

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }


        
        [HttpGet("{id:length(24)}", Name = "GetNeedsIds")]
        public ActionResult<List<MongoDB.Bson.ObjectId>> Get(string id)
        {
            var ListsIds = _userService.GetUserNeedsIds(id);
            
            if (ListsIds == null)
            {
                return NotFound();
            }
          
            ListsIds.ForEach(objid =>
            {
                System.Diagnostics.Debug.WriteLine(
                    _needService.GetNeed(objid.ToString()).Title
                    );

                System.Diagnostics.Debug.WriteLine(
                    _needService.GetNeed(objid.ToString()).Description
                    );
            });
            return ListsIds;
        }

        [HttpGet("getUser/{id:length(24)}", Name = "GetUserData")]
        public ActionResult<User> GetUser(string id)
        {
            var userData = _userService.GetUser(id);

            if (userData == null)
            {
                return NotFound();
            }
          
            return userData;
        }

        [HttpPut("updateUser/{id:length(24)}", Name = "UpdateUser")]
        public ActionResult<User> UpdateUser(string id, [FromBody] User user)
        {
            
            var UserCheck = _userService.GetUser(id);
            if (UserCheck == null)
            {
                return NotFound();
            }

            _userService.UpdateUser(id, user);
            return NoContent();
        }


    }
}
