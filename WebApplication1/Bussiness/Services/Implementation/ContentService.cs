using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;
using WebApplication1.Data.Repo.Implementation;
using WebApplication1.Data.Repo.Interface;
using WebApplication1.Data.UnitOfWork;
using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Services.Implementation
{
    public class ContentService : BaseService<ContentReq , ContentRes  , ContentRepo , Content> , IContentService
    {
        public ContentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }


        public async Task<IActionResult> Content(string Url)
        {
            try
            {
                var content = await UnitOfWork.Context.Contents
                    .Where(c => c.URL == Url)
                    .Include(c => c.Subject)
                    .ThenInclude(s => s.Grade)
                    .FirstOrDefaultAsync();

                string subjectName = content.Subject != null ? content.Subject.SubjectName : "";
                string gradeName = content.Subject?.Grade?.GradeName ?? "";

                var contentRes = new ContentRes()
                {
                    SubjectId = content.SubjectId,
                    Name = subjectName,
                    URL = content.URL,
                };
                
                return contentRes.Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("An error ocurred while retrieving content.", ex);
            }
        }





        public override async Task<IActionResult> GetAll(Pagination pagination)
        {
            return await base.GetAll(pagination);
        }

        public override async Task<IActionResult> Get(Guid id)
        {
            return await base.Get(id);
        }



    }
}
