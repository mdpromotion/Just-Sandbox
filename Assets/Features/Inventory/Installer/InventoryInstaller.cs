using Feature.Inventory.Application;
using Feature.Inventory.Domain;
using Feature.Inventory.Infrastructure;
using System.ComponentModel;
using Zenject;

public class InventoryInstaller : Installer
{
    public override void InstallBindings()
    {
        BindServices();
        BindControllers();
        BindInventory();
        BindUseCases();
        BindOrchestrator();
    }

    private void BindServices()
    {
        Container.Bind<IGameObjectProvider>().To<GameObjectProvider>().AsSingle();
        Container.Bind<IInteractionWorldService>().To<InteractionWorldService>().AsSingle();
    }

    private void BindControllers()
    {
        Container.BindInterfacesTo<InventoryInputController>().AsSingle();
        Container.BindInterfacesTo<CooldownService>().AsSingle();
    }

    private void BindInventory()
    {
        Container.BindInterfacesTo<PlayerInventory>().AsSingle();
    }

    private void BindUseCases()
    {
        Container.Bind<IManagementUseCase>().To<ManagementUseCase>().AsSingle();
        Container.Bind<IInteractionUseCase>().To<InteractionUseCase>().AsSingle();
        Container.Bind<IItemMover>().To<ItemMover>().AsSingle();
    }

    private void BindOrchestrator()
    {
        Container.BindInterfacesTo<InventoryOrchestrator>().AsSingle();
    }
}