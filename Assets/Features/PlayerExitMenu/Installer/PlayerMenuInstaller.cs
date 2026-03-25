using Feature.PlayerExitMenu.Application;
using Feature.PlayerExitMenu.Domain;
using Feature.PlayerExitMenu.Infrastructure;
using Feature.PlayerExitMenu.Presentation;
using Zenject;

public class PlayerMenuInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<MenuInputController>().AsSingle();
        Container.BindInterfacesTo<MenuData>().AsSingle();
        Container.Bind<IMenuUseCase>().To<MenuUseCase>().AsSingle();
        Container.BindInterfacesTo<Presenter>().AsSingle();
        Container.Bind<IView>().To<View>().FromComponentInHierarchy().AsSingle();
    }
}
