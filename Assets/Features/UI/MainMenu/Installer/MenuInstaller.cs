using Feature.MainMenu.Application;
using Feature.MainMenu.Presentation;
using Feature.UI.Presentation;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Install<SceneInstaller>();
        Container.Bind<NavigationButton>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<LoadSceneButton>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<IView>().To<View>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IAnimator>().To<Animator>().AsSingle();
        Container.BindInterfacesTo<Presenter>().AsSingle();
        Container.BindInterfacesTo<StartGameUseCase>().AsSingle();
    }
}
