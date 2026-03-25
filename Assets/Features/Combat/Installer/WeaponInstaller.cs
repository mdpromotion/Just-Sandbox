using Feature.Combat.Application;
using Feature.Combat.Domain;
using Feature.Combat.Infrastructure;
using Feature.Combat.Presentation;
using Zenject;

public class WeaponInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
        Container.BindInterfacesTo<InputController>().AsSingle();

        Container.Bind<IParticleAnimator>().To<ParticleAnimator>().AsSingle();
        Container.BindInterfacesTo<WeaponEffectsCoordinator>().AsSingle();
        Container.Bind<IWeaponService>().To<WeaponService>().AsSingle();

        Container.BindInterfacesAndSelfTo<WeaponInventory>().AsSingle();
        Container.Bind<IWeaponItemUseCase>().To<WeaponItemUseCase>().AsSingle();
        Container.Bind<IUseItemUseCase>().To<UseItemUseCase>().AsSingle();
        Container.Bind<IWeaponShotUseCase>().To<WeaponShotUseCase>().AsSingle();
        Container.BindInterfacesTo<UseItemOrchestrator>().AsSingle();
    }
}
