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
    public class OrganizationSchService : BaseService<OrganizationReq, OrganizationRes, OrganizationSchRepo, OrganizationSch>, IOrganizationSchService
    {
        public OrganizationSchService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IActionResult> Add(OrganizationReq reqModel)
        {
            try
            {
                var trans = await UnitOfWork.Context.Database.BeginTransactionAsync();

                OrganizationSch organizationSch = new OrganizationSch()
                {
                    Id = Guid.NewGuid(),
                    Organization = reqModel.Organization,
                    Location = reqModel.Location,
                    TotalStaff = reqModel.TotalStaff,
                    TotalStudent = reqModel.TotalStudent,
                    StartTime = reqModel.StartTime,
                    EndTime = reqModel.EndTime,
                   /* Students = reqModel.Students.Select(s => new Student
                    {
                        Name = s.Name,
                    }).ToList(),
                    Subjects = reqModel.Subjects.Select(sub => new Subject
                    {
                        Id = sub.Id,
                        SubjectName = sub.SubjectName,
                       
                    }).ToList()*/
                };

                await Repository.Add(organizationSch);
                await UnitOfWork.SaveAsync();
                await UnitOfWork.CommitTransactionAsync(trans);

                return organizationSch.Ok();
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
                var organizations = await UnitOfWork.Context.organizations.ToListAsync();

                List<OrganizationRes> organizationResList = new List<OrganizationRes>();

                foreach (var organization in organizations)
                {
                    OrganizationRes organizationRes = new OrganizationRes()
                    {
                        Id = organization.Id,
                        Organization = organization.Organization,
                        Location = organization.Location,
                        TotalStaff = organization.TotalStaff,
                        TotalStudent = organization.TotalStudent,
                        StartTime = organization.StartTime,
                        EndTime = organization.EndTime,
                        Students = organization.Students.Select(s => new StudentRes
                        {
                            Id = s.Id,
                            Name = s.Name
                           
                        }).ToList(),
                        Subjects = organization.Subjects.Select(sub => new SubjectRes
                        {
                            Id = sub.Id,
                            SubjectName = sub.SubjectName
                           
                        }).ToList()
                    };

                    organizationResList.Add(organizationRes);
                }

                return organizationResList.Ok();
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
                var organization = await UnitOfWork.Context.organizations
                    .Include(o => o.Students)
                .Include(o => o.Subjects)
                .FirstOrDefaultAsync(x => x.Id == id);

                if (organization == null)
                {
                    throw new Exception("Organization not found");
                }

                OrganizationRes organizationRes = new OrganizationRes()
                {
                    Id = organization.Id,
                    Organization = organization.Organization,
                    Location = organization.Location,
                    TotalStaff = organization.TotalStaff,
                    TotalStudent = organization.TotalStudent,
                    StartTime = organization.StartTime,
                    EndTime = organization.EndTime,
                    Students = organization.Students.Select(s => new StudentRes
                    {
                        Id = s.Id,
                        Name = s.Name
                       
                    }).ToList(),
                    Subjects = organization.Subjects.Select(sub => new SubjectRes
                    {
                        Id = sub.Id,
                        SubjectName = sub.SubjectName
                       
                    }).ToList()
                };

                return organizationRes.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred.", e);
            }
        }

        public override Task<IActionResult> Update(OrganizationReq reqModel)
        {
            return base.Update(reqModel);
        }
    }

}
