using Core.Data;
using Core.Providers;
using Core.Service;
using Feature.Storage.Domain;
using Shared.Providers;
using Shared.Service;
using UnityEngine;
using Zenject;

public class CoreInstaller : Installer
{
    public override void InstallBindings()
    {
        DataInitialize();

        Container.Bind<ITimeProvider>().To<TimeProvider>().AsSingle();
        Container.Bind<IRandomProvider>().To<RandomProvider>().AsSingle();
        Container.Bind<ILogger>().FromInstance(Debug.unityLogger).AsSingle();
        Container.Bind<IDelay>().To<DelayService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IAudioPlayer>().To<AudioPlayer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IRaycastService>().To<RaycastService>().AsSingle();
        Container.Bind<IWorldEntityService>().To<WorldEntityService>().AsSingle();

        Container.BindInterfacesTo<CoreGameStates>().AsSingle();

        #if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
        #else
            Debug.unityLogger.logEnabled = false;
        #endif
    }

    void DataInitialize()
    {
        Container.BindInterfacesTo<GameState>().AsSingle();
        Container.BindInterfacesAndSelfTo<ControlSettings>().AsSingle();
        Container.BindInterfacesAndSelfTo<Feature.Storage.Domain.AudioSettings>().AsSingle();
        Container.BindInterfacesAndSelfTo<GraphicsSettings>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerProgress>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerEconomy>().AsSingle();
        Container.BindInterfacesAndSelfTo<PurchaseItem>().AsSingle();
    }
}
