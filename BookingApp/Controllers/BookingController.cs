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
using Stateless;

namespace BookingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {

        private readonly ILogger<BookingController> _logger;

        enum Trigger
        {
            ClientBookedInSendMail,
            ClientCheckOutInSendMail
        }
        
    enum State
        { Free, Occupied, Reserved, Cleaned, Unavailable }

        State _state = State.Free;

        StateMachine<State, Trigger> _machine;
        StateMachine<State, Trigger>.TriggerWithParameters<int> _setClientBookedTrigger;

        private readonly IServiceProvider _collection;

        public BookingController(IServiceProvider collection, ILogger<BookingController> logger)
        {
            _collection = collection;
            _logger = logger;

            _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);



            _machine.Configure(State.Free)
                .Permit(Trigger.ClientBookedInSendMail, State.Occupied);
        }

        [HttpPost]
        //[Route("Add")]
        public bool Add(AddBookingRequest request)
        {

            using (var scope = _collection.CreateScope())
            {
                var checkroomNo = _collection.GetService<IRoomService>().GetByRoomNo(request.RoomNo);
                if(checkroomNo == null)
                {
                    return false;
                }

                var checkroomAvailability = _collection.GetService<IBookingReservationService>().GetByRoomNo(request.RoomNo);
                if (checkroomAvailability.RoomStatus == true)
                {
                    return false;
                }

                var bookingReservation = _collection.GetService<IBookingReservationService>().Add(request);

                if (bookingReservation != null)
                {
                    _machine.Fire(Trigger.ClientBookedInSendMail);
                }
                return bookingReservation;
            }
        }

        [HttpGet]
        //[Route("Get")]
        public List<GetBookingResponse> Get()
        {
            using (var scope = _collection.CreateScope())
            {
                return _collection.GetService<IBookingReservationService>().GetAll();
            }
        }

        [HttpGet]
        [Route("dashboard")]
        public GetDashBoardResponse Dashboard()
        {
            GetDashBoardResponse getDashBoardResponse = new GetDashBoardResponse();
            using (var scope = _collection.CreateScope())
            {
                getDashBoardResponse.rooms = _collection.GetService<IRoomService>().GetAll().Count();
                getDashBoardResponse.customers = _collection.GetService<ICustomerService>().GetAll().Count();
                getDashBoardResponse.checkins = _collection.GetService<IBookingReservationService>().GetCheckedIn().Count();
                getDashBoardResponse.checkouts = _collection.GetService<IBookingReservationService>().GetCheckedOut().Count();
                return getDashBoardResponse;
            }
        }
    }
}
