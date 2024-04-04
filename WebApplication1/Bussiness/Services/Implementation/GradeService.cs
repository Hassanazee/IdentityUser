using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;
using WebApplication1.Data.Repo.Implementation;
using WebApplication1.Data.Repo.Interface;
using WebApplication1.Data.UnitOfWork;
using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Services.Implementation
{
    public class GradeService : BaseService<GradeReq , GradeRes , GradeRepo , Grade> , IGradeService
    {
        
        public GradeService(IUnitOfWork unitOfWork ) : base(unitOfWork)
        {
        }

        public override async Task<IActionResult> Add(GradeReq reqModel)
        {
            try
            {
                var trans = await UnitOfWork.Context.Database.BeginTransactionAsync();
                var year = await UnitOfWork.Context.Years.FirstOrDefaultAsync(x => x.Id == reqModel.Year.YearId) ?? throw new Exception("Year not found");

                Grade grade = new Grade()
                {
                    Id = new Guid(),
                    Section = reqModel.Section,
                    GradeName = reqModel.GradeName,
                    OrganizationId = reqModel.OrgId,
                     Year = new Year()
                     {
                            Id = reqModel.Year.YearId,
                            EnrollmentYear = reqModel.Year.EnrollmentYear,
                            EndYear = reqModel.Year.EndYear,
                            GradeName = reqModel.Year.GradeName
                     }
                        /*     Students = reqModel.Student.Select(s => new Student
                             {
                                 Name = s.Name,
                             }).ToList(),
                             Subjects = reqModel.Subjects.Select(s=> new Subject
                             {
                                 Id = s.Id,
                                 SubjectName = s.SubjectName,

                             }).ToList(),*/
                    }; 
                    await Repository.Add(grade);
                    await UnitOfWork.SaveAsync();
                    await UnitOfWork.CommitTransactionAsync(trans);  
                return grade.Ok();  
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


        /*  public override async Task<IActionResult> GetAll(Pagination pagination)
          {
              try
              {
                  var gradeStudents = await UnitOfWork.Context.Grades.Include(x => x.Students).ToListAsync();
                  List<GradeRes> gradeResList = new List<GradeRes>();

                  foreach (var grade in gradeStudents)
                  {
                      GradeRes gradeRes = new GradeRes()
                      {
                          GradeId = grade.Id,
                          GradeName = grade.GradeName,
                      };


                      gradeResList.Add(gradeRes);
                  }

                  return gradeResList.Ok();
              }
              catch (Exception e)
              {
                  Console.WriteLine(e);
                  throw new Exception("An error occurred.", e);
              }
          }*/

        public override async Task<IActionResult> GetAll(Pagination pagination)
        {
            try
            {
                var gradeStudents = await UnitOfWork.Context.Grades
                    .Include(x=> x.Organization).Include(x=> x.Year)
                    .Include(g => g.Students)
                    .Include(g => g.Subjects)
                    .ToListAsync();

                List<GradeRes> gradeResList = new List<GradeRes>();

                foreach (var grade in gradeStudents)
                {
                    GradeRes gradeRes = new GradeRes()
                    {
                        GradeId = grade.Id,
                        GradeName = grade.GradeName,
                      
                        OrganizationRes = new OrganizationRes()
                        {
                            StartTime = grade.Organization.StartTime,
                            EndTime = grade.Organization.EndTime,
                            Id = grade.Organization.Id,
                            Location = grade.Organization.Location,
                            TotalStaff = grade.Organization.TotalStaff,
                            TotalStudent = grade.Organization.TotalStudent,
                            Organization = grade.Organization.Organization,
                            Students = grade.Organization.Students.Select(s => new StudentRes
                            {
                                GradeId = s.Id,
                                OrgId = s.OrgId,
                                RoomId = s.RoomId,
                                TeacherId = s.teacherId,
                            }).ToList(),
                        },
                        Student = grade.Students.Select(s => new StudentRes
                        {
                            Id = s.Id,
                            Name = s.Name,
                            GradeId = s.Id,
                            Grade = s.CurrentGrade,
                            OrgId = s.OrgId,
                            RoomId = s.RoomId,
                            TeacherId = s.teacherId,
                        }).ToList(),
                        Subjects = grade.Subjects.Select(sub => new SubjectRes
                        {
                            Id= sub.Id,
                            SubjectName = sub.SubjectName,
                            GradeId = sub.GradeId,
                        }).ToList()
                    };

                    gradeResList.Add(gradeRes);
                }

                return gradeResList.Ok();
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
                   var grade = await UnitOfWork.Context.Grades
                               .Include(g => g.Students)
                               .ThenInclude(s => s.SubjectStudents)
                               .FirstOrDefaultAsync(g => g.Id == id);


                   if (grade != null)
                   {
                       var gradeRes = new GradeRes
                       {
                           GradeId = grade.Id,
                           GradeName = grade.GradeName,
                       };

                       return gradeRes.Ok();
                   }
                   else
                   {
                       throw new Exception("Grade not found "); 
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
            try
            {
                var grade = await UnitOfWork.Context.Grades
                    .Include(g => g.Students)
                    .Include(g => g.Subjects)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (grade == null)
                {
                    throw new Exception(); 
                }

                if (grade != null)
                {
                    GradeRes gradeRes = new GradeRes()
                    {
                        GradeId = grade.Id,
                        GradeName = grade.GradeName,
                        Student = grade.Students.Select(s => new StudentRes
                        {
                            Id = s.Id,
                            Name = s.Name,
                            
                        }).ToList(),
                        Subjects = grade.Subjects.Select(sub => new SubjectRes
                        {
                            Id= sub.Id,
                            SubjectName = sub.SubjectName
                        }).ToList()
                    };

                    return gradeRes.Ok();
                }
                else
                {
                    throw new Exception("Grade not found ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }


        public override Task<IActionResult> Update(GradeReq reqModel)
        {
            return base.Update(reqModel);
        }
    }
}
