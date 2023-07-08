
using SCAGEScale.Application.VO;

namespace SCAGEScale.Application.RepositorySide
{
    public interface IScaleRepository
    {
        public Task<Guid> CreateScale(List<PropertiesCreateScale> scale, Guid monthId);
    }
}
