using Feature.Storage.Domain;
using Feature.Storage.Infrastructure;
using Zenject;

public class StorageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallDomains();
        Container.Bind<IPlayerStorage>().To<PlayerStorage>().AsSingle();
        Container.Bind<IStorageDataService>().To<StorageDataService>().AsSingle();
        Container.Bind<StorageBootstrap>().AsSingle().NonLazy();
    }

    private void InstallDomains()
    {
        Container.Bind<IReadOnlyPurchaseItem>().To<PurchaseItem>().AsSingle();
        Container.Bind<IReadOnlyAudioSettings>().To<AudioSettings>().AsSingle();
        Container.Bind<IReadOnlyControlSettings>().To<ControlSettings>().AsSingle();
        Container.Bind<IReadOnlyGraphicsSettings>().To<GraphicsSettings>().AsSingle();
        Container.Bind<IReadOnlyPlayerProgress>().To<PlayerProgress>().AsSingle();
    }
}
