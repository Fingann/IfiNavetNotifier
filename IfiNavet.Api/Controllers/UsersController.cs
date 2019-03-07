using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IfiNavet.Application.Events.Queries.GetEvent;
using IfiNavet.Application.Events.Queries.GetEventsList;
using IfiNavet.Application.Users.Commands.ChangePassword;
using IfiNavet.Application.Users.Commands.CreateUserLogin;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Events;
using IfiNavet.Core.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IfiNavet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
       
        
        // PUT api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(string id, [FromBody]ChangePasswordCommand command)
        {
            if (await Mediator.Send(command))
            {
                return Ok();
            }

            return NotFound();

        }

        // PUT api/Users/5
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post(string id, [FromBody]CreateUserLoginCommand command)
        {
            if (await Mediator.Send(command))
            {
                return Ok();
            }

            return NotFound();

        }

    }
}