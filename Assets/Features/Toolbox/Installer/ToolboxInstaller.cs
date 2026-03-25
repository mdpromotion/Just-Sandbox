using Feature.Agent.Infrastructure;
using Feature.Toolbox.Application;
using Feature.Toolbox.Application.UseCases;
using Feature.Toolbox.Domain;
using Feature.Toolbox.Infrastructure;
using Shared.Service;
using System.ComponentModel;
using Zenject;

public class ToolboxInstaller : Installer
{
    public override void InstallBindings()
    {
        BindServices();
        BindStateAndControllers();
        BindUseCases();
        BindOrchestrators();
    }

    private void BindServices()
    {
        Container.Bind<ITransformWorldService>().To<TransformWorldService>().AsSingle();
        Container.Bind<IItemConfigService>().To<ItemConfigService>().AsSingle();
        Container.Bind<IAgentConfigService>().To<AgentConfigService>().AsSingle();
        Container.Bind<ITextureConfigService>().To<TextureConfigService>().AsSingle();
    }

    private void BindStateAndControllers()
    {
        Container.BindInterfacesTo<ToolboxState>().AsSingle();
        Container.BindInterfacesTo<MenuUseCase>().AsSingle();
        Container.BindInterfacesTo<InputController>().AsSingle();
    }

    private void BindUseCases()
    {
        Container.Bind<IItemSpawnUseCase>().To<ItemSpawnUseCase>().AsSingle();
        Container.Bind<INPCSpawnUseCase>().To<NPCSpawnUseCase>().AsSingle();
    }

    private void BindOrchestrators()
    {
        Container.Bind<ISpawnUseCase>().WithId("Item").To<SpawnItemFromToolboxOrchestrator>().AsSingle();
        Container.Bind<ISpawnUseCase>().WithId("NPC").To<SpawnNPCFromToolboxOrchestrator>().AsSingle();
    }
}