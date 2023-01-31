using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RollCageBusiness.PlanGoodsIssue;
using RollCageBusiness.PlanGoodIssue;
using RollCageBusiness.Reports;
using planGoodsIssueBusiness.GoodsReceive;
using PTTPL.OMS.Business.Documents;
using PTTPL.TMS.Business.Common;
using PTTPL.TMS.Business.ViewModels;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static RollCageBusiness.PlanGoodsIssue.PopupGIViewModel;
using RollCageBusiness.RollCage;
using RollCageBusiness.CheckTagoutWithRollcage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RollCageAPI.Controllers
{
    //[Authorize]
    [Route("api/CheckRollcage")]
    public class CheckRollcageController : Controller
    {
        //private RollCageDbContext context;

        private readonly IHostingEnvironment _hostingEnvironment;
        public CheckRollcageController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        #region Check Rollcage Staging
        [HttpPost("Check_Rollcage_Staging")]
        public IActionResult Check_Rollcage_Staging([FromBody]JObject body)
        {
            try
            {
                var service = new CheckRollcageService();
                var result = service.CheckRollcage_TEMP();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

    }
}
