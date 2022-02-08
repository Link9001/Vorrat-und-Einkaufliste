using HouseholdmanagementTool.UI.ViewModel.TabViewModel;
using HouseholdmanagementTool.UI.ViewModel.Windows;
using Unity;

namespace HouseholdmanagementTool.UI;

public static class Configuration
{
    public static IUnityContainer CreateUnityContainer()
    {
        IUnityContainer container = new UnityContainer();
        return container.RegisterType<IUnityContainer>(TypeLifetime.PerContainer)
            .RegisterInstance(container);
    }

    public static IUnityContainer ConfigureApp(this IUnityContainer container)
    {
        return container.RegisterType<RecipeTabViewModel>()
            .RegisterType<SettingsTabViewModel>()
            .RegisterType<StockTabViewModel>()
            .RegisterType<NewFoodStuffViewModel>()
            .RegisterType<NewIngredientViewModel>();
    }
}