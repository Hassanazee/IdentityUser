using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;

namespace WebApplication1.Controller
{
    public class RoomController : BaseController<RoomController, IRoomService, RoomReq, RoomRes>
    {
        public RoomController(ILogger<RoomController> logger, IRoomService roomService) : base(logger, roomService)
        {

        }
    }

}
