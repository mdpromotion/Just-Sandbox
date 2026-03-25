using Feature.Items.Application;
using Feature.Items.Infrastructure;
using Shared.Repository;
using Shared.Service;
using Zenject;

public class ItemsInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<IWorldPhysicsService>().To<WorldPhysicsService>().AsSingle();
        Container.Bind<IWorldItemConfigurator>().To<WorldItemConfigurator>().AsSingle();
        Container.Bind<IWorldItemLinkService>().To<WorldItemLinkService>().AsSingle();
        Container.Bind<IWorldItemRepository>().To<WorldItemRepository>().AsSingle();
        Container.BindInterfacesTo<ItemAnimator>().FromComponentInHierarchy().AsSingle();

        Container.Bind<IWorldItemUseCase>().To<WorldItemUseCase>().AsSingle();

    }
}
