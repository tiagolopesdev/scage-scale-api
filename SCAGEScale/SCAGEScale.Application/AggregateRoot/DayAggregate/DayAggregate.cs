
namespace SCAGEScale.Application.AggregateRoot.DayAggregate
{
    public class DayAggregate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public Guid CameraOne { get; set; }
        public Guid CameraTwo { get; set; }
        public Guid CutDesk { get; set; }
        public bool IsEnable { get; set; }

        public static DayAggregate New(Guid id, string name, DateTime date, Guid cameraOne, Guid cameraTwo, Guid cutDesk, bool isEnable = true)
        {
            return new DayAggregate
            {
                Id = id,
                Name = name,
                DateTime = date,
                CameraOne = cameraOne,
                CameraTwo = cameraTwo,
                CutDesk = cutDesk,
                IsEnable = isEnable
            };
        }
    }
}
