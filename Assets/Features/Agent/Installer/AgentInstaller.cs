using Feature.Agent.Application;
using Feature.Agent.Domain;
using Feature.Agent.Infrastructure;
using Feature.Toolbox.Infrastructure;
using Zenject;

public class AgentInstaller : Installer
{
    public override void InstallBindings()
    {
        BindServices();
        BindUseCases();
        BindFactories();
    }

    private void BindServices()
    {
        Container.Bind<ICooldownService>().To<CooldownService>().AsSingle();
        Container.BindInterfacesTo<AIUpdateService>().AsSingle();
        Container.BindInterfacesTo<AgentLifeController>().AsSingle();
    }

    private void BindUseCases()
    {
        Container.Bind<IAttackUseCase>().To<AttackUseCase>().AsSingle();
        Container.Bind<ILifeUseCase>().To<LifeUseCase>().AsSingle();
        Container.Bind<IWorldAgentUseCase>().To<WorldAgentUseCase>().AsSingle();
    }

    private void BindFactories()
    {
        Container.Bind<IAgentComponentResolver>().To<AgentComponentResolver>().AsSingle();
        Container.Bind<IAgentControllerFactory>().To<AgentControllerFactory>().AsSingle();
        Container.Bind<IAgentFactory>().To<AgentFactory>().AsSingle();
        Container.Bind<IAgentFsmFactory>().To<AgentFsmFactory>().AsSingle();
        Container.Bind<IFacadeFactory>().To<FacadeFactory>().AsSingle();
        Container.Bind<IAgentAssembler>().To<AgentAssembler>().AsSingle();
        Container.Bind<IAgentConfigurator>().To<AgentConfigurator>().AsSingle();
    }
}