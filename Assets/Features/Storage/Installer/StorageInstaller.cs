using Zenject;

public class StorageInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<IPlayerStorage>().To<PlayerStorage>().AsSingle();
    }
}
