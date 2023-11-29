
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.QuerySide
{
    public interface IScaleQuery
    {
        public Task<List<ScaleDto>?> GetAllSingleByFilterScales(string filter);
        public Task<List<ScaleDto>?> GetAllSingleScales();
        public Task<ScaleDto> GetScaleById(Guid id);
        public Task<ReferencyUser> GetUserByReferency(Guid userId);
        public Task<List<ScaleMonthDto>> ScaleMonthMakedList(List<ScaleDay> scaleDays);
    }
}
