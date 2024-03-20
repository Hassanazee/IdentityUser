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
    public class TimeTableService : BaseService<TimeTableReq, TimeTableRes, TimeTableRepo, TimeTable>, ITimeTableService
    {
        public TimeTableService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IActionResult> Add(TimeTableReq reqModel)
        {
            try
            {
                var trans = await UnitOfWork.Context.Database.BeginTransactionAsync();

                TimeTable timeTable = new TimeTable()
                {
                    Id = Guid.NewGuid(),
                    Name = reqModel.Name,
                    StartDate = reqModel.StartDate,
                    EndDate = reqModel.EndDate
                };

                await Repository.Add(timeTable);
                await UnitOfWork.SaveAsync();
                await UnitOfWork.CommitTransactionAsync(trans);

                return timeTable.Ok();
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
                var timeTables = await UnitOfWork.Context.TimeTables.ToListAsync();

                List<TimeTableRes> timeTableResList = new List<TimeTableRes>();

                foreach (var timeTable in timeTables)
                {
                    TimeTableRes timeTableRes = new TimeTableRes()
                    {
                        Id = timeTable.Id,
                        Name = timeTable.Name,
                        StartDate = timeTable.StartDate,
                        EndDate = timeTable.EndDate
                    };

                    timeTableResList.Add(timeTableRes);
                }

                return timeTableResList.Ok();
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
                var timeTable = await UnitOfWork.Context.TimeTables.FirstOrDefaultAsync(x => x.Id == id);

                if (timeTable == null)
                {
                    throw new Exception("TimeTable not found");
                }

                TimeTableRes timeTableRes = new TimeTableRes()
                {
                    Id = timeTable.Id,
                    Name = timeTable.Name,
                    StartDate = timeTable.StartDate,
                    EndDate = timeTable.EndDate
                };

                return timeTableRes.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override Task<IActionResult> Update(TimeTableReq reqModel)
        {
            return base.Update(reqModel);
        }
    }

}
