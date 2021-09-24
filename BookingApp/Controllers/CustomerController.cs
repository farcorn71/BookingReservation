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
    public class CustomerController : ControllerBase
    {
       

        private readonly ILogger<CustomerController> _logger;


        private readonly IServiceProvider _collection;

        enum Trigger
        {
            ClientRegisteredSendMail,
            ClientBookedInSendMail
        }

        enum State
        { Registered, BookedRoom, CheckIn, CheckOut    }

        State _state = State.Registered;

        public CustomerController(IServiceProvider collection, ILogger<CustomerController> logger)
        {
            _collection = collection;
            _logger = logger;

            _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);

          

            _machine.Configure(State.Registered)
                .Permit(Trigger.ClientBookedInSendMail, State.Registered);

        }


        StateMachine<State, Trigger> _machine;
        StateMachine<State, Trigger>.TriggerWithParameters<int> _setClientRegistrationTrigger;

        StateMachine<State, Trigger>.TriggerWithParameters<string> _setCalleeTrigger;

        string _caller;

        string _callee;
        [HttpPost]
        //[Route("Add")]
        public bool Add(AddCustomerRequest request)
        {
            using (var scope = _collection.CreateScope())
            {
                var client = _collection.GetService<ICustomerService>().Add(request);
                if(client != null)
                {
                    _machine.Fire(Trigger.ClientBookedInSendMail);
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
    }
}
