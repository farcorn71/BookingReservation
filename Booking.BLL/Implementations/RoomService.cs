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
    public class RoomService : IRoomService
    {
        private readonly IServiceProvider _collection;

        public RoomService(IServiceProvider collection)
        {
            _collection = collection;
        }

        public bool Add(AddRoomRequest request)
        {
            Room entity = new Room();
            //entity.BirthDate = request.BirthDate;
            entity.RoomName = request.RoomName;
            entity.RoomNo = request.RoomNo;
            entity.Price = request.Price;
            entity.ServicesDescription = request.ServicesDescription;
            entity.CreateDate = DateTime.Now;

            entity.ActionBy = request.ActionBy;
            entity.ActionPerformed = request.ActionPerformed;

            var room = _collection.GetService<IRoomRepository>().Add(entity);

            return room != null ? true : false;
        }

        public GetRoomResponse Get(Guid request)
        {
            GetRoomResponse room = new GetRoomResponse();

            var response = _collection.GetService<IRoomRepository>().GetById(request);

            if (response != null)
            {
                room.Id = response.Id;
                room.RoomName = response.RoomName;
                room.RoomNo = response.RoomNo;
                room.Price = response.Price;
                room.ServicesDescription = response.ServicesDescription;
                room.ActionBy = response.ActionBy;
                room.ActionPerformed = response.ActionPerformed;
            }
            return room;
        }

        public GetRoomResponse GetByRoomNo(string roomNo)
        {
            GetRoomResponse room = new GetRoomResponse();
            var filter = Builders<Room>.Filter.Eq("RoomNo", roomNo);
            var response = _collection.GetService<IRoomRepository>().GetBySpecfied(filter);

            if (response != null)
            {
                room.Id = response.Id;
                room.RoomName = response.RoomName;
                room.RoomNo = response.RoomNo;
                room.Price = response.Price;
                room.ServicesDescription = response.ServicesDescription;
                room.ActionBy = response.ActionBy;
                room.ActionPerformed = response.ActionPerformed;
            }
            return room;
        }

        public List<GetRoomResponse> GetAll()
        {
            List <GetRoomResponse> responses = new List<GetRoomResponse>();

            IEnumerable<Room> rooms = _collection.GetService<IRoomRepository>().GetAll().AsEnumerable();

            foreach(var room in rooms)
            {
                GetRoomResponse response = new GetRoomResponse();
                response.Id = room.Id;
                response.RoomName = room.RoomName;
                response.RoomNo = room.RoomNo;
                response.Price = room.Price;
                response.ServicesDescription = room.ServicesDescription;
                room.ActionBy = response.ActionBy;
                room.ActionPerformed = response.ActionPerformed;
                responses.Add(response);
                //response.BirthDate = customer.BirthDate;
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
