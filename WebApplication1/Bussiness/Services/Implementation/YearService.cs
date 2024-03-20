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
    public class YearService : BaseService<YearReq, YearRes, YearRepo, Year>, IYearService
    {
        public YearService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IActionResult> Add(YearReq reqModel)
        {
            try
            {
                var trans = await UnitOfWork.Context.Database.BeginTransactionAsync();

                Year year = new Year()
                {
                    Id = Guid.NewGuid(),
                    GradeName = reqModel.GradeName,
                    EnrollmentYear = reqModel.EnrollmentYear,
                    EndYear = reqModel.EndYear
                };

                await Repository.Add(year);
                await UnitOfWork.SaveAsync();
                await UnitOfWork.CommitTransactionAsync(trans);

                return year.Ok();
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
                var years = await UnitOfWork.Context.Years.ToListAsync();

                List<YearRes> yearResList = new List<YearRes>();

                foreach (var year in years)
                {
                    YearRes yearRes = new YearRes()
                    {
                        Id = year.Id,
                        GradeName = year.GradeName,
                        EnrollmentYear = year.EnrollmentYear,
                        EndYear = year.EndYear,
                        Grades = year.Grade.Select(g => new GradeRes
                        {
                            GradeId = g.Id,
                        }).ToList()
                    };

                    yearResList.Add(yearRes);
                }

                return yearResList.Ok();
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
                var year = await UnitOfWork.Context.Years.Include(y => y.Grade).FirstOrDefaultAsync(x => x.Id == id);

                if (year == null)
                {
                    throw new Exception("Year not found");
                }

                YearRes yearRes = new YearRes()
                {
                    Id = year.Id,
                    GradeName = year.GradeName,
                    EnrollmentYear = year.EnrollmentYear,
                    EndYear = year.EndYear,
                    Grades = year.Grade.Select(g => new GradeRes
                    {
                        GradeId = g.Id,
                    }).ToList()
                };

                return yearRes.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override Task<IActionResult> Update(YearReq reqModel)
        {
            return base.Update(reqModel);
        }
    }

}
