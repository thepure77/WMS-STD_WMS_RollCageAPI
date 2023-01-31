using System;
using DataAccess;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RollCageBusiness.PlanGoodIssue;

namespace RollCageAPI.Controllers
{
    [Route("api/FilterTable")]
    [ApiController]
    public class FilterTableController : ControllerBase
    {
        private RollCageDbContext context;



        #region im_PlanGoodsIssue
        [HttpPost("im_PlanGoodsIssue")]
        public IActionResult im_PlanGoodsReceive([FromBody]JObject body)
        {
            try
            {
                var service = new PlanGoodIssueService();
                var Models = new DocumentViewModel();
                Models = JsonConvert.DeserializeObject<DocumentViewModel>(body.ToString());
                var result = service.im_PlanGoodsIssue(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region im_PlanGoodsIssue
        [HttpPost("im_PlanGoodsIssueItem")]
        public IActionResult im_PlanGoodsReceiveItem([FromBody]JObject body)
        {
            try
            {
                var service = new PlanGoodsIssueItemService();
                var Models = new DocumentViewModel();
                Models = JsonConvert.DeserializeObject<DocumentViewModel>(body.ToString());
                var result = service.im_PlanGoodsIssueItem(Models);
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