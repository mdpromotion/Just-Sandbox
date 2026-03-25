using Feature.Toolbox.Application;
using Feature.Toolbox.Presentation;
using Feature.Toolbox.Presentation.Buttons;
using System.Collections.Generic;
using Zenject;

public class ToolboxUIInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<IView>().To<View>().FromComponentInHierarchy().AsSingle();

        InstallButtons();
        InstallPresenter();
        Container.Bind<Dictionary<SpawnCategory, ISpawnUseCase>>().FromMethod(context =>
        {
            return new Dictionary<SpawnCategory, ISpawnUseCase>
            {
                { SpawnCategory.Item, context.Container.ResolveId<ISpawnUseCase>("Item") },
                { SpawnCategory.NPC, context.Container.ResolveId<ISpawnUseCase>("NPC")  }
            };

        }).AsSingle();
    }

    private void InstallButtons()
    {
        Container.Bind<TabButtonView>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<SpawnButtonView>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<SpawnModeButtonView>().FromComponentsInHierarchy().AsSingle();
        Container.Bind<TextureButtonView>().FromComponentsInHierarchy().AsSingle();
    }

    private void InstallPresenter()
    {
        Container.BindInterfacesTo<MenuPresenter>().AsSingle();
        Container.BindInterfacesTo<SpawnPresenter>().AsSingle();
        Container.BindInterfacesTo<SpawnSettingsPresenter>().AsSingle();
    }
}
