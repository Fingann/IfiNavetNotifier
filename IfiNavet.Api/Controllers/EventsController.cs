using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IfiNavet.Application.Events.Queries.GetEvent;
using IfiNavet.Application.Events.Queries.GetEventsList;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Events;
using IfiNavet.Core.Entities.Users;
using Microsoft.AspNetCore.Mvc;

namespace IfiNavet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : BaseController
    {
       
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventViewModel>>> Get()
        {
            var s = await Mediator.Send(new GetEventListQuery());
            return Ok(s);
        }

        // GET api/values/5
        [HttpGet("{link}")]
        public async Task<ActionResult<EventViewModel>> Get(string link)
        {
            var s = await Mediator.Send(new GetEventQuery(link));
            return Ok(s);
        }

    }
}