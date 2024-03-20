﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;

namespace WebApplication1.Controller
{
    public class TimeTableEntryController : BaseController<TimeTableEntryController, ITimeTableEntryService, TimeTableEntryReq, TimeTableEntryRes>
    {
        public TimeTableEntryController(ILogger<TimeTableEntryController> logger, ITimeTableEntryService timeTableEntryService) : base(logger, timeTableEntryService)
        {

        }
    }



}
