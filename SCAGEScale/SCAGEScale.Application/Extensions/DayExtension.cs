﻿
using SCAGEScale.Application.AggregateRoot.DayAggregate;
using SCAGEScale.Application.DTO;

namespace SCAGEScale.Application.Extensions
{
    public static class DayExtension
    {
        public static DayAggregate ToAggregateCreate(this DayOnlyReferencyCreateDto day)
        {
            return DayAggregate.New(
                    Guid.NewGuid(),
                    day.Name,
                    day.DateTime,
                    day.CameraOne,
                    day.CameraTwo,
                    day.CutDesk
                    );
        }
        public static IEnumerable<DayAggregate> ToAggregateListCreate(this List<DayOnlyReferencyCreateDto> days)
        {
            var listToReturn = new List<DayAggregate>();

            foreach (var day in days)
            {
                listToReturn.Add(ToAggregateCreate(day));
            }

            return listToReturn;
        }
        public static DayAggregate ToAggregateUpdate(this DayOnlyReferencyUpdateDto day)
        {
            return DayAggregate.New(
                    day.Id != null ? (Guid)day.Id : Guid.NewGuid(),
                    day.Name,
                    day.DateTime,
                    day.CameraOne,
                    day.CameraTwo,
                    day.CutDesk,
                    day.IsEnable
                    );
        }
        public static IEnumerable<DayAggregate> ToAggregateListUpdate(this List<DayOnlyReferencyUpdateDto> days)
        {
            var listToReturn = new List<DayAggregate>();

            foreach (var day in days)
            {
                listToReturn.Add(ToAggregateUpdate(day));
            }

            return listToReturn;
        }
    }
}
