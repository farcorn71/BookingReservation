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
using Booking.BLL;
using Booking.Core.Entities.BusinessEntities;

namespace BookingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
       

        private readonly ILogger<CustomerController> _logger;
        private readonly IEmailHelper _emailHelper;
        private readonly IEmailSender _emailSender;


        private readonly IServiceProvider _collection;

        enum Trigger
        {
            ClientRegisteredSendMail,
            ClientBookedInSendMail
        }

        enum State
        { New, Registered, BookedRoom, CheckIn, CheckOut    }

        State _state = State.Registered;

        public CustomerController(IServiceProvider collection, ILogger<CustomerController> logger, IEmailSender emailSender, IEmailHelper emailHelper)
        {
            _collection = collection;
            _logger = logger;
            _emailHelper = emailHelper;
            _emailSender = emailSender;



            // Instantiate a new state machine in the New state
            _machine = new StateMachine<State, Trigger>(State.New);

            _machine.Configure(State.New)
                .Permit(Trigger.ClientRegisteredSendMail, State.Registered);

            // Instantiate a new trigger with a parameter. 
            _setClientRegistrationTrigger = _machine.SetTriggerParameters<string>(Trigger.ClientRegisteredSendMail);

            // Configure the Registered state
            _machine.Configure(State.Registered)
                .SubstateOf(State.New)
                .OnEntryFrom(_setClientRegistrationTrigger, OnClientRegisteredSendMail)  // This is where the TriggerWithParameters is used. Note that the TriggerWithParameters object is used, not something from the enum
                .PermitReentry(Trigger.ClientRegisteredSendMail);
        }



        StateMachine<State, Trigger> _machine;
        StateMachine<State, Trigger>.TriggerWithParameters<string> _setClientRegistrationTrigger;


        [Authorize]
        [HttpPost]
        //[Route("Add")]
        public bool Add(AddCustomerRequest request)
        {
            using (var scope = _collection.CreateScope())
            {
                var currentUser = (User)HttpContext.Items["User"];
                request.ActionBy = currentUser.Username;
                request.ActionPerformed = "AddCustomer";
                
                var client = _collection.GetService<ICustomerService>().Add(request);
                if(client != true)
                {
                    ClientRegisteredSendMail(request.EmailAddress);
                }
                return client;
            }
        }

        [HttpGet]
        //[Route("Get")]
        public List<GetCustomerResponse> Get()
        {
            using (var scope = _collection.CreateScope())
            {
                return _collection.GetService<ICustomerService>().GetAll();
            }
        }

        public void OnClientRegisteredSendMail(string newCustomer) {
            var builder = _emailHelper.BodyBuilder("CustomerRegistration.html");

            _emailSender.SendMail(newCustomer, "New Customer", "");
        }

        public void ClientRegisteredSendMail(string newCustomer)
        {
            // This is how a trigger with parameter is used, the parameter is supplied to the state machine as a parameter to the Fire method.
            _machine.Fire(_setClientRegistrationTrigger, newCustomer);
        }
    }
}
