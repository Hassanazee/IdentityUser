using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;
using WebApplication1.Data.Repo.Implementation;
using WebApplication1.Data.UnitOfWork;
using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Services.Implementation
{
    public class StudentService : BaseService<StudentReq , StudentRes , StudentRepo , Student> , IStudentService
    {
      

        public StudentService(IUnitOfWork unitOfWork ) :base(unitOfWork)
        {
          
        }

        public  async Task<Student> AddStudent(StudentReq reqModel)
        {
            try
            {
                var trans = await UnitOfWork.Context.Database.BeginTransactionAsync();
                {
                    var student = new Student
                    {
                        Id = Guid.NewGuid(),
                        Name = reqModel.Name,
                        CurrentGradeId = reqModel.CurrentGradeId,
                        teacherId = reqModel.TeacherId,
                        RoomId = reqModel.RoomId,
                        OrgId = reqModel.OrgId,
                    };

                    await Repository.Add(student);
                    await UnitOfWork.SaveAsync();


                    await UnitOfWork.CommitTransactionAsync(trans);

                    return student;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the student.", ex);
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

        /*   public override async Task<IActionResult> Get(Guid id)
           {
               try
               {
                  // var student = await UnitOfWork.Context.Students.FirstOrDefaultAsync(x => x.Id == id);
                   var student = await UnitOfWork.Context.Students
             .Include(s => s.SubjectStudents)
             .FirstOrDefaultAsync(s => s.Id == id);
                   if (student != null)
                   {
                       var studentRes = new StudentRes
                       {
                           Id = student.Id,
                           Name = student.Name
                       };
                       return studentRes.Ok();
                   }
                   else
                   {
                       throw new Exception("Student not found");
                   }

               }
               catch (Exception e)
               {
                   Console.WriteLine(e);
                   throw new Exception("An error occurred.", e);

               }
           }*/



        public override async Task<IActionResult> Get(Guid id)
        {
            /*try
            {
                var student = await UnitOfWork.Context.Students
                    .Include(s => s.SubjectStudents)
                        .ThenInclude(ss => ss.Subject).Include(s=>s.CurrentGrade)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (student != null)
                {
                    var studentResponse = new StudentRes
                    {
                        Id = student.Id,
                        Name = student.Name,
                        GradeId = (Guid)student.CurrentGradeId,
                        
                        SubjectStudents = student.SubjectStudents.Select(x=> new SubjectStudent
                        {
                            StudentId = x.StudentId,
                            SubjectId = x.SubjectId,
                        }).ToList(),
                        subjects = student.SubjectStudents.Select(ss => new SubjectRes
                        {
                            Id = ss.Subject.Id,
                            SubjectName = ss.Subject.SubjectName,

                        }).ToList()
                    };

                    return studentResponse.Ok();
                }
                else
                {
                    return null;
                }
            }*/

            try
            {
                var student = await UnitOfWork.Context.Students.FirstOrDefaultAsync(x => x.Id == id);

                if (student == null)
                {
                    throw new Exception("Student not found");
                }

                StudentRes studentRes = new StudentRes()
                {
                    Id = student.Id,
                    Name = student.Name,
                    GradeId = (Guid)student.CurrentGradeId,
                    OrgId = student.OrgId,
                    TeacherId = student.teacherId,
                    RoomId = student.RoomId,
                    SubjectStudents = student.SubjectStudents.Select(x => new SubjectStudent
                    {
                        StudentId = x.StudentId,
                        SubjectId = x.SubjectId,
                    }).ToList(),
                    subjects = student.SubjectStudents.Select(ss => new SubjectRes
                    {
                        Id = ss.Subject.Id,
                        SubjectName = ss.Subject.SubjectName,

                    }).ToList()

                };

                return studentRes.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override async Task<IActionResult> GetAll(Pagination pagination)
        {
            /* try 
             {
                 //  var Students = await UnitOfWork.Context.Students.ToListAsync();
                 var Students = await UnitOfWork.Context.Students
                         .Include(x => x.SubjectStudents).ToListAsync();

                 List<StudentRes> studentRes = new List<StudentRes>();

                 foreach( var student in Students)
                 {
                     StudentRes res = new StudentRes()
                     {
                        Id = student.Id,
                        Name = student.Name,
                        GradeId = student.CurrentGrade.Id,
                        Grade = student.CurrentGrade,
                       SubjectStudents = student.SubjectStudents.Select(x=> new SubjectStudent
                       {
                           StudentId = x.StudentId,
                           SubjectId = x.SubjectId,
                           Student = x.Student,
                           Subject = x.Subject
                       }).ToList(),

                     };
                     studentRes.Add(res);
                 }
                 return studentRes.Ok();
             }
             catch (Exception e)
             {
                 Console.WriteLine(e);
                 throw new Exception("An error occurred.", e);
             }*/

            try
            {
                var students = await UnitOfWork.Context.Students
                    .Include(s => s.SubjectStudents)
                    .ThenInclude(ss => ss.Subject).Include(s => s.CurrentGrade)
                    .ToListAsync();

                var studentResList = new List<StudentRes>();

                foreach (var student in students)
                {
                    var studentRes = new StudentRes
                    {
                        Id = student.Id,
                        Name = student.Name,
                        GradeId = (Guid)student.CurrentGradeId,
                        Grade = student.CurrentGrade,
                        SubjectStudents = (IList<SubjectStudent>)student.SubjectStudents.Select(o=> new SubjectStudentRes
                        {
                            SubjectId = (Guid)o.SubjectId,
                        }).ToList(),    
                    };

                    studentResList.Add(studentRes);
                }
                return studentResList.Ok();

            }


            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }


        }

        public override Task<IActionResult> Update(StudentReq reqModel)
        {
            return base.Update(reqModel);
        }

    }

}

