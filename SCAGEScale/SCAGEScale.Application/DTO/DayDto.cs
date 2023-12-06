
namespace SCAGEScale.Application.DTO
{
    public class DayDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public Guid Month { get; set; }
        public UserDto CameraOne { get; set; }
        public UserDto CameraTwo { get; set; }
        public UserDto CutDesk { get; set; }
        public bool IsEnable { get; set; }

        public static DayDto New(Guid id, string name, DateTime dateTime, Guid month, bool isEnable, UserDto cameraOne, UserDto cameraTwo, UserDto cutDesk)
        {
            var dtoToReturn = new DayDto
            {
                Id = id,
                Name = name,
                DateTime = dateTime,
                Month = month,
                IsEnable = isEnable,
                CameraOne = cameraOne,
                CameraTwo = cameraTwo,
                CutDesk = cutDesk
            };
            return dtoToReturn;
        }
    }
}
