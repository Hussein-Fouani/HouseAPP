using System.Net;
using AutoMapper;
using HousesApp.Models;
using HousesApp.Models.Dto;
using HousesApp.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HousesApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HouseController : ControllerBase
{
    private readonly IHouseRepository _houseRepository;
    private readonly IMapper _mapper;


    public HouseController(IHouseRepository houseRepository, IMapper mapper)
    {
        _houseRepository = houseRepository;
        _mapper = mapper;
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<HouseDto>> GetHouseById(Guid id)
    {
        var house = await _houseRepository.GetHouseByIdAsync(i => i.Id == id);
        if (house == null)
            return NotFound("HOUSE NOT FOUND");
        return Ok(_mapper.Map<HouseDto>(house));
    }
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<HouseDto>> CreateHouse([FromBody] HouseCreateDto house)
    {

        if (house == null)
            return BadRequest("NO HOUSE PROVIDED");
        var newhouse = _mapper.Map<HouseModel>(house);
        await _houseRepository.CreateHouseAsync(newhouse);

        return CreatedAtRoute("GetHouseById", new { Id = newhouse.Id }, house);
    }
    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteHouseById(Guid id)
    {
        var house = await _houseRepository.GetHouseByIdAsync(i => i.Id == id);
        if (house == null)
            return NotFound("HOUSE NOT FOUND");
      await  _houseRepository.DeleteHouseAsync(house);
        return NoContent();
    }
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateHouse(Guid id, [FromBody] HouseDto house)
    {
        if (id == Guid.Empty)
            return NotFound("NO ID PROVIDED");
        return NoContent();
    }
    [HttpPatch("{Id:Guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<UpdateHouseDto>> PartialUpdate(Guid Id, [FromBody] JsonPatchDocument<UpdateHouseDto> document)
    {
        var upadtedHouse = await _houseRepository.GetHouseByIdAsync(i => i.Id == Id);
        if (upadtedHouse == null)
            return NotFound("HOUSE NOT FOUND");
        var houseToPatch = _mapper.Map<UpdateHouseDto>(upadtedHouse);
        document.ApplyTo(houseToPatch, ModelState);
        if (!TryValidateModel(houseToPatch))
            return ValidationProblem(ModelState);
        var newhouse = _mapper.Map(houseToPatch, upadtedHouse);
        await _houseRepository.UpdateHouseAsync(Id, newhouse);
        var result = _mapper.Map<UpdateHouseDto>(newhouse);
        return Ok(result);
    }
}