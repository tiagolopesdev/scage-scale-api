using SCAGEScale.Application.DTO;

namespace SCAGEScale.Application.ServiceSide
{
    public interface IScaleService
    {
        public Task<Guid> CreateScale(CreateScaleDto createScaleDto);
        public List<EventsGeneratedDTO> GenerationDays(GenerationDaysDTO request);
        public Task<List<ScaleDto>?> GetAllSingleByFilterScales(string filter);
        public Task<List<ScaleDto>?> GetAllSingleScales();
        public Task<ScaleDto> GetScaleById(Guid id);
        public Task<List<ScaleMonthDto>> PreviewScale(PreviewDto previewDto);
        public Task<Guid> UpdateScale(UpdateScaleDto updateScaleDto);
    }
}
