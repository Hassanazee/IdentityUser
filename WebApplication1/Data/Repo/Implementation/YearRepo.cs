using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repo.Interface;
using WebApplication1.Entity;

namespace WebApplication1.Data.Repo.Implementation
{
    public class YearRepo : BaseRepo<Year> , IYear
    {
        public YearRepo(DbContext context) : base(context)
        {
            
        }

        public override async Task<Year> Add(Year model)
        {
            await DbSet.AddAsync(model);
            return model;
        }

        public override async Task<bool> Delete(Guid id)
        {
            var found = await DbSet.FirstOrDefaultAsync(c => c.Id == id && c.IsDelete != true);
            if (found == null)
            {
                throw new($"{nameof(Year)} not found for Id:{id}");
            }

            found.IsDelete = true;
            await DbContext.SaveChangesAsync();
            return true;
        }

        public override async Task<Year> Get(Guid id)
        {
            var data = await DbSet.FirstOrDefaultAsync(c => !c.IsDelete && c.Id == id);
            return data;
        }

        public override async Task<(Pagination, IList<Year>)> GetAll(Pagination pagination, Func<IQueryable<Year>, IQueryable<Year>> includeFunc)
        {
            {
                var total = 0;
                var totalPages = 0;

                var res = await DbSet.Where(f => f.IsDelete != true)
                    .Paginate(pagination.PageIndex, pagination.PageSize, ref total, ref totalPages).ToListAsync();

                pagination = pagination.Combine(total, totalPages);
                return (pagination, res);
            }
        }


        public override Task<Year> Update(Year model, Func<Year, Year>? func = null)
        {
            return base.Update(model, func);
        }
    }
}
