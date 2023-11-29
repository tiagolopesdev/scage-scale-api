
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.AggregateRoot.ScaleSingleAggregate
{
    public class SingleScaleAggregate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StatusScaleVo Status { get; set; }
        public int Transmissions { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsEnable { get; set; }
    }
}
