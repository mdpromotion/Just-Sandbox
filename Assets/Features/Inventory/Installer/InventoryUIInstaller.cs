using Feature.Inventory.Presentation;
using Zenject;

namespace Feature.Inventory.Installers
{
    public class InventoryUIInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<View>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<Presenter>().AsSingle();
            Container.BindInterfacesTo<ItemEffectsCoordinator>().AsSingle();
        }
    }
}