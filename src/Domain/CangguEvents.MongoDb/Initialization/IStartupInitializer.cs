namespace CangguEvents.MongoDb.Initialization
{
    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
    }

}