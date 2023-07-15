
using SCAGEScale.Application.AggregateRoot.ScaleSingleAggregate;
using SCAGEScale.Application.DTO;

namespace SCAGEScale.Application.Extensions
{
    public static class ScaleExtensions
    {
        public static ScaleDto ToDtoListByDinamic(this IEnumerable<dynamic> scale)
        {
            var dayGroup = scale.ToList().GroupBy(x => new
            {
                x.IdDay,
                x.NameDay,
                x.DateDay,
                x.Month,
                x.IdUserOne,
                x.NameUserOne,
                x.EmailUserOne,
                x.SexUserOne,
                x.IdUserTwo,
                x.NameUserTwo,
                x.EmailUserTwo,
                x.SexUserTwo,
                x.IdUserThree,
                x.NameUserThree,
                x.EmailUserThree,
                x.SexUserThree,
            });

            var scaleGroup = scale.ToList().GroupBy(x => new
            {
                x.Id,
                x.Name,
                x.Transmissions,
                x.Start,
                x.End,
                x.Status
            }).ElementAt(0);

            var dayList = new List<DayDto>();

            foreach (var item in dayGroup)
            {
                var cameraOne = UserDto.New(
                    item.Key.IdUserOne,
                    item.Key.NameUserOne,
                    item.Key.EmailUserOne,
                    item.Key.SexUserOne
                );
                var cameraTwo = UserDto.New(
                    item.Key.IdUserTwo,
                    item.Key.NameUserTwo,
                    item.Key.EmailUserTwo,
                    item.Key.SexUserTwo
                );
                var cutDesk = UserDto.New(
                    item.Key.IdUserThree,
                    item.Key.NameUserThree,
                    item.Key.EmailUserThree,
                    item.Key.SexUserThree
                );

                dayList.Add(DayDto.New(
                    item.Key.IdDay,
                    item.Key.NameDay,
                    item.Key.DateDay,
                    item.Key.Month,
                    cameraOne,
                    cameraTwo,
                    cutDesk)
                );
            }

            var scaleDto = ScaleDto.NewDto(scaleGroup.Key.Id, scaleGroup.Key.Name, scaleGroup.Key.Transmissions,
                            scaleGroup.Key.Start, scaleGroup.Key.End, scaleGroup.Key.Status, dayList
                        );

            return scaleDto;
        }
        public static List<ScaleDto> ToDTOList(this IEnumerable<SingleScaleAggregate> singleScaleAggregates)
        {
            var listScaleDto = new List<ScaleDto>();

            foreach (var aggregate in singleScaleAggregates)
            {
                listScaleDto.Add(ScaleDto.NewDto(
                    aggregate.Id,
                    aggregate.Name,
                    aggregate.Transmissions,
                    aggregate.Start,
                    aggregate.End,
                    aggregate.Status.ToString())
                );
            }
            return listScaleDto;
        }
    }
}
