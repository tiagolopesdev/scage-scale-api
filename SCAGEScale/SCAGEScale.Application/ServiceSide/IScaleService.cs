
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.ServiceSide
{
    public interface IScaleService
    {
        public Task<List<ScaleDay>> PreviewScale(PreviewDto previewDto);
    }
}
