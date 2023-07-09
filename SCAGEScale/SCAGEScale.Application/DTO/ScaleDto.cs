
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.DTO
{
    public class ScaleDto
    {
        public string Name { get; set; }
        public int Transmissions { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public StatusScaleVo Status { get; set; }
        public List<DayDto> Days { get; set; }
    }
}
