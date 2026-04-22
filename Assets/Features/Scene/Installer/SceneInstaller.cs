using Feature.Scene.Application;
using Feature.Scene.Infrastructure;
using Zenject;

namespace Feature.Scene.Installer
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISceneManager>().To<SceneManager>().AsSingle();
            Container.Bind<SceneLoadService>().AsSingle();
            Container.Bind<ILoadSceneUseCase>().To<LoadSceneUseCase>().AsSingle();
        }
    }
}