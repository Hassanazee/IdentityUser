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

                Grade grade = new Grade()
                {
                    Id = new Guid(),
                    Section = reqModel.Section,
                    Students = reqModel.Student.Select(s => new Student
                    {
                        Name = s.Name,
                    }).ToList(),
                    Subjects = reqModel.Subjects.Select(s=> new Subject
                    {
                        Id = s.Id,
                        SubjectName = s.SubjectName,
                        
                    }).ToList(),
                };


              await Repository.Add( grade );
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
                        Student = grade.Students.Select(s => new StudentRes
                        {
                            Id = s.Id,
                            Name = s.Name
                        }).ToList(),
                        Subjects = grade.Subjects.Select(sub => new SubjectRes
                        {
                            Id= sub.Id,
                            SubjectName = sub.SubjectName
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
