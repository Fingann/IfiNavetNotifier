namespace IfiNavet.Application.ApiLogic.Mapper
{
    public interface IMapper<in T, out TG>
    {
        TG Map(T instance);
    }
}