
namespace Loanda.Web.Mappers.Contracts
{
    public interface IMapper<TDto, UViewModel>
    {
        UViewModel Map(TDto dto);
    }
}
