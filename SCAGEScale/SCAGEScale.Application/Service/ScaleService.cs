
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.QuerySide;
using SCAGEScale.Application.RepositorySide;
using SCAGEScale.Application.ServiceSide;
using SCAGEScale.Application.Utils;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.Service
{
    public class ScaleService : IScaleService
    {
        private IScaleRepository _scaleRepository { get; set; }
        private IScaleQuery _scaleQuery;

        public ScaleService(IScaleQuery scaleQuery, IScaleRepository scaleRepository)
        {
            _scaleRepository = scaleRepository;
            _scaleQuery = scaleQuery;
        }

        public async Task<List<ScaleDto>> GetAllScales()
        {
            var responseQuery = await _scaleQuery.GetAllScales();
            throw new NotImplementedException();
        }
        public async Task<Guid> CreateScale(CreateScaleDto createScaleDto)
        {
            var month = PropertiesCreateScale.PropertiesToCreateMonth(createScaleDto);

            var scale = new List<PropertiesCreateScale>();

            var monthId = (Guid)month.MonthId;
            
            scale = PropertiesCreateScale.PropertiesToCreateDay(createScaleDto.Days, monthId);

            scale.Insert(0, month);

            return await _scaleRepository.CreateScale(scale, monthId);
        }
        public async Task<List<ScaleMonthDto>> PreviewScale(PreviewDto previewDto)
        {
            var scaleDay = new ScaleDay();
            var scaleMonth = new List<ScaleDay>();
            bool newRange = true;
            var indexs = new List<int>();

            while (scaleMonth.Count < previewDto.days.Count)
            {
                while (scaleDay.CameraOne == Guid.Empty || scaleDay.CameraTwo == Guid.Empty || scaleDay.CutDesk == Guid.Empty)
                {
                    int indexPeople = RandomIndex(newRange, indexs, previewDto.users.Count);

                    if (scaleDay.CameraOne == Guid.Empty)
                    {
                        scaleDay.CameraOne = previewDto.users[indexPeople];
                        indexs.Add(indexPeople);
                    }
                    else if (scaleDay.CameraTwo == Guid.Empty)
                    {
                        scaleDay.CameraTwo = previewDto.users[indexPeople];
                        indexs.Add(indexPeople);
                    }
                    else if (scaleDay.CutDesk == Guid.Empty)
                    {
                        scaleDay.CutDesk = previewDto.users[indexPeople];
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

    }
}
