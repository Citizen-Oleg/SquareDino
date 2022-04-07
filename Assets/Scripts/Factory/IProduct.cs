namespace DefaultNamespace
{
    public interface IProduct<T> where T: IProduct<T>
    {
        int ID { get; }
    }
}