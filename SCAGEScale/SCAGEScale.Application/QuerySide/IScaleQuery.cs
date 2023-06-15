
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.QuerySide
{
    public interface IScaleQuery
    {
        public Task<ReferencyUser> GetUserByReferency(Guid userId);
        public Task<List<ScaleMonth>> ScaleMonthMakedList(List<ScaleDay> scaleDays);
    }
}
