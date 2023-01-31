
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RollCageBusiness.PlanGoodsIssue;
using System;
using RollCageBusiness.RollCage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RollCageAPI.Controllers
{
    [Route("api/CallFullRollCage")]
    public class CallFullRollCageController : Controller
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        public CallFullRollCageController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #region ScanLocation
        [HttpPost("ScanLocation")]
        public IActionResult ScanLocation([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.ScanLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region scanEmptyLocation
        [HttpPost("scanEmptyLocation")]
        public IActionResult scanStagingLocation([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.scanEmptyLocation(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region scanEmptyLocation_staging
        [HttpPost("scanEmptyLocation_staging")]
        public IActionResult scanEmptyLocation_staging([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.scanEmptyLocation_staging(Models);
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
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.CallRollCage(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region Scanshipment
        [HttpPost("Scanshipment")]
        public IActionResult Scanshipment([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.Scanshipment(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region Scantagout
        [HttpPost("Scantagout")]
        public IActionResult Scantagout([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.Scantagout(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region movestaging
        [HttpPost("movestaging")]
        public IActionResult movestaging([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.movestaging(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region checkRollcage
        [HttpPost("checkRollcage")]
        public IActionResult checkRollcage([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.checkRollcage(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        #region checkfromcallfullRollcage
        [HttpPost("checkfromcallfullRollcage")]
        public IActionResult checkfromcallfullRollcage([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.checkfromcallfullRollcage(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        #endregion

        #region movePallet
        [HttpPost("movePallet")]
        public IActionResult movePallet([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.movePallet(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        #region movePallettest
        [HttpPost("movePallettest")]
        public IActionResult movePallettest([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.movePallettest(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region updateActiveRollCageBuff
        [HttpPost("updateActiveRollCageBuff")]
        public IActionResult updateActiveRollCageBuff([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.updateActiveRollCageBuff(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region Check_unscan_out_Rollcage
        [HttpPost("Check_unscan_out_Rollcage")]
        public IActionResult Check_unscan_out_Rollcage([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new CallFullRollCageViewModel();
                Models = JsonConvert.DeserializeObject<CallFullRollCageViewModel>(body.ToString());
                var result = service.Check_unscan_out_Rollcage(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
        #endregion


        #region findSuggestionStagingAreainDock_staging
        [HttpPost("findSuggestionStagingAreainDock_staging")]
        public IActionResult findSuggestionStagingAreainDock_staging([FromBody]JObject body)
        {
            try
            {
                var service = new CallFullRollCageService();
                var Models = new LocationRollCageViewModel();
                Models = JsonConvert.DeserializeObject<LocationRollCageViewModel>(body.ToString());
                var result = service.findSuggestionStagingAreainDock_staging(Models);
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
