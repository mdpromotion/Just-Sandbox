using Features.Combat.Presentation;
using Features.Combat.Presentation.View;

namespace Features.Combat.Installer
{
    public class WeaponUIInstaller : Zenject.Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Presenter>().AsSingle();
            Container.Bind<AmmoView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<View>().AsSingle();
        }
    }
}