using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WaterMango.BAL;
using WaterMango.Helper;
using WaterMango.Model;

namespace WaterMango.Controllers
{
    [EnableCors()]
    [Route("api/[controller]")]
    public class PlantController : Controller
    {
        private readonly Settings _configs;
        public PlantController(Settings configs )
        { 
            _configs = configs;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("getall/")] 
        public async Task<ActionResult> getAll( )
        {
            try
            { 
                var plantSystem = new PlantSystem(_configs );
                List<PlantModel> result=await plantSystem.getAll();
                if (result != null && result.Count > 0)
                    return Ok(new JsonResult(result));
                else return Ok();
                //    return StatusCode((int)HttpStatusCode.NotFound,  "No data found" );
            }
            catch (Exception e1)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,  e1.Message);
            } 
        }
        [HttpGet("watering/{plantToken}/start/")]
        public async Task<ActionResult> setWatering(Guid plantToken)
        {
            try
            {
                var wateringHardware = new WateringHardwareSystem(); 
                    wateringHardware.startWatering();
                    return Ok(); 
            }
            catch (Exception e1)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,  e1 .Message);
            }

        }
        [HttpGet("watering/{plantToken}/stop/{duration}/")]
        public async Task<ActionResult> stopWatering( Guid plantToken,int duration)
        {
            try
            {
                var wateringHardware = new WateringHardwareSystem();
                wateringHardware.stopWatering();

                var plantSystem = new PlantSystem(_configs);

                var result = await plantSystem.addWateringRecored(plantToken, duration);
                return Ok(result);

            }
            catch (Exception e1)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e1.Message);
            }

        }
        [HttpGet("reset/database")] // to clear the Json file - just for testing purpuse  
        public ActionResult clearJsonData()
        {
            var plantSystem = new PlantSystem(_configs);
            plantSystem.restDatabase();
            return Ok();
        }

    }
}