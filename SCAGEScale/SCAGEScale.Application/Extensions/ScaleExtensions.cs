
using SCAGEScale.Application.AggregateRoot.ScaleSingleAggregate;
using SCAGEScale.Application.DTO;

namespace SCAGEScale.Application.Extensions
{
    public static class ScaleExtensions
    {
        public static List<ScaleDto> ToDTOList(this IEnumerable<SingleScaleAggregate> singleScaleAggregates)
        {
            var listScaleDto = new List<ScaleDto>();

            foreach (var aggregate in singleScaleAggregates)
            {
                listScaleDto.Add(new ScaleDto(
                    aggregate.Id,
                    aggregate.Name,
                    aggregate.Transmissions,
                    aggregate.Start,
                    aggregate.End,
                    aggregate.Status)
                );
            }

            return listScaleDto;
        }
    }
}
