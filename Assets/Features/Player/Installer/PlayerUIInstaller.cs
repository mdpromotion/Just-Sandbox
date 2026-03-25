using Feature.Player.Presentation;
using Zenject;

public class PlayerUIInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<ILifeView>().To<LifeView>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<LifePresenter>().AsSingle();
    }
}
