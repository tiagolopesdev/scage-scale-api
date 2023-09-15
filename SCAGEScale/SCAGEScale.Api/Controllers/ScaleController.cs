using Microsoft.AspNetCore.Mvc;
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.ServiceSide;
using SCAGEScale.Application.Utils;
using System.Net;

namespace SCAGEScale.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScaleController : ControllerBase
    {
        private readonly IScaleService _scaleService;
        public ScaleController(IScaleService scaleService)  
        {
            _scaleService = scaleService;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RequestResponse>> GetScaleById(Guid id)
        {
            try
            {
                var response = await _scaleService.GetScaleById(id);

                return response != null ?
                    Ok(RequestResponse.New("Sucesso ao obter escala!", response)) :
                    BadRequest(RequestResponse.Error("Não foi possível obter a escala"));
            }
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }

        [HttpGet("singleScales")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RequestResponse>> GetAllScalesSingle([FromQuery] string? filter)
        {
            try
            {
                var response = filter == null ? 
                    await _scaleService.GetAllSingleScales() :
                    await _scaleService.GetAllSingleByFilterScales(filter);

                return response != null ?
                    Ok(RequestResponse.New("Sucesso ao obter as escalas!", response)) :
                    BadRequest(RequestResponse.Error("Não foi possível obter as escalas"));
            }
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }

        [HttpPut("updateScale")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RequestResponse>> UpdateScale([FromBody] UpdateScaleDto updateScaleDto)
        {
            try
            {
                var response = await _scaleService.UpdateScale(updateScaleDto);

                return Ok(RequestResponse.New("Escala atualizada com sucesso!", response));
            }
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }

        [HttpPost("createScale")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RequestResponse>> CreateScale([FromBody] CreateScaleDto createScaleDto)
        {
            try
            {
                var response = await _scaleService.CreateScale(createScaleDto);

                return Ok(RequestResponse.New("Escala criada com sucesso!", response));
            }
            catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }

        [HttpPost("generationScale")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RequestResponse>> PreviewScale([FromBody] PreviewDto previewDto)
        {
            try
            {
                var response = await _scaleService.PreviewScale(previewDto);

                return Ok(RequestResponse.New("Pré-visualização da escala gerada", response));

            }catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }
    }
}
