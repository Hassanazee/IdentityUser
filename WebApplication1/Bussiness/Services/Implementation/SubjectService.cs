using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;
using WebApplication1.Data.Repo.Implementation;
using WebApplication1.Data.Repo.Interface;
using WebApplication1.Data.UnitOfWork;
using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Services.Implementation
{
    public class SubjectService : BaseService<SubjectReq , SubjectRes , SubjectRepo , Subject> , ISubjectService
    {
        public SubjectService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public   override async Task<IActionResult> Add(SubjectReq reqModel)
        {
            try
            {
                var trans = await UnitOfWork.Context.Database.BeginTransactionAsync();
                {
                    var subject = new Subject
                    {
                    SubjectName=reqModel.SubjectName,
                   /*     Title = reqModel.Title,
                        Description = reqModel.Description,
                        StartDate = reqModel.StartDate,*/
                        //EndDate = reqModel.
                    };

                    await Repository.Add(subject);
                    await UnitOfWork.SaveAsync();

                    var subjectRes = new SubjectRes
                    {
                        SubjectName=reqModel.SubjectName,
                      /*  Title = subject.Title,
                        Description = subject.Description,
                        StartDate = DateTime.UtcNow,
                        //EndDate = subject.EndDate*/
                    };

                    await UnitOfWork.CommitTransactionAsync(trans);

                    return (IActionResult) subject;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the subject.", ex);
            }
        }

        public override Task<bool> Delete(Guid id)
        {
            return base.Delete(id);
        }

        public override async Task<IActionResult> Get(Guid id)
        {
            try
            {
                /* var subject = await UnitOfWork.Context.Subjects.Include(s => s.Grade).FirstOrDefaultAsync(s => s.Id == id);

                 if (subject != null)
                 {
                     var studentsInGrade = await UnitOfWork.Context.Students
                                             .Where(s => s.CurrentGradeId == subject.GradeId)
                                             .ToListAsync();

                     var response = new SubjectRes
                     {
                         Id = subject.Id,
                         SubjectName = subject.SubjectName,
                         Grades = new GradeRes
                         {
                             GradeId = subject.GradeId,
                             Student = subject.SubjectStudents.Select(x => new StudentRes
                             {
                                 Id = x.StudentId,
                             }).ToList(),

                         },

                         StudentsInGrade = studentsInGrade.Select(s => new StudentRes
                         {
                             Id = s.Id,
                             Name = s.Name
                         }).ToList()
                     };

                     return response.Ok();
                 }
                 else
                 {
                     throw new Exception("An error occurred.");

                 }*/

                var subject = await UnitOfWork.Context.Subjects
                .Include(s => s.Grade)
                .Include(s => s.SubjectStudents)
                .FirstOrDefaultAsync(s => s.Id == id);

                if (subject != null)
                {
                    var studentsInGrade = await UnitOfWork.Context.Students
                        .Where(s => s.CurrentGradeId == subject.GradeId)
                        .ToListAsync();

                    var response = new SubjectRes
                    {
                        Id = subject.Id,
                        SubjectName = subject.SubjectName,
                        GradeId = subject.GradeId,
                       
                       /* StudentsInGrade = studentsInGrade.Select(s => new StudentRes
                        {
                            Id = s.Id,
                            Name = s.Name
                        }).ToList()*/
                    };

                    return response.Ok();
                }
                else
                {
                    return null;
                }




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
                var subject = await UnitOfWork.Context.Subjects
                    .Include(x=> x.Grade).Include(x=> x.SubjectStudents)
                    .ToListAsync();

                List<SubjectRes> subjectRes = new List<SubjectRes>();

                foreach (var sub in subject)
                {
                    SubjectRes res = new SubjectRes()
                    {
                        SubjectName = sub.SubjectName,
                        Id = sub.Id,
                        GradeId =sub.GradeId,
                    };
                    subjectRes.Add(res);
                }
                return subjectRes.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override Task<IActionResult> Update(SubjectReq reqModel)
        {
            return base.Update(reqModel);
        }

        
    }
}
