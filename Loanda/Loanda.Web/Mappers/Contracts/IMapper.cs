
namespace Loanda.Web.Mappers.Contracts
{
    public interface IMapper<TEntity, UViewModel>
    {
        UViewModel Map(TEntity entity);
    }
}
