namespace BoundsApp.Biz.Core.Application.Global
{
    public interface IFactory<T>
    {
        T Get();
    }
}
