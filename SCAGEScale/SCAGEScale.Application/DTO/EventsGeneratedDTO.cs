
namespace SCAGEScale.Application.DTO
{
    public class EventsGeneratedDTO
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }

        public static EventsGeneratedDTO New(string name, DateTime dateTime)
        {
            return new EventsGeneratedDTO { Name = name, DateTime = dateTime };
        }
    }
}
