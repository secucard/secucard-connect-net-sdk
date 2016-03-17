namespace Secucard.Connect.Client
{
    public interface IService
    {
        ClientContext Context { get; set; }
        void RegisterEvents();
    }
}