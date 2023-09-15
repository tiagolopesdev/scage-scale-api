
namespace SCAGEScale.Application.DTO
{
    public class DayOnlyReferencyUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public Guid CameraOne { get; set; }
        public Guid CameraTwo { get; set; }
        public Guid CutDesk { get; set; }
    }
}
