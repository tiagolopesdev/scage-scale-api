﻿using SCAGEScale.Application.DTO;

namespace SCAGEScale.Application.ServiceSide
{
    public interface IScaleService
    {
        public Task<Guid> CreateScale(CreateScaleDto createScaleDto);
        public Task<List<ScaleMonthDto>> PreviewScale(PreviewDto previewDto);
    }
}
