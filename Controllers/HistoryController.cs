using HistoryMockApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistoryMockApi.Wrappers;
using HistoryMockApi.Filter;
using HistoryMockApi.Services;
using HistoryMockApi.Helpers;

namespace HistoryMockApi.Controllers
{
	[Route("api/[controller]")]
	public class HistoryController : ControllerBase
	{
        private readonly DataContext _db;
        private readonly IUriService _uriService;

        public HistoryController(DataContext context, IUriService uriService)
        {
            this._db = context;
            this._uriService = uriService;

        }

        //[HttpGet]
        //public async Task<IActionResult> GetValues() 
        //{
        //    var values = await _db.Users.ToListAsync();
        //    return Ok(new Response<List<Models.UserModel>>(values) );


        //}

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _db.Users
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _db.Users.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Models.UserModel>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
    }
}
