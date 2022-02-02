using RezepteSammelung.ViewModel.TabViewModel;
using RezepteSammelung.ViewModel.Windows;
using Unity;

namespace RezepteSammelung
{
    public static class Configure
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
}
