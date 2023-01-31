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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReworkRollCageAPI.Controllers
{
    //[Authorize]
    [Route("api/ReworkRollCage")]
    public class ReworkRollCageController : Controller
    {
        //private RollCageDbContext context;

        private readonly IHostingEnvironment _hostingEnvironment;
        public ReworkRollCageController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #region Scanshipment
        [HttpPost("Scanshipment")]
        public IActionResult Scanshipment([FromBody]JObject body)
        {
            try
            {
                var service = new ReworkRollCageService();
                var Models = new ReworkRollCageViewModel();
                Models = JsonConvert.DeserializeObject<ReworkRollCageViewModel>(body.ToString());
                var result = service.Scanshipment(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region CallRollCage
        [HttpPost("CallRollCage")]
        public IActionResult CallRollCage([FromBody]JObject body)
        {
            try
            {
                var service = new ReworkRollCageService();
                var Models = new ReworkRollCageViewModel();
                Models = JsonConvert.DeserializeObject<ReworkRollCageViewModel>(body.ToString());
                var result = service.CallRollCage(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region ScanLocation
        [HttpPost("ScanLocation")]
        public IActionResult ScanLocation([FromBody]JObject body)
        {
            try
            {
                var service = new ReworkRollCageService();
                var Models = new ReworkRollCageViewModel();
                Models = JsonConvert.DeserializeObject<ReworkRollCageViewModel>(body.ToString());
                var result = service.ScanLocation(Models);
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
