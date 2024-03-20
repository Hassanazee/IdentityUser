using Microsoft.EntityFrameworkCore;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Data.Repo.Interface;
using WebApplication1.Entity;

namespace WebApplication1.Data.Repo.Implementation
{
    public class GradeRepo : BaseRepo<Grade> , IGradeRepo
    {
        
        public GradeRepo(DbContext context ) : base(context)
        {
        }

        public override async Task<Grade> Add(Grade model)
        {
            await DbSet.AddAsync(model);
            return model;
        }

        public override async Task<bool> Delete(Guid id)
        {
            var found = await DbSet.FirstOrDefaultAsync(c => c.Id == id && c.IsDelete != true);
            if (found == null)
            {
                throw new Exception($"Grade not found for Id: {id}");
            }

            found.IsDelete = true;
            await DbContext.SaveChangesAsync();
            return true;
        }

    
        public override async Task<(Pagination, IList<Grade>)> GetAll(
            Pagination pagination, Func<IQueryable<Grade>, IQueryable<Grade>> includeFunc = null)
        {
            var total = 0;
            var totalPages = 0;

            var query = DbSet.Where(f => f.IsDelete != true);
            if (includeFunc != null)
            {
                query = includeFunc(query);
            }

            var res = await query.Paginate(pagination.PageIndex, pagination.PageSize, ref total, ref totalPages).ToListAsync();

            pagination = pagination.Combine(total, totalPages);
            return (pagination, res);
        }

      


        public override Task<Grade> Update(Grade model, Func<Grade, Grade> func = null)
        {
            return base.Update(model, func);
        }

        public override async Task<Grade> Get(Guid id)
        {
            try
            {
                var data = await DbSet.FirstOrDefaultAsync(c => !c.IsDelete && c.Id == id);
                if (data != null)
                {
                    return data;
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
        public async Task<IList<GradeRes>> GetAllGrades(Pagination pagination)
        {
            try
            {
                var grades = await DbSet
                    .Where(g => !g.IsDelete)
                    .Skip((pagination.PageIndex - 1) * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .ToListAsync();

                var gradeResList = grades.Select(grade => new GradeRes
                {
                    GradeId = grade.Id,
                    GradeName = grade.GradeName,
                    Student = grade.Students.Select(s => new StudentRes
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).ToList(),
                    Subjects = grade.Subjects.Select(s=> new SubjectRes
                    {
                        SubjectName = s.SubjectName,
                    }).ToList(),    
                });

                return (IList<GradeRes>)gradeResList;
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred while fetching grades.", e);
            }
        }
    }
}
