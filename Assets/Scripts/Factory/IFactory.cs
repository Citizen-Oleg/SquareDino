namespace DefaultNamespace
{
    public interface IFactory<T> where T : IProduct<T>
    {
        T GetProduct(T product);
        void ReleaseProduct(T product);
    }
}