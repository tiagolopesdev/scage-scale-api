
namespace SCAGEScale.Application.DTO
{
    public class UpdateScaleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<DayOnlyReferencyUpdateDto> Days { get; set; }
        public bool IsEnable { get; set; }
    }
}
