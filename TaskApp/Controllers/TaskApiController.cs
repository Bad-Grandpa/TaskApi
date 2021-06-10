using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp.Data.Models;
using TaskApp.Services;

namespace TaskApp.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TaskApiController : ControllerBase
    {
        private readonly MeetingService _meetingService;

        public TaskApiController(MeetingService meetingService)
        {
            _meetingService = meetingService;
        }
        
        [HttpGet]
        public ActionResult<List<Meeting>> Get() =>
            _meetingService.Get();

        [HttpGet("{id:length(24)}", Name = "GetMeeting")]
        public ActionResult<Meeting> Get(string id)
        {
            var meeting = _meetingService.Get(id);

            if (meeting == null)
            {
                return NotFound();
            }

            return meeting;
        }

        [HttpPost]
        public ActionResult<Meeting> Create(Meeting meeting)
        {
            
            _meetingService.Create(meeting);

            return CreatedAtRoute("GetMeeting", new { id = meeting.Id.ToString() }, meeting);
        }

        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, Meeting meetingIn)
        //{
        //    var meeting = _meetingService.Get(id);
        //    if (meeting == null)
        //    {
        //        return NotFound();
        //    }
        //    _meetingService.Update(id, meetingIn);

        //    return NoContent();
        //}

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Member attendee)
        {
            var meeting = _meetingService.Get(id);
            if (meeting == null)
            {
                return NotFound();
            }
            _meetingService.Update(id, attendee);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var meeting = _meetingService.Get(id);

            if (meeting == null)
            {
                return NotFound();
            }

            _meetingService.Remove(meeting.Id);

            return NoContent();
        }
    }
}
