
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.ServiceSide
{
    public interface IScaleService
    {
        public Task<List<ScaleMonthDto>> PreviewScale(PreviewDto previewDto);
    }
}
