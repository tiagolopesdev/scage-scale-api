
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.ServiceSide;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.Service
{
    public class ScaleService : IScaleService
    {
        public async Task<List<ScaleDay>> PreviewScale(PreviewDto previewDto)
        {
            var scaleDay = new ScaleDay();
            var scaleMonth = new List<ScaleDay>();
            bool newRange = true;
            Random random = new();
            var indexs = new List<int>();

            while (scaleMonth.Count < previewDto.days.Count) 
            {
                while (scaleDay.CameraOne == Guid.Empty || scaleDay.CameraTwo == Guid.Empty || scaleDay.CutDesk == Guid.Empty)
                {
                    int indexPeople = 0;
                    while (newRange)
                    {
                        indexPeople = random.Next(0, previewDto.users.Count);

                        var existIndex = indexs.Contains(indexPeople);

                        if (!existIndex)
                        {
                            newRange = false;
                        }
                    }

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
            return scaleMonth;
        }
    }
}
