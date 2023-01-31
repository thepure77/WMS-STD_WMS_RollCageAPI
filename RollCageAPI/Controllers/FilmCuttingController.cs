using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using RollCageBusiness.RollCage;
using RollCageBusiness.FilmCutting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RollCageAPI.Controllers
{
    //[Authorize]
    [Route("api/FilmCutting")]
    public class FilmCuttingController : Controller
    {
        //private RollCageDbContext context;

        private readonly IHostingEnvironment _hostingEnvironment;
        public FilmCuttingController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #region findtag
        [HttpPost("findtag")]
        public IActionResult findtag([FromBody]JObject body)
        {
            try
            {
                FilmCuttingService service = new FilmCuttingService();
                FilmCuttingViewModel Models = JsonConvert.DeserializeObject<FilmCuttingViewModel>(body.ToString());
                var result = service.findtag(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region cuttingPallet
        [HttpPost("cuttingPallet")]
        public IActionResult cuttingPallet([FromBody]JObject body)
        {
            try
            {
                FilmCuttingService service = new FilmCuttingService();
                FilmCuttingViewModel Models = JsonConvert.DeserializeObject<FilmCuttingViewModel>(body.ToString());
                var result = service.Cutting(Models);
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
