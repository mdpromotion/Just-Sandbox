using Features.Camera.Application.Helpers;
using Features.Camera.Application.UseCases;
using Features.Camera.Infrastructure;
using Shared.Providers;
using Unity.Cinemachine;
using CameraState = Features.Camera.Domain.CameraState;

namespace Features.Camera.Installer
{
    public class CameraInstaller : Zenject.Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<ICameraTransformData>().To<TransformProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraState>().AsSingle();
            Container.Bind<CinemachineCamera>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PhysicsService>().AsSingle();
            Container.Bind<RotationCalculator>().AsSingle();
            Container.Bind<CameraUseCase>().AsSingle();
        }
    }
}
