using System;
using Microsoft.Extensions.DependencyInjection;
using Booking.BLL.Business.Abstractions;
using Booking.Core.Data.Repository.Abstractions;
using Booking.Core.Entities.BusinessEntities;
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
            entity.ActionBy = request.ActionBy;
            entity.ActionPerformed = request.ActionPerformed;
            entity.Amount = request.Amount;
            entity.CreateDate = DateTime.Now;

            var bookingReservation = _collection.GetService<IBookingReservationRepository>().Add(entity);

            return bookingReservation != null ? true : false;
        }

        public void Update(AddBookingRequest request)
        {
            BookingReservation entity = new BookingReservation();
            var filter = Builders<BookingReservation>.Filter.Eq("CustomerId", request.CustomerId);
            filter &= (Builders<BookingReservation>.Filter.Eq("RoomStatus", true));
            var response = _collection.GetService<IBookingReservationRepository>().GetBySpecfied(filter);
            
            response.RoomStatus = false;

            response.ActionBy = response.ActionBy;
            response.ActionPerformed = response.ActionPerformed;
            response.UpdatedDate = DateTime.Now;

            _collection.GetService<IBookingReservationRepository>().Update(response);

            //entity.BirthDate = request.BirthDate;
            entity.CustomerId = response.CustomerId;
            entity.RoomNo = response.RoomNo;
            entity.RoomStatus = false;
            entity.NoOfdays = response.NoOfdays;
            entity.ActionBy = request.ActionBy;
            entity.ActionPerformed = request.ActionPerformed;
            entity.Amount = response.Amount;
            entity.CreateDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;

            _collection.GetService<IBookingReservationRepository>().Add(entity);
        }

        public GetBookingResponse Get(Guid request)
        {
            GetBookingResponse _booking = new GetBookingResponse();

            var response = _collection.GetService<IBookingReservationRepository>().GetById(request);
            
            if (response != null)
            {
                var filter = Builders<Customer>.Filter.Eq("Id", response.CustomerId);
                var customerdetail = _collection.GetService<ICustomerRepository>().GetBySpecfied(filter);
                _booking.Id = response.Id;
                _booking.RoomNo = response.RoomNo;
                _booking.CustomerId = response.CustomerId;
                _booking.CustomerName = customerdetail.FirstName + " " + customerdetail.LastName;
                _booking.NoOfdays = response.NoOfdays;
                _booking.RoomStatus = response.RoomStatus;
                _booking.Amount = response.Amount;
                _booking.ActionBy = response.ActionBy;
                _booking.ActionPerformed = response.ActionPerformed;
                _booking.ActionDate = response.RoomStatus == true ? "CheckedIn at " + response.CreateDate : "CheckedOut at " + response.UpdatedDate;
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

                var filter = Builders<Customer>.Filter.Eq("Id", _booking.CustomerId);
                var customerdetail = _collection.GetService<ICustomerRepository>().GetBySpecfied(filter);
                booking.Id = _booking.Id;
                booking.RoomNo = _booking.RoomNo;
                booking.CustomerId = _booking.CustomerId;
                booking.CustomerName = customerdetail.FirstName + " " + customerdetail.LastName;
                booking.NoOfdays = _booking.NoOfdays;
                booking.RoomStatus = _booking.RoomStatus;
                booking.Amount = _booking.Amount;
                booking.ActionBy = _booking.ActionBy;
                booking.ActionPerformed = _booking.ActionPerformed;
                booking.ActionDate = _booking.RoomStatus == true ? "CheckedIn at " + _booking.CreateDate : "CheckedOut at " + _booking.UpdatedDate;

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
                var filt = Builders<Customer>.Filter.Eq("Id", _booking.CustomerId);
                var customerdetail = _collection.GetService<ICustomerRepository>().GetBySpecfied(filt);
                booking.Id = _booking.Id;
                booking.RoomNo = _booking.RoomNo;
                booking.CustomerId = _booking.CustomerId;
                booking.CustomerName = customerdetail.FirstName + " " + customerdetail.LastName;
                booking.NoOfdays = _booking.NoOfdays;
                booking.RoomStatus = _booking.RoomStatus;
                booking.Amount = _booking.Amount;
                booking.ActionBy = _booking.ActionBy;
                booking.ActionPerformed = _booking.ActionPerformed;
                booking.ActionDate = _booking.RoomStatus == true ? "CheckedIn at " + _booking.CreateDate : "CheckedOut at " + _booking.UpdatedDate;

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
                var filt = Builders<Customer>.Filter.Eq("Id", _booking.CustomerId);
                var customerdetail = _collection.GetService<ICustomerRepository>().GetBySpecfied(filt);
                booking.Id = _booking.Id;
                booking.RoomNo = _booking.RoomNo;
                booking.CustomerId = _booking.CustomerId;
                booking.CustomerName = customerdetail.FirstName + " " + customerdetail.LastName;
                booking.NoOfdays = _booking.NoOfdays;
                booking.RoomStatus = _booking.RoomStatus;
                booking.Amount = _booking.Amount;
                booking.ActionBy = _booking.ActionBy;
                booking.ActionPerformed = _booking.ActionPerformed;
                booking.ActionDate = _booking.RoomStatus == true ? "CheckedIn at " + _booking.CreateDate : "CheckedOut at " + _booking.UpdatedDate;

                responses.Add(booking);
                //response.BirthDate = customer.BirthDate;
            }
            return responses;
        }

        public GetBookingResponse GetByCustomerLastBookingId(string customerId)
        {
            GetBookingResponse _booking = new GetBookingResponse();

            var filter = Builders<BookingReservation>.Filter.Eq("CustomerId", customerId);
            filter &= (Builders<BookingReservation>.Filter.Eq("RoomStatus", true));
            var response = _collection.GetService<IBookingReservationRepository>().GetBySpecfied(filter);

            if (response != null)
            {
                var filt = Builders<Customer>.Filter.Eq("Id", response.CustomerId);
                var customerdetail = _collection.GetService<ICustomerRepository>().GetBySpecfied(filt);
                _booking.Id = response.Id;
                _booking.RoomNo = response.RoomNo;
                _booking.CustomerId = response.CustomerId;
                _booking.CustomerName = customerdetail.FirstName + " " + customerdetail.LastName;
                _booking.NoOfdays = response.NoOfdays;
                _booking.RoomStatus = response.RoomStatus;
                _booking.Amount = response.Amount;
                _booking.ActionBy = response.ActionBy;
                _booking.ActionPerformed = response.ActionPerformed;
                _booking.ActionDate = response.RoomStatus == true ? "CheckedIn at " + response.CreateDate : "CheckedOut at " + response.UpdatedDate;

            }
            return _booking;
        }

        public List<GetBookingResponse> GetByCustomerId(string customerId)
        {
            List<GetBookingResponse> responses = new List<GetBookingResponse>();

            var filter = Builders<BookingReservation>.Filter.Eq("CustomerId", customerId);
            var response = _collection.GetService<IBookingReservationRepository>().GetBySpecfied(filter);

            IEnumerable<BookingReservation> _bookings = _collection.GetService<IBookingReservationRepository>().GetListBySpecfied(filter);

            foreach (var _booking in _bookings)
            {
                GetBookingResponse booking = new GetBookingResponse();
                var filt = Builders<Customer>.Filter.Eq("Id", _booking.CustomerId);
                var customerdetail = _collection.GetService<ICustomerRepository>().GetBySpecfied(filt);
                booking.Id = _booking.Id;
                booking.RoomNo = _booking.RoomNo;
                booking.CustomerId = _booking.CustomerId;
                booking.CustomerName = customerdetail.FirstName + " " + customerdetail.LastName;
                booking.NoOfdays = _booking.NoOfdays;
                booking.RoomStatus = _booking.RoomStatus;
                booking.Amount = _booking.Amount;
                booking.ActionBy = _booking.ActionBy;
                booking.ActionPerformed = _booking.ActionPerformed;
                booking.ActionDate = _booking.RoomStatus == true ? "CheckedIn at " + _booking.CreateDate : "CheckedOut at " + _booking.UpdatedDate;

                responses.Add(booking);
                //response.BirthDate = customer.BirthDate;
            }

            //if (response != null)
            //{
            //    var filt = Builders<Customer>.Filter.Eq("Id", response.CustomerId);
            //    var customerdetail = _collection.GetService<ICustomerRepository>().GetBySpecfied(filt);
            //    _booking.Id = response.Id;
            //    _booking.RoomNo = response.RoomNo;
            //    _booking.CustomerId = response.CustomerId;
            //    _booking.CustomerName = customerdetail.FirstName + " " + customerdetail.LastName;
            //    _booking.NoOfdays = response.NoOfdays;
            //    _booking.RoomStatus = response.RoomStatus;
            //    _booking.Amount = response.Amount;
            //    _booking.ActionBy = response.ActionBy;
            //    _booking.ActionPerformed = response.ActionPerformed;
            //    _booking.ActionDate = response.RoomStatus == true ? "CheckedIn at " + response.CreateDate : "CheckedOut at " + response.UpdatedDate;

            //}
            return responses;
        }

        public GetBookingResponse GetByRoomNoLastBooking(string roomNo)
        {
            GetBookingResponse _booking = new GetBookingResponse();

            var filter = Builders<BookingReservation>.Filter.Eq("RoomNo", roomNo);
            filter &= (Builders<BookingReservation>.Filter.Eq("RoomStatus", true));
            var response = _collection.GetService<IBookingReservationRepository>().GetBySpecfied(filter);

            if (response != null)
            {
                var filt = Builders<Customer>.Filter.Eq("Id", response.CustomerId);
                var customerdetail = _collection.GetService<ICustomerRepository>().GetBySpecfied(filt);
                _booking.Id = response.Id;
                _booking.RoomNo = response.RoomNo;
                _booking.CustomerId = response.CustomerId;
                _booking.CustomerName = customerdetail.FirstName + " " + customerdetail.LastName;
                _booking.NoOfdays = response.NoOfdays;
                _booking.RoomStatus = response.RoomStatus;
                _booking.Amount = response.Amount;
                _booking.ActionBy = response.ActionBy;
                _booking.ActionPerformed = response.ActionPerformed;
                _booking.ActionDate = response.RoomStatus == true ? "CheckedIn at " + response.CreateDate : "CheckedOut at " + response.UpdatedDate;

            }
            return _booking;
        }

        public List<GetBookingResponse> GetByRoomNo(string roomNo)
        {
            List<GetBookingResponse> responses = new List<GetBookingResponse>();

            var filter = Builders<BookingReservation>.Filter.Eq("RoomNo", roomNo);
            var response = _collection.GetService<IBookingReservationRepository>().GetBySpecfied(filter);

            IEnumerable<BookingReservation> _bookings = _collection.GetService<IBookingReservationRepository>().GetListBySpecfied(filter);

            foreach (var _booking in _bookings)
            {
                GetBookingResponse booking = new GetBookingResponse();
                var filt = Builders<Customer>.Filter.Eq("Id", _booking.CustomerId);
                var customerdetail = _collection.GetService<ICustomerRepository>().GetBySpecfied(filt);
                booking.Id = _booking.Id;
                booking.RoomNo = _booking.RoomNo;
                booking.CustomerId = _booking.CustomerId;
                booking.CustomerName = customerdetail.FirstName + " " + customerdetail.LastName;
                booking.NoOfdays = _booking.NoOfdays;
                booking.RoomStatus = _booking.RoomStatus;
                booking.Amount = _booking.Amount;
                booking.ActionBy = _booking.ActionBy;
                booking.ActionPerformed = _booking.ActionPerformed;
                booking.ActionDate = _booking.RoomStatus == true ? "CheckedIn at " + _booking.CreateDate : "CheckedOut at " + _booking.UpdatedDate;

                responses.Add(booking);
            }

            return responses;
        }


        public bool Delete(Guid request)
        {
            _collection.GetService<IRoomRepository>().Remove(request);
            return true;
        }
    }
}
