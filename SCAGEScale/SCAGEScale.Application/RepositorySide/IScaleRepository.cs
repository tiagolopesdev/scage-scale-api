
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.RepositorySide
{
    public interface IScaleRepository
    {
        public Task<Guid> TransitionsScale(List<PropertiesCreateScale> scale, Guid monthId);
    }
}
