
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.DTO
{
    public class GenerationDaysDTO
    {
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public List<DaysGenerate> Days { get; set; }
    }

    public class DaysGenerate
    {
        public DayOfWeek Day { get; set; }
        public string NameEvent { get; set; }
        public TimeOnly Time { get; set; }
    }    
}
