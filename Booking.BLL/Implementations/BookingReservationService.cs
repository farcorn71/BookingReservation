using System;
using Microsoft.Extensions.DependencyInjection;
using Booking.BLL.Business.Abstractions;
using Booking.Core.Data.Repository.Abstractions;
using Booking.Core.Entities.BusinessEntities;
using Booking.Core.Entities.ClientEntities;
using Booking.Core.Entities.ClientEntities.Request;
using Booking.Core.Entities.ClientEntities.Response;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Booking.BLL.Business.Implementations
{
    public class BookingReservationService : IBookingReservationService
    {
        private readonly IServiceProvider _collection;

        public BookingReservationService(IServiceProvider collection)
        {
            _collection = collection;
        }

        public bool Add(AddBookingRequest request)
        {
            BookingReservation entity = new BookingReservation();
            //entity.BirthDate = request.BirthDate;
            entity.CustomerId = request.CustomerId;
            entity.RoomNo = request.RoomNo;
            entity.RoomStatus = true;
            entity.NoOfdays = request.NoOfdays;

            var bookingReservation = _collection.GetService<IBookingReservationRepository>().Add(entity);

            return bookingReservation != null ? true : false;
        }

        public GetBookingResponse Get(Guid request)
        {
            GetBookingResponse _booking = new GetBookingResponse();

            var response = _collection.GetService<IBookingReservationRepository>().GetById(request);
            
            if (response != null)
            {
                _booking.Id = response.Id;
                _booking.RoomNo = response.RoomNo;
                _booking.CustomerId = response.CustomerId;
                _booking.NoOfdays = response.NoOfdays;
                _booking.RoomStatus = response.RoomStatus;
            }
            return _booking;
        }



        public List<GetBookingResponse> GetAll()
        {
            List <GetBookingResponse> responses = new List<GetBookingResponse>();

            IEnumerable<BookingReservation> _bookings = _collection.GetService<IBookingReservationRepository>().GetAll().AsEnumerable();

            foreach(var _booking in _bookings)
            {
                GetBookingResponse booking = new GetBookingResponse();
                booking.Id = _booking.Id;
                booking.RoomNo = _booking.RoomNo;
                booking.CustomerId = _booking.CustomerId;
                booking.NoOfdays = _booking.NoOfdays;
                booking.RoomStatus = _booking.RoomStatus;
                responses.Add(booking);
                //response.BirthDate = customer.BirthDate;
            }
            return responses;
        }

        public List<GetBookingResponse> GetCheckedIn()
        {
            List<GetBookingResponse> responses = new List<GetBookingResponse>();

            var filter = Builders<BookingReservation>.Filter.Eq("RoomStatus", true);
            IEnumerable<BookingReservation> _bookings = _collection.GetService<IBookingReservationRepository>().GetListBySpecfied(filter);

            foreach (var _booking in _bookings)
            {
                GetBookingResponse booking = new GetBookingResponse();
                booking.Id = _booking.Id;
                booking.RoomNo = _booking.RoomNo;
                booking.CustomerId = _booking.CustomerId;
                booking.NoOfdays = _booking.NoOfdays;
                booking.RoomStatus = _booking.RoomStatus;
                responses.Add(booking);
                //response.BirthDate = customer.BirthDate;
            }
            return responses;
        }

        public List<GetBookingResponse> GetCheckedOut()
        {
            List<GetBookingResponse> responses = new List<GetBookingResponse>();

            var filter = Builders<BookingReservation>.Filter.Eq("RoomStatus", false);
            IEnumerable<BookingReservation> _bookings = _collection.GetService<IBookingReservationRepository>().GetListBySpecfied(filter);

            foreach (var _booking in _bookings)
            {
                GetBookingResponse booking = new GetBookingResponse();
                booking.Id = _booking.Id;
                booking.RoomNo = _booking.RoomNo;
                booking.CustomerId = _booking.CustomerId;
                booking.NoOfdays = _booking.NoOfdays;
                booking.RoomStatus = _booking.RoomStatus;
                responses.Add(booking);
                //response.BirthDate = customer.BirthDate;
            }
            return responses;
        }

        public GetBookingResponse GetByCustomerId(Guid customerId)
        {
            GetBookingResponse _booking = new GetBookingResponse();

            var filter = Builders<BookingReservation>.Filter.Eq("CustomerId", customerId);
            var response = _collection.GetService<IBookingReservationRepository>().GetBySpecfied(filter);

            if (response != null)
            {
                _booking.Id = response.Id;
                _booking.RoomNo = response.RoomNo;
                _booking.CustomerId = response.CustomerId;
                _booking.NoOfdays = response.NoOfdays;
                _booking.RoomStatus = response.RoomStatus;
            }
            return _booking;
        }

        public GetBookingResponse GetByRoomNo(string roomNo)
        {
            GetBookingResponse _booking = new GetBookingResponse();

            var filter = Builders<BookingReservation>.Filter.Eq("RoomNo", roomNo);
            var response = _collection.GetService<IBookingReservationRepository>().GetBySpecfied(filter);

            if (response != null)
            {
                _booking.Id = response.Id;
                _booking.RoomNo = response.RoomNo;
                _booking.CustomerId = response.CustomerId;
                _booking.NoOfdays = response.NoOfdays;
                _booking.RoomStatus = response.RoomStatus;
            }
            return _booking;
        }

        

        public bool Delete(Guid request)
        {
            _collection.GetService<IRoomRepository>().Remove(request);
            return true;
        }
    }
}
