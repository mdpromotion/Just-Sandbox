using Feature.UI.Maps.Application;
using Feature.UI.Maps.Presentation;
using Feature.UI.Shared;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<NavigationButton>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<LoadSceneButton>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IView>().To<View>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IAnimator>().To<Animator>().AsSingle();
        Container.BindInterfacesTo<Presenter>().AsSingle();
        Container.BindInterfacesTo<StartGameUseCase>().AsSingle();
    }
}
