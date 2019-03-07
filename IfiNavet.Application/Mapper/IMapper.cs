namespace IfiNavet.Application.Mapper
{
    public interface IMapper<in T, out TG>
    {
        TG Map(T instance);
    }
}