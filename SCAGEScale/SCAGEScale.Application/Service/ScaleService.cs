
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.QuerySide;
using SCAGEScale.Application.ServiceSide;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.Service
{
    public class ScaleService : IScaleService
    {
        private IScaleQuery _scaleQuery;

        public ScaleService(IScaleQuery scaleQuery)
        {
            _scaleQuery = scaleQuery;
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
