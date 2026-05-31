using Feature.Combat.Application;
using Features.Combat.Application.Interfaces;
using Features.Combat.Application.UseCases;
using Features.Combat.Domain;
using Features.Combat.Infrastructure;
using Features.Combat.Presentation;
using Features.Combat.Presentation.Interfaces;
using Zenject;

public abstract class WeaponInstaller : Installer
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
