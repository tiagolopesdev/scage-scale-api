
using SCAGEScale.Application.VO;
using System.Text.Json.Serialization;

namespace SCAGEScale.Application.DTO
{
    public class ScaleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Transmissions { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public StatusScaleVo Status { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<DayDto>? Days { get; set; }
        public bool IsEnable { get; set; }


        public static ScaleDto New(Guid id, string name, int transmissions, bool isEnable, DateTime start, DateTime end, string status, List<DayDto>? days = null)
        {
            var dto = new ScaleDto
            {
                Id = id,
                Name = name,
                Transmissions = transmissions,
                IsEnable = isEnable,
                Start = start,
                End = end,
                Status = StatusScaleVo.COMPLETE.ToString() == status ? StatusScaleVo.COMPLETE : StatusScaleVo.IN_PROGRESS
            };
            if (days != null) dto.Days = days;

            return dto;
        }
    }
}
