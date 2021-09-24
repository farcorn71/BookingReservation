using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Core.Entities.ClientEntities.Request;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Booking.BLL.Business.Abstractions;
using Booking.Core.Entities.ClientEntities.Response;

namespace BookingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {

        private readonly ILogger<RoomController> _logger;


        private readonly IServiceProvider _collection;

        public RoomController(IServiceProvider collection, ILogger<RoomController> logger)
        {
            _collection = collection;
            _logger = logger;
        }

        [HttpPost]
        //[Route("Add")]
        public bool Add(AddRoomRequest request)
        {
            using (var scope = _collection.CreateScope())
            {
                return _collection.GetService<IRoomService>().Add(request);
            }
        }

        [HttpGet]
        //[Route("Get")]
        public List<GetRoomResponse> Get()
        {
            using (var scope = _collection.CreateScope())
            {
                return _collection.GetService<IRoomService>().GetAll();
            }
        }
    }
}
