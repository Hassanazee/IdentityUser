using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;
using WebApplication1.Data.Repo.Implementation;
using WebApplication1.Data.UnitOfWork;
using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Services.Implementation
{
    public class RoomService : BaseService<RoomReq, RoomRes, RoomRepo, Room>, IRoomService
    {
        public RoomService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IActionResult> Add(RoomReq reqModel)
        {
            try
            {
                var trans = await UnitOfWork.Context.Database.BeginTransactionAsync();

                Room room = new Room()
                {
                    Id = Guid.NewGuid(),
                    OrgId = reqModel.OrgId,
                    ClassName = reqModel.ClassName,
                    Location = reqModel.Location,
                    TotalStaff = reqModel.TotalStaff,
                    TotalStudent = reqModel.TotalStudent,
                    RoomNumber = reqModel.RoomNumber,
                    Capacity = reqModel.Capacity
                };

                await Repository.Add(room);
                await UnitOfWork.SaveAsync();
                await UnitOfWork.CommitTransactionAsync(trans);

                return room.Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred.", ex);
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var res = await Repository.Delete(id);
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override async Task<IActionResult> GetAll(Pagination pagination)
        {
            try
            {
                var rooms = await UnitOfWork.Context.Rooms.ToListAsync();

                List<RoomRes> roomResList = new List<RoomRes>();

                foreach (var room in rooms)
                {
                    RoomRes roomRes = new RoomRes()
                    {
                        Id = room.Id,
                        ClassName = room.ClassName,
                        Location = room.Location,
                        TotalStaff = room.TotalStaff,
                        TotalStudent = room.TotalStudent,
                        RoomNumber = room.RoomNumber,
                        Capacity = room.Capacity
                    };

                    roomResList.Add(roomRes);
                }

                return roomResList.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var room = await UnitOfWork.Context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

                if (room == null)
                {
                    throw new Exception("Room not found");
                }

                RoomRes roomRes = new RoomRes()
                {
                    Id = room.Id,
                    ClassName = room.ClassName,
                    Location = room.Location,
                    TotalStaff = room.TotalStaff,
                    TotalStudent = room.TotalStudent,
                    RoomNumber = room.RoomNumber,
                    Capacity = room.Capacity,
                    OrgId = room.OrgId,
                    

                };

                return roomRes.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override Task<IActionResult> Update(RoomReq reqModel)
        {
            return base.Update(reqModel);
        }
    }

}
