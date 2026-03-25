using Feature.PlayerCamera.Application;
using Feature.PlayerCamera.Infrastructure;
using Shared.Providers;
using Unity.Cinemachine;
using Zenject;

public class CameraInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<ICameraTransformData>().To<TransformProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<Feature.PlayerCamera.Domain.CameraState>().AsSingle();
        Container.Bind<CinemachineCamera>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<InputController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PhysicsService>().AsSingle();
        Container.Bind<RotationCalculator>().AsSingle();
        Container.Bind<CameraUseCase>().AsSingle();
    }
}
