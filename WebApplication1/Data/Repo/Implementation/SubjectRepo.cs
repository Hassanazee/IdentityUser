using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repo.Interface;
using WebApplication1.Entity;

namespace WebApplication1.Data.Repo.Implementation
{
    public class SubjectRepo : BaseRepo<Subject>, ISubjectRepo
    {
        public SubjectRepo(DbContext dbContext) : base(dbContext)
        {
            
        }
        public override async Task<Subject> Add(Subject model)
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

        public override async Task<Subject> Get(Guid id)
        {
            var data = await DbSet.FirstOrDefaultAsync(c => !c.IsDelete && c.Id == id);
            return data;
        }

        public override async Task<(Pagination, IList<Subject>)> GetAll(Pagination pagination, Func<IQueryable<Subject>, IQueryable<Subject>> includeFunc = null)
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

        public override Task<Subject> Update(Subject model, Func<Subject, Subject> func = null)
        {
            return base.Update(model, func);
        }







    }
}
