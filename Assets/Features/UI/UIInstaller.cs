using Core.Providers;
using Feature.Combat.Installers;
using Feature.ExitMenu.Installers;
using Feature.Inventory.Installers;
using Feature.Player.Installers;
using Feature.Toolbox.Installers;
using Feature.UI.Utils;
using Zenject;

public class UIInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<CursorProvider>().AsSingle();
        Container.BindInterfacesTo<ToggleMenuUseCase>().AsSingle();
        Container.Bind<IAnimator>().To<Animator>().AsSingle();

        ContainersInstall();
    }

    private void ContainersInstall()
    {
        Container.Install<ToolboxUIInstaller>();
        Container.Install<PlayerUIInstaller>();
        Container.Install<WeaponUIInstaller>();
        Container.Install<InventoryUIInstaller>();
        Container.Install<PlayerMenuInstaller>();
    }
}
