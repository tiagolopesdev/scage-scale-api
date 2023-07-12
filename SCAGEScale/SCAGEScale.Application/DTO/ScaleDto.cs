
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

        public ScaleDto(Guid id, string name, int transmissions, DateTime start, DateTime end, StatusScaleVo status, List<DayDto>? days = null)
        {
            Id = id;
            Name = name;
            Transmissions = transmissions;
            Start = start;
            End = end;
            Status = status;
            if (days != null) Days = days;
        }
    }
}
