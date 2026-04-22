using Feature.ExitMenu.Application;
using Feature.ExitMenu.Domain;
using Feature.ExitMenu.Infrastructure;
using Feature.ExitMenu.Presentation;
using Zenject;

namespace Feature.ExitMenu.Installers
{
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
}