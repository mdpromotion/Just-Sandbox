using Core.PlayerInput;
using Zenject;

public class InputInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<IMovementInput>().To<DesktopMovementInput>().AsSingle();
        Container.Bind<IInventoryInput>().To<DesktopInventoryInput>().AsSingle();
        Container.Bind<ICameraInput>().To<DesktopCameraInput>().AsSingle();
        Container.Bind<IInteractionInput>().To<DesktopInteractionInput>().AsSingle();
        Container.Bind<ISpecialButtonInput>().To<DesktopSpecialButtonInput>().AsSingle();

        Container.BindInterfacesAndSelfTo<InputManager>().AsSingle().NonLazy();
    } 
}
