﻿
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.QuerySide
{
    public interface IScaleQuery
    {
        public Task<List<ScaleDto>> GetAllScales();
        public Task<List<ScaleDto>?> GetAllSingleScales();
        public Task<ReferencyUser> GetUserByReferency(Guid userId);
        public Task<List<ScaleMonthDto>> ScaleMonthMakedList(List<ScaleDay> scaleDays);
    }
}
