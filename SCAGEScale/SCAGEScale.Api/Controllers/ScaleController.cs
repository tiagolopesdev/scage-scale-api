using Microsoft.AspNetCore.Mvc;
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.ServiceSide;
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
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseDto>> GetScaleById(Guid id)
        {
            try
            {
                var response = await _scaleService.GetScaleById(id);

                return response != null ?
                    Ok(ResponseDto.New("Sucesso ao obter escala!", response)) :
                    BadRequest(ResponseDto.Error("Não foi possível obter a escala"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDto.Error(ex.Message));
            }
        }

        [HttpGet("singleScales")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseDto>> GetAllScalesSingle([FromQuery] string? filter)
        {
            try
            {
                var response = filter == null ? 
                    await _scaleService.GetAllSingleScales() :
                    await _scaleService.GetAllSingleByFilterScales(filter);

                return response != null ?
                    Ok(ResponseDto.New("Sucesso ao obter as escalas!", response)) :
                    BadRequest(ResponseDto.Error("Não foi possível obter as escalas"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDto.Error(ex.Message));
            }
        }

        [HttpPut("updateScale")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseDto>> UpdateScale([FromBody] UpdateScaleDto updateScaleDto)
        {
            try
            {
                var response = await _scaleService.UpdateScale(updateScaleDto);

                return Ok(ResponseDto.New("Escala atualizada com sucesso!", response));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDto.Error(ex.Message));
            }
        }

        [HttpPost("createScale")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseDto>> CreateScale([FromBody] CreateScaleDto createScaleDto)
        {
            try
            {
                var response = await _scaleService.CreateScale(createScaleDto);

                return Ok(ResponseDto.New("Escala criada com sucesso!", response));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseDto.Error(ex.Message));
            }
        }

        [HttpPost("generationScale")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseDto>> PreviewScale([FromBody] PreviewDto previewDto)
        {
            try
            {
                var response = await _scaleService.PreviewScale(previewDto);

                return Ok(ResponseDto.New("Pré-visualização da escala gerada", response));

            }catch (Exception ex)
            {
                return BadRequest(ResponseDto.Error(ex.Message));
            }
        }
    }
}
