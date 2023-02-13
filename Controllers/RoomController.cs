using JobsityChallengeAPI.DTOs;
using JobsityChallengeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsityChallengeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ChatRoom _chatRoom;

        public RoomController(ChatRoom chatRoom)
        {
            _chatRoom = chatRoom;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDTO model)
        {
            if (_chatRoom.AddUserToList(model.Name))
            {
               
                return NoContent();
            }
            return BadRequest("This name is taken, please choose another");
        }
    }
}
