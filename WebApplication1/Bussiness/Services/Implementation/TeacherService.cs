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
    public class TeacherService : BaseService<TeacherReq, TeacherRes, TeacherRepo, Teacher>, ITeacherService
    {
        public TeacherService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IActionResult> Add(TeacherReq reqModel)
        {
            try
            {
                var trans = await UnitOfWork.Context.Database.BeginTransactionAsync();

                Teacher teacher = new Teacher()
                {
                    Id = Guid.NewGuid(),
                    Name = reqModel.Name,
                    Salary = reqModel.Salary,
                    SubjectTitle = reqModel.SubjectTitle,
                    OrgId = reqModel.OrgId,
                    Student = reqModel.Students.Select(s => new Student
                    {
                        //Id = s.Id,
                        Name = s.Name
                    }).ToList()
                };

                await Repository.Add(teacher);
                await UnitOfWork.SaveAsync();
                await UnitOfWork.CommitTransactionAsync(trans);

                return teacher.Ok();
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
                var teachers = await UnitOfWork.Context.Teachers.ToListAsync();

                List<TeacherRes> teacherResList = new List<TeacherRes>();

                foreach (var teacher in teachers)
                {
                    TeacherRes teacherRes = new TeacherRes()
                    {
                        Id = teacher.Id,
                        Name = teacher.Name,
                        Salary = teacher.Salary,
                        SubjectTitle = teacher.SubjectTitle,
                        OrgId = teacher.OrgId,
                        Students = teacher.Student.Select(s => new StudentRes
                        {
                            Id = s.Id,
                            Name = s.Name
                        }).ToList()
                    };

                    teacherResList.Add(teacherRes);
                }

                return teacherResList.Ok();
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
                var teacher = await UnitOfWork.Context.Teachers
                    .Include(t => t.Student)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (teacher == null)
                {
                    throw new Exception("Teacher not found");
                }

                TeacherRes teacherRes = new TeacherRes()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Salary = teacher.Salary,
                    SubjectTitle = teacher.SubjectTitle,
                    OrgId = teacher.OrgId,
                    Students = teacher.Student.Select(s => new StudentRes
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList()
                };

                return teacherRes.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override Task<IActionResult> Update(TeacherReq reqModel)
        {
            return base.Update(reqModel);
        }
    }

}
