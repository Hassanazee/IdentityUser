using Azure;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public static class Helpers
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int page, int perPage, ref int total,
       ref int pages)
        {
            if (pages < 0) throw new ArgumentOutOfRangeException(nameof(pages));

            total = source.Count();
            pages = (int)Math.Ceiling(total / (double)perPage);
            return source.Skip((page - 1) * perPage).Take(perPage);
        }
        public static Pagination Combine(this Pagination pagination, int total, int totalPages)
        {
            pagination.Total = total < 0 ? 0 : total;
            pagination.TotalPages = totalPages < 0 ? 0 : totalPages;
            return pagination;
        }
        public static OkObjectResult Ok<T>(this T obj, string message = "Executed Successfully!", object miscD = null)
        {
            return new OkObjectResult(Response<T>.Ok(message, obj, miscD));
        }

        public static string GetKametiOTP()
        {
            Random rand = new Random();
            string str = "";
            for (int i = 0; i < 6; i++)
            {
                str += Convert.ToChar(rand.Next(0, 26) + 65);
            }
            return str;
        }



    }
}
