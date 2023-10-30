
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.QuerySide;
using SCAGEScale.Application.RepositorySide;
using SCAGEScale.Application.ServiceSide;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.Service
{
    public class ScaleService : IScaleService
    {
        private readonly IScaleRepository _scaleRepository;
        private readonly IScaleQuery _scaleQuery;

        public ScaleService(IScaleQuery scaleQuery, IScaleRepository scaleRepository)
        {
            _scaleRepository = scaleRepository;
            _scaleQuery = scaleQuery;
        }

        public async Task<ScaleDto> GetScaleById(Guid id)
        {
            return await _scaleQuery.GetScaleById(id);
        }

        public async Task<Guid> CreateScale(CreateScaleDto createScaleDto)
        {
            try
            {
                var scaleExists = await _scaleQuery.GetAllSingleByFilterScales(createScaleDto.Name);

                if (scaleExists != null && scaleExists.Count > 0) throw new Exception($"Escala referente ao mês de {createScaleDto.Name}, já existe.");

                var month = PropertiesCreateScale.PropertiesToCreateMonth(createScaleDto);

                var monthId = (Guid)month.MonthId;

                var scale = PropertiesCreateScale.PropertiesToCreateDay(createScaleDto.Days, monthId);

                scale.Insert(0, month);

                return await _scaleRepository.TransitionsScale(scale, monthId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ScaleMonthDto>> PreviewScale(PreviewDto previewDto)
        {
            try
            {
                var scaleExists = await _scaleQuery.GetAllSingleByFilterScales(previewDto.Name);

                if (scaleExists != null && scaleExists.Count > 0) throw new Exception($"Escala referente ao mês de {previewDto.Name}, já existe.");

                var scaleDay = new ScaleDay();
                var scaleMonth = new List<ScaleDay>();
                bool newRange = true;
                var indexs = new List<int>();

                while (scaleMonth.Count < previewDto.Days.Count)
                {
                    while (scaleDay.CameraOne == Guid.Empty || scaleDay.CameraTwo == Guid.Empty || scaleDay.CutDesk == Guid.Empty)
                    {
                        int indexPeople = RandomIndex(newRange, indexs, previewDto.Users.Count);

                        if (scaleDay.CameraOne == Guid.Empty)
                        {
                            scaleDay.CameraOne = previewDto.Users[indexPeople];
                            indexs.Add(indexPeople);
                        }
                        else if (scaleDay.CameraTwo == Guid.Empty)
                        {
                            scaleDay.CameraTwo = previewDto.Users[indexPeople];
                            indexs.Add(indexPeople);
                        }
                        else if (scaleDay.CutDesk == Guid.Empty)
                        {
                            scaleDay.CutDesk = previewDto.Users[indexPeople];
                            indexs.Add(indexPeople);
                        }
                        newRange = true;
                    }

                    scaleMonth.Add(scaleDay);

                    scaleDay = new ScaleDay();

                    indexs.Clear();
                }
                return await _scaleQuery.ScaleMonthMakedList(scaleMonth);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static int RandomIndex(bool newRange, List<int> indexs, int sizeUserList)
        {
            int indexPeople = 0;
            while (newRange)
            {
                indexPeople = new Random().Next(0, sizeUserList);

                var existIndex = indexs.Contains(indexPeople);

                newRange = existIndex;
            }
            return indexPeople;
        }

        public async Task<List<ScaleDto>?> GetAllSingleScales()
        {
            return await _scaleQuery.GetAllSingleScales();
        }
        public async Task<List<ScaleDto>?> GetAllSingleByFilterScales(string filter)
        {
            return await _scaleQuery.GetAllSingleByFilterScales(filter);
        }

        public async Task<Guid> UpdateScale(UpdateScaleDto updateScaleDto)
        {
            var month = PropertiesCreateScale.PropertiesToUpdateMonth(updateScaleDto);

            var scale = PropertiesCreateScale.PropertiesToUpdateDay(updateScaleDto.Days, updateScaleDto.Id);

            scale.Insert(0, month);

            return await _scaleRepository.TransitionsScale(scale, updateScaleDto.Id);
        }
    }
}
