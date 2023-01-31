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
    [Route("api/RollCage")]
    public class RollCageController : Controller
    {
        //private RollCageDbContext context;

        private readonly IHostingEnvironment _hostingEnvironment;
        public RollCageController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #region autoCall
        //[AllowAnonymous]
        [HttpPost("getDate")]
        public IActionResult getDate()
        {
            try
            {
                return Ok(DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoCall
        //[AllowAnonymous]
        [HttpPost("autoCall")]
        public IActionResult autoCall()
        {
            try
            {
                var service = new RollCageService();
                //var Models = new SearchDetailModel();
                //Models = JsonConvert.DeserializeObject<SearchDetailModel>(body.ToString());
                var result = service.autoCall();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region updateActiveRollCage
        //[AllowAnonymous]
        [HttpPost("updateActiveRollCage")]
        public IActionResult updateActiveRollCage([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new RollCageViewModel();
                Models = JsonConvert.DeserializeObject<RollCageViewModel>(body.ToString());
                var result = service.updateActiveRollCage(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findRollCage
        //[AllowAnonymous]
        [HttpPost("findRollCage")]
        public IActionResult findRollCage([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new RollCageViewModel();
                Models = JsonConvert.DeserializeObject<RollCageViewModel>(body.ToString());
                var result = service.findRollCage(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findRollCageBUF
        //[AllowAnonymous]
        [HttpPost("findRollCageBUF")]
        public IActionResult findRollCageBUF([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new RollCageViewModel();
                Models = JsonConvert.DeserializeObject<RollCageViewModel>(body.ToString());
                var result = service.findRollCageBUF(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findRollCageScanIn
        //[AllowAnonymous]
        [HttpPost("findRollCageScanIn")]
        public IActionResult findRollCageScanIn([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new RollCageViewModel();
                Models = JsonConvert.DeserializeObject<RollCageViewModel>(body.ToString());
                var result = service.findRollCageScanIn(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findRollCageActiveEmptyChute
        //[AllowAnonymous]
        [HttpPost("findRollCageActiveEmptyChute")]
        public IActionResult findRollCageActiveEmptyChute([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new RollCageViewModel();
                Models = JsonConvert.DeserializeObject<RollCageViewModel>(body.ToString());
                var result = service.findRollCageActiveEmptyChute(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findRollCageActiveReworkChute
        //[AllowAnonymous]
        [HttpPost("findRollCageActiveReworkChute")]
        public IActionResult findRollCageActiveReworkChute([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new RollCageViewModel();
                Models = JsonConvert.DeserializeObject<RollCageViewModel>(body.ToString());
                var result = service.findRollCageActiveReworkChute(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findQRCodeGoodsIssueTruckload
        //[AllowAnonymous]
        [HttpPost("findQRCodeGoodsIssueTruckload")]
        public IActionResult findQRCodeGoodsIssueTruckload([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.findQRCodeGoodsIssueTruckload(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region scanQRCodeOrder
        //[AllowAnonymous]
        [HttpPost("scanQRCodeOrder")]
        public IActionResult scanQRCodeOrder([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.scanQRCodeOrder(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findQRCodeGoodsIssueTruckloadScanOut
        //[AllowAnonymous]
        [HttpPost("findQRCodeGoodsIssueTruckloadScanOut")]
        public IActionResult findQRCodeGoodsIssueTruckloadOut([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.findQRCodeGoodsIssueTruckloadScanOut(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        //#region insertOrderAndUpdateTagOut
        ////[AllowAnonymous]
        //[HttpPost("insertOrderAndUpdateTagOut")]
        //public IActionResult insertOrderAndUpdateTagOut([FromBody]JObject body)
        //{
        //    try
        //    {
        //        var service = new RollCageService();
        //        var Models = new LocationRollCageViewModel();
        //        Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
        //        var result = service.updateTagOut(Models,0);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
        //#endregion

        #region findScanSummary
        //[AllowAnonymous]
        [HttpPost("findScanSummary")]
        public IActionResult findScanSummary([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.findScanSummary(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findScanSummary
        //[AllowAnonymous]
        [HttpPost("findQRScanSummary")]
        public IActionResult findQRScanSummary([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.findQRScanSummary(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findCheckQRScanSummary
        //[AllowAnonymous]
        [HttpPost("findCheckQRScanSummary")]
        public IActionResult findCheckQRScanSummary([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.findCheckQRScanSummary(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findQRCodeDataTruckload
        //[AllowAnonymous]
        [HttpPost("findQRCodeDataTruckload")]
        public IActionResult findQRCodeDataTruckload([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.findQRCodeDataTruckload(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findFullChute
        //[AllowAnonymous]
        [HttpPost("findFullChute")]
        public IActionResult findFullChute([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.findFullChute(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region findSuggestionStagingArea
        //[AllowAnonymous]
        [HttpPost("findSuggestionStagingArea")]
        public IActionResult findSuggestionStagingArea([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.findSuggestionStagingArea(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region updateLocationRollCage
        //[AllowAnonymous]
        [HttpPost("updateLocationRollCage")]
        public IActionResult updateLocationRollCage([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.updateLocationRollCage(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region sendRollCageToStaging
        //[AllowAnonymous]
        [HttpPost("sendRollCageToStaging")]
        public IActionResult sendRollCageToStaging([FromBody]JObject body)
        {
            try
            {
                var service = new RollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.sendRollCageToStaging(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region CheckTagout
        [HttpPost("CheckTagout")]
        public IActionResult CheckTagout([FromBody]JObject body)
        {
            try
            {
                var service = new CheckTagoutWithRollcageService();
                var Models = new SearchCheckTagoutWithRollcage();
                Models = JsonConvert.DeserializeObject<SearchCheckTagoutWithRollcage>(body.ToString());
                var result = service.CheckTagout(Models);
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
