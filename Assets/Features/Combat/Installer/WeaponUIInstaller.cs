using Feature.Combat.Presentation;
using Zenject;

public class WeaponUIInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<Presenter>().AsSingle();
        Container.Bind<AmmoView>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<View>().AsSingle();
    }
}