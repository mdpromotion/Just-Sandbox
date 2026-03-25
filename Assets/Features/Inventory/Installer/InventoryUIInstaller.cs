using Feature.Inventory.Infrastructure;
using Feature.Inventory.Presentation;
using Zenject;

public class InventoryUIInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<View>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<Presenter>().AsSingle();
        Container.BindInterfacesTo<ItemEffectsCoordinator>().AsSingle();
    }
}
