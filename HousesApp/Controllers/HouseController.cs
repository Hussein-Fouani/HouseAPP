using System.Net;
using AutoMapper;
using HousesApp.Models;
using HousesApp.Models.Dto;
using HousesApp.Repository.IRepository;
using HousesApp.Response;
using Microsoft.AspNetCore.Mvc;

namespace HousesApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HouseController : ControllerBase
{
    private readonly IHouseRepository _houseRepository;
    private readonly IMapper _mapper;
    protected ApiResponse apiResponse;


    public HouseController(IHouseRepository houseRepository, IMapper mapper)
    {
        _houseRepository = houseRepository;
        _mapper = mapper;
        this.apiResponse = new ApiResponse();
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ApiResponse>> GetHouseById(Guid id)
    {
        try
        {
            var house = await _houseRepository.GetHouseByIdAsync(i => i.Id == id);
            if (house == null)
            {
                apiResponse.isSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                apiResponse.ErrorMessages.Add("HOUSE NOT FOUND");
                return NotFound(apiResponse);
            }
            apiResponse.isSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = _mapper.Map<HouseDto>(house);
            return Ok(apiResponse);
        }
        catch (Exception ex)
        {

            apiResponse.ErrorMessages.Add(ex.Message);
            apiResponse.isSuccess = false;
            apiResponse.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(apiResponse);
        }
    }

    [HttpGet("all")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ApiResponse>> GetHouses()
    {
        try
        {
            var houses = await _houseRepository.GetHousesAsync();
            if (houses == null)
            {
                apiResponse.isSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                apiResponse.ErrorMessages.Add("HOUSES NOT FOUND");
                return NotFound(apiResponse);
            }
            apiResponse.isSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = _mapper.Map<HouseDto>(houses);
            return Ok(apiResponse);
        }
        catch (Exception ex)
        {

            apiResponse.ErrorMessages.Add(ex.Message);
            apiResponse.isSuccess = false;
            apiResponse.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(apiResponse);
        }
    }



    [HttpPost("create")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ApiResponse>> CreateHouse([FromBody] HouseCreateDto house)
    {

        try
        {
            if (house == null)
            {
                apiResponse.ErrorMessages.Add("NO HOUSE PROVIDED");
                apiResponse.isSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(apiResponse);
            }
            var Newhouse = _mapper.Map<HouseModel>(house);
            await _houseRepository.CreateHouseAsync(Newhouse);
            apiResponse.isSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = _mapper.Map<HouseDto>(Newhouse);
            return CreatedAtRoute("GetHouseById", new { Id = Newhouse.Id }, apiResponse);
        }
        catch (Exception ex)
        {

            apiResponse.ErrorMessages.Add(ex.Message);
            apiResponse.isSuccess = false;
            apiResponse.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(apiResponse); 
        }
    }
    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ApiResponse>> DeleteHouseById(Guid id)
    {
        try
        {
            var house = await _houseRepository.GetHouseByIdAsync(i => i.Id == id);
            if (house == null)
            {
                apiResponse.ErrorMessages.Add("NO HOUSE PROVIDED");
                apiResponse.isSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(apiResponse);
            }
            await _houseRepository.DeleteHouseAsync(house);
            apiResponse.isSuccess = true;
            apiResponse.StatusCode = HttpStatusCode.NoContent;
            return Ok(apiResponse);
        }
        catch (Exception ex )
        {

            apiResponse.ErrorMessages.Add(ex.Message);
            apiResponse.isSuccess = false;
            apiResponse.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(apiResponse);
        }
    }
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<UpdateHouseDto>> UpdateHouse(Guid id, [FromBody] UpdateHouseDto house)
    {
        try
        {
            if (house == null)
                return BadRequest("NO HOUSE PROVIDED");
            var updatedHouse = await _houseRepository.GetHouseByIdAsync(i => i.Id == id);
            if (updatedHouse == null)
                return NotFound("HOUSE NOT FOUND");
            var Newhouse = _mapper.Map(house, updatedHouse);
            await _houseRepository.UpdateHouseAsync(id, Newhouse);
            return NoContent();
        }
        catch (Exception ex)
        {

            apiResponse.ErrorMessages.Add(ex.Message);
            apiResponse.isSuccess = false;
            apiResponse.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(apiResponse);
        }
    }

   
}