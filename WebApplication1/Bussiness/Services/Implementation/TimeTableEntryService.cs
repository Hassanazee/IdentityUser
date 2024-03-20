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
    public class TimeTableEntryService : BaseService<TimeTableEntryReq, TimeTableEntryRes, TimeTableEntryRepo, TimeTableEntry>, ITimeTableEntryService
    {
        public TimeTableEntryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IActionResult> Add(TimeTableEntryReq reqModel)
        {
            try
            {
                var trans = await UnitOfWork.Context.Database.BeginTransactionAsync();

                TimeTableEntry timeTableEntry = new TimeTableEntry()
                {
                    Id = Guid.NewGuid(),
                    TimetableId = reqModel.TimetableId,
                    ClassId = reqModel.ClassId,
                    SubjectId = reqModel.SubjectId,
                    TeacherId = reqModel.TeacherId,
                    RoomId = reqModel.RoomId,
                    DayOfWeek = reqModel.DayOfWeek,
                    StartTime = reqModel.StartTime,
                    EndTime = reqModel.EndTime
                };

                await Repository.Add(timeTableEntry);
                await UnitOfWork.SaveAsync();
                await UnitOfWork.CommitTransactionAsync(trans);

                return timeTableEntry.Ok();
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
                var timeTableEntries = await UnitOfWork.Context.TimeTableEntries.ToListAsync();

                List<TimeTableEntryRes> timeTableEntryResList = new List<TimeTableEntryRes>();

                foreach (var timeTableEntry in timeTableEntries)
                {
                    TimeTableEntryRes timeTableEntryRes = new TimeTableEntryRes()
                    {
                        Id = timeTableEntry.Id,
                        TimetableId = timeTableEntry.TimetableId,
                        ClassId = timeTableEntry.ClassId,
                        SubjectId = timeTableEntry.SubjectId,
                        TeacherId = timeTableEntry.TeacherId,
                        RoomId = timeTableEntry.RoomId,
                        DayOfWeek = timeTableEntry.DayOfWeek,
                        StartTime = timeTableEntry.StartTime,
                        EndTime = timeTableEntry.EndTime
                    };

                    timeTableEntryResList.Add(timeTableEntryRes);
                }

                return timeTableEntryResList.Ok();
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
                var timeTableEntry = await UnitOfWork.Context.TimeTableEntries.FirstOrDefaultAsync(x => x.Id == id);

                if (timeTableEntry == null)
                {
                    throw new Exception("TimeTableEntry not found");
                }

                TimeTableEntryRes timeTableEntryRes = new TimeTableEntryRes()
                {
                    Id = timeTableEntry.Id,
                    TimetableId = timeTableEntry.TimetableId,
                    ClassId = timeTableEntry.ClassId,
                    SubjectId = timeTableEntry.SubjectId,
                    TeacherId = timeTableEntry.TeacherId,
                    RoomId = timeTableEntry.RoomId,
                    DayOfWeek = timeTableEntry.DayOfWeek,
                    StartTime = timeTableEntry.StartTime,
                    EndTime = timeTableEntry.EndTime
                };

                return timeTableEntryRes.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override Task<IActionResult> Update(TimeTableEntryReq reqModel)
        {
            return base.Update(reqModel);
        }
    }

}
