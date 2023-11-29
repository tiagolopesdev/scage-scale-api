
namespace SCAGEScale.Application.DTO
{
    public class CreateScaleDto
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<DayOnlyReferencyCreateDto> Days { get; set; }
    }
}
