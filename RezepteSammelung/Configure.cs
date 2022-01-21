using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RezepteSammelung.ViewModel.TabViewModel;
using RezepteSammelung.ViewModel.Windows;
using Unity;
using Unity.Lifetime;

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
                .RegisterType<NewIngredientViewModel>()
                .RegisterType<NewOvenSettingsViewModel>()
                .RegisterType<NewPlacementViewModel>()
                .RegisterType<MainWindowViewModel>();
        }
    }
}
