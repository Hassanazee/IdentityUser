using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Bussiness.Services.Interface;

namespace WebApplication1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class BaseController<T, TService, TReq, TRes> : ControllerBase where TService : IBaseService<TReq, TRes>
    {
        private readonly ILogger<T> _logger;
        protected readonly TService Service;

        public BaseController(ILogger<T> logger, TService service)
        {
            _logger = logger;
            Service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var result = await Service.GetAll(pagination);
            return result;
        }

 
        [HttpGet("{id:Guid}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            return await Service.Get(id);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(TReq model)
        {
            try
            {
                var result = await Service.Add(model);
                if (result is IActionResult actionResult)
                {
                    return actionResult;
                }
                else
                {
                    return StatusCode(500, "Unexpected response from service."); 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing the request."); 
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put(TReq model)
        {
            return await Service.Update(model);
        }

        [HttpDelete("{id:Guid}")]
  
        public virtual async Task<bool> Delete(Guid id)
        {
            return true;
        }


        ~BaseController()
        {
            _logger.LogInformation("Instance Destroyed!");
        }

        public static string GetVersionedBase(string action)
        {
            return "api/v{version:apiVersion}/" + action;
        }
    }
}
