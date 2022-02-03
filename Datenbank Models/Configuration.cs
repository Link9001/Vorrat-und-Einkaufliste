using Unity;

namespace Database_Models;

public static class ConfigurationDatabase
{
    public static IUnityContainer ConfigureDatabase(this IUnityContainer container)
    {
        container.RegisterType<Database>(TypeLifetime.Singleton);
        return container;
    }
}