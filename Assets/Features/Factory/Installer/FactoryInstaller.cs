using Feature.Factory.Infrastructure;
using Zenject;

public class FactoryInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<IGameObjectFactory>().To<GameObjectFactory>().AsSingle();
        Container.Bind<ISpriteFactory>().To<SpriteFactory>().AsSingle();
        Container.Bind<IMaterialFactory>().To<MaterialFactory>().AsSingle();
    }
}
