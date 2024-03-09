using System.Net;
using HousesApp.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HousesApp.Controllers;
[ApiController]
public class HouseApiController : ControllerBase
{
    // GET
    [Route("api/[controller]")]
    [HttpGet]
    public ActionResult<IEnumerable<HouseDto>> GetHouses()
    {
        return Ok();
    }
    
    [Route("api/[controller]/{id}")]
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public ActionResult<HouseDto> GetHouseById(Guid id)
    {
        if(id == Guid.Empty)
            return BadRequest("NO ID PROVIDED");
        return Ok();
    }
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public ActionResult<HouseDto> CreateHouse([FromBody]HouseDto house)
    {
        
        if(house == null)
            return BadRequest("NO HOUSE PROVIDED");
        if (house.Id != Guid.Empty)
            return StatusCode(StatusCodes.Status500InternalServerError);

        
        return CreatedAtRoute("GetHouseById", new {id = house.Id}, house);
    }
   [HttpDelete]
   [ProducesResponseType(204)]
   [ProducesResponseType(400)]
   [ProducesResponseType(404)]
   public IActionResult DeleteHouseById(Guid id)
   {
       if(id == Guid.Empty)
           return NotFound("NO ID PROVIDED");
       return NoContent();
   }
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateHouse(Guid id, [FromBody] HouseDto house)
    {
        if(id == Guid.Empty)
            return NotFound("NO ID PROVIDED");
        return NoContent();
    }
    [HttpPatch("{Id:Guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult PartialUpdate(Guid Id, [FromBody] JsonPatchDocument<HouseDto> document)
    {
        if(Id == Guid.Empty)
            return NotFound("NO ID PROVIDED");
        return NoContent();
    }
}