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
using Booking.Core.Entities.BusinessEntities;
using Booking.BLL;

namespace BookingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {

        private readonly ILogger<BookingController> _logger;
        private readonly IEmailHelper _emailHelper;
        private readonly IEmailSender _emailSender;
        enum Trigger
        {
            ClientBookedInSendMail,
            ClientCheckOutInSendMail
        }
        
    enum State
        { Free, Occupied, Reserved, Cleaned, Unavailable }

        State _state = State.Free;

        StateMachine<State, Trigger> _machine;
        StateMachine<State, Trigger>.TriggerWithParameters<string> _setClientBookedTrigger;

        private readonly IServiceProvider _collection;

        public BookingController(IServiceProvider collection, ILogger<BookingController> logger,IEmailSender emailSender, IEmailHelper emailHelper)
        {
            _collection = collection;
            _logger = logger;

            _emailHelper = emailHelper;
            _emailSender = emailSender;



            // Instantiate a new state machine in the New state
            _machine = new StateMachine<State, Trigger>(State.Free);

            _machine.Configure(State.Free)
                .Permit(Trigger.ClientBookedInSendMail, State.Occupied);

            // Instantiate a new trigger with a parameter. 
            _setClientBookedTrigger = _machine.SetTriggerParameters<string>(Trigger.ClientBookedInSendMail);

            
        }
        [Authorize]
        [HttpPost]
        [Route("checkin")]
        public bool Add(AddBookingRequest request)
        {

            using (var scope = _collection.CreateScope())
            {
                var checkroomNo = _collection.GetService<IRoomService>().GetByRoomNo(request.RoomNo);
                if(checkroomNo == null)
                {
                    return false;
                }

                var checkroomAvailability = _collection.GetService<IBookingReservationService>().GetByRoomNoLastBooking(request.RoomNo);
                if (checkroomAvailability.RoomStatus == true)
                {
                    return false;
                }
                var isRoomBooked = _collection.GetService<IBookingReservationService>().GetByCustomerLastBookingId(request.CustomerId);
                if (isRoomBooked.RoomStatus == true)
                {
                    return false;
                }
                var currentUser = (User)HttpContext.Items["User"];
                request.ActionBy = currentUser.Username;
                request.ActionPerformed = "CheckIn";

                var roomDetails = _collection.GetService<IRoomService>().GetByRoomNo(request.RoomNo);

                request.Amount = roomDetails.Price * request.NoOfdays;

                var bookingReservation = _collection.GetService<IBookingReservationService>().Add(request);

                if (bookingReservation == true)
                {

                    var customer = _collection.GetService<ICustomerService>().Get(request.CustomerId);
                    ClientBookedInSendMail(customer.EmailAddress);
                }
                return bookingReservation;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("checkout")]
        public IActionResult CheckOut(AddBookingRequest request)
        {

            using (var scope = _collection.CreateScope())
            {
                

                var checkroomAvailability = _collection.GetService<IBookingReservationService>().GetByCustomerLastBookingId(request.CustomerId);
                if (checkroomAvailability == null) {
                    return Ok(new { status = false, message = "Room Not Booked by User" });
                }
                if (checkroomAvailability.RoomStatus == false)
                {
                    return Ok(new { status = false, message = "Room Already Checked Out"});
                }
                
                var currentUser = (User)HttpContext.Items["User"];
                request.ActionBy = currentUser.Username;
                request.ActionPerformed = "CheckOut";

                _collection.GetService<IBookingReservationService>().Update(request);

                
                //_machine.Fire(Trigger.ClientBookedInSendMail);
                
                return Ok(new { status = true, message = "Room Checked Out Successfully" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetByRoomNo")]
        public List<GetBookingResponse> Get(string roomNo)
        {
            using (var scope = _collection.CreateScope())
            {
                return _collection.GetService<IBookingReservationService>().GetByRoomNo(roomNo);
            }
        }
        [Authorize]
        [HttpGet]
        //[Route("Get")]
        public List<GetBookingResponse> Get()
        {
            using (var scope = _collection.CreateScope())
            {
                return _collection.GetService<IBookingReservationService>().GetAll();
            }
        }
        [Authorize]
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

        public void OnClientBookingSendMail(string newReservation)
        {
            var builder = _emailHelper.BodyBuilder("Reservation.html");

            _emailSender.SendMail(newReservation, "CheckIn Made", "");
        }

        public void ClientBookedInSendMail(string newReservation)
        {
            // This is how a trigger with parameter is used, the parameter is supplied to the state machine as a parameter to the Fire method.
            _machine.Fire(_setClientBookedTrigger, newReservation);
        }
    }
}
