using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repo.Interface;
using WebApplication1.Entity;

namespace WebApplication1.Data.Repo.Implementation
{
    public class ContentRepo : BaseRepo<Content> , IContentRepo
    {
        public ContentRepo( DbContext context) : base (context)
        {
            
        }

        public override async Task<(Pagination, IList<Content>)> GetAll(
           Pagination pagination, Func<IQueryable<Content>, IQueryable<Content>> includeFunc = null)
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





        public override async Task<Content> Get(Guid id)
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
    }
}
