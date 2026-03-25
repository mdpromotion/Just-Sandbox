using Feature.Scene.Infrastructure;
using Zenject;

public class SceneInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<ISceneManager>().To<SceneManager>().AsSingle();
        Container.Bind<SceneLoadService>().AsSingle();
    }
}
