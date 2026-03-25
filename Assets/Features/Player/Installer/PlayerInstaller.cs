using Feature.Core.Infrastructure;
using Feature.Player.Application;
using Feature.Player.Domain;
using Feature.Player.Infrastructure;
using Feature.Player.Presentation;
using Zenject;

public class PlayerInstaller : Installer
{
    public override void InstallBindings()
    {
        BindStates();
        BindControllers();
        BindUseCases();
        BindServices();
    }

    /// <summary>
    /// Registers the PlayerWorldState and MovementInputState types with the dependency injection container as
    /// single-instance services.
    /// </summary>
    /// <remarks>This method ensures that both PlayerWorldState and MovementInputState are available for
    /// dependency injection throughout the application's lifetime, with only one instance of each created and shared.
    /// This promotes consistent state management and resource efficiency across dependent components.</remarks>
    private void BindStates()
    {
        Container.BindInterfacesAndSelfTo<PlayerWorldState>().AsSingle();
        Container.BindInterfacesAndSelfTo<MovementInputState>().AsSingle();
    }

    /// <summary>
    /// Configures the dependency injection container to bind player-related controller interfaces to their
    /// implementations as single instances.
    /// </summary>
    /// <remarks>Call this method during the initialization phase to ensure that all player controller
    /// dependencies, such as life management, input handling, effects, and state tracking, are registered and available
    /// for injection throughout the application. This setup is essential for consistent and centralized management of
    /// player functionality.</remarks>
    private void BindControllers()
    {
        Container.BindInterfacesAndSelfTo<PhysicsController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<PlayerLifeController>().AsSingle();
        Container.BindInterfacesAndSelfTo<Player>().AsSingle();
        Container.BindInterfacesTo<TransformProvider>().AsSingle();
        Container.BindInterfacesTo<GroundCheckUpdater>().AsSingle();
        Container.BindInterfacesTo<PlayerFacade>().AsSingle();
        Container.BindInterfacesTo<PlayerEffectsController>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle();
    }

    /// <summary>
    /// Registers use case implementations and related services with the dependency injection container as singletons.
    /// </summary>
    /// <remarks>This method ensures that MovementCalculator, PunchCalculator, MovementUseCase, and
    /// LifeUseCase are each registered as single instances within the container. As a result, the same instance of each
    /// service is used throughout the application's lifetime, which can improve performance and maintain consistent
    /// state where required.</remarks>
    private void BindUseCases()
    {
        Container.Bind<IMovementCalculator>().To<MovementCalculator>().AsSingle();
        Container.Bind<IPunchCalculator>().To<PunchCalculator>().AsSingle();
        Container.Bind<MovementUseCase>().AsSingle();
        Container.Bind<ILifeUseCase>().To<LifeUseCase>().AsSingle();
    }

    /// <summary>
    /// Registers required services and components with the dependency injection container for the application.
    /// </summary>
    /// <remarks>This method binds the EntityWorldBind and PlayerSpawner components as single instances,
    /// ensuring that only one instance of each is created and shared throughout the application's lifetime. Components
    /// are retrieved from the hierarchy, which allows for flexible scene organization and component
    /// management.</remarks>
    private void BindServices()
    {
        Container.Bind<EntityWorldBind>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<PlayerSpawner>().FromComponentInHierarchy().AsSingle();
    }
}