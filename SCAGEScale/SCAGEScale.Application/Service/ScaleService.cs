﻿
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.Extensions;
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
            var response = await _scaleQuery.GetScaleById(id);

            var daysOrdered = response.Days.OrderBy(item => item.DateTime).ToList();

            response.Days = daysOrdered;

            return response;
        }

        public async Task<Guid> CreateScale(CreateScaleDto createScaleDto)
        {
            try
            {
                var scaleExists = await _scaleQuery.GetAllSingleByFilterScales(createScaleDto.Name);

                if (scaleExists != null && scaleExists.Count > 0) throw new Exception($"Escala referente ao mês de {createScaleDto.Name}, já existe.");

                var month = PropertiesManipulationMonth.PropertiesToCreateMonth(createScaleDto);

                var monthId = (Guid)month.MonthId;

                var scale = PropertiesManipulationMonth.PropertiesToCreateDay(createScaleDto.Days.ToAggregateListCreate().ToList(), monthId);

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
            var month = PropertiesManipulationMonth.PropertiesToUpdateMonth(updateScaleDto);

            var dayToCreateOrUpdate = new List<PropertiesManipulationMonth>();

            var daysToCreate = updateScaleDto.Days.Where(item => item.Id == null).ToList();

            dayToCreateOrUpdate.AddRange(PropertiesManipulationMonth.PropertiesToCreateDay(
                daysToCreate.ToAggregateListUpdate().ToList(),
                updateScaleDto.Id)
                );

            var daysToUpdate = updateScaleDto.Days.Where(item => item.Id != null).ToList();

            dayToCreateOrUpdate.AddRange(PropertiesManipulationMonth.PropertiesToUpdateDay(
                daysToUpdate.ToAggregateListUpdate().ToList(),
                updateScaleDto.Id)
                );

            dayToCreateOrUpdate.Insert(0, month);

            return await _scaleRepository.TransitionsScale(dayToCreateOrUpdate, updateScaleDto.Id);
        }

        public List<EventsGeneratedDTO> GenerationDays(GenerationDaysDTO request)
        {
            bool[] days = { false, false, false, false, false, false, false };

            foreach (var item in request.Days)
            {
                switch (item.Day)
                {
                    case DayOfWeek.Sunday:
                        days[0] = true;
                        break;
                    case DayOfWeek.Monday:
                        days[1] = true;
                        break;
                    case DayOfWeek.Tuesday:
                        days[2] = true;
                        break;
                    case DayOfWeek.Wednesday:
                        days[3] = true;
                        break;
                    case DayOfWeek.Thursday:
                        days[4] = true;
                        break;
                    case DayOfWeek.Friday:
                        days[5] = true;
                        break;
                    case DayOfWeek.Saturday:
                        days[6] = true;
                        break;
                }
            }

            List<EventsGeneratedDTO> daysFounded = new();

            DateTime dateToCompare = request.PeriodStart.Date;

            while (dateToCompare <= request.PeriodEnd)
            {
                if (DayExtension.DayIsMatching(dateToCompare.DayOfWeek, DayOfWeek.Sunday, days[0]))
                {
                    daysFounded.Add(DayExtension.InclusionMatchingDay(dateToCompare, request.Days, DayOfWeek.Sunday));
                }
                else if (DayExtension.DayIsMatching(dateToCompare.DayOfWeek, DayOfWeek.Monday, days[1]))
                {
                    daysFounded.Add(DayExtension.InclusionMatchingDay(dateToCompare, request.Days, DayOfWeek.Monday));
                }
                else if (DayExtension.DayIsMatching(dateToCompare.DayOfWeek, DayOfWeek.Tuesday, days[2]))
                {
                    daysFounded.Add(DayExtension.InclusionMatchingDay(dateToCompare, request.Days, DayOfWeek.Tuesday));
                }
                else if (DayExtension.DayIsMatching(dateToCompare.DayOfWeek, DayOfWeek.Wednesday, days[3]))
                {
                    daysFounded.Add(DayExtension.InclusionMatchingDay(dateToCompare, request.Days, DayOfWeek.Wednesday));
                }
                else if (DayExtension.DayIsMatching(dateToCompare.DayOfWeek, DayOfWeek.Thursday, days[4]))
                {
                    daysFounded.Add(DayExtension.InclusionMatchingDay(dateToCompare, request.Days, DayOfWeek.Thursday));
                }
                else if (DayExtension.DayIsMatching(dateToCompare.DayOfWeek, DayOfWeek.Friday, days[5]))
                {
                    daysFounded.Add(DayExtension.InclusionMatchingDay(dateToCompare, request.Days, DayOfWeek.Friday));
                }
                else if (DayExtension.DayIsMatching(dateToCompare.DayOfWeek, DayOfWeek.Saturday, days[6]))
                {
                    daysFounded.Add(DayExtension.InclusionMatchingDay(dateToCompare, request.Days, DayOfWeek.Saturday));
                }

                dateToCompare = dateToCompare.AddDays(1);
            }
            return daysFounded;
        }
    }
}
