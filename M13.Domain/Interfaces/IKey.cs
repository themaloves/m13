namespace M13.Domain.Interfaces
{
    public interface IKey<T>
    {
        T Id { get; set; }
    }
}