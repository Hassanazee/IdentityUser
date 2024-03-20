using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;

namespace WebApplication1.Controller
{
    public class OrganizationController : BaseController<OrganizationController, IOrganizationSchService, OrganizationReq, OrganizationRes>
    {
        public OrganizationController(ILogger<OrganizationController> logger, IOrganizationSchService organizationService) : base(logger, organizationService)
        {

        }
    }
}
