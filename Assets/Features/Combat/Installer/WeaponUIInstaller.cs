using Feature.Combat.Presentation;
using Zenject;

namespace Feature.Combat.Installers
{
    public class WeaponUIInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Presenter>().AsSingle();
            Container.Bind<AmmoView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<View>().AsSingle();
        }
    }
}