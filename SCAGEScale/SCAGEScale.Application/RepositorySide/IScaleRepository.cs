
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.RepositorySide
{
    public interface IScaleRepository
    {
        public Task<Guid> TransitionsScale(List<PropertiesManipulationMonth> scale, Guid monthId);
    }
}
