using UnityEngine;
using Zenject;

public class SceneBuilder : MonoInstaller
{
    [SerializeField] private SceneConfig _sceneConfig;

    public override void InstallBindings()
    {
        if (_sceneConfig == null)
        {
            Debug.LogError("SceneConfig is not assigned in SceneBuilder!");
            return;
        }

        Container.Install<CoreInstaller>();
        Container.Install<InputInstaller>();

        if (_sceneConfig.enableCamera)
        {
            Container.Install<CameraInstaller>();
        }

        Container.Install<PlayerInstaller>();
        Container.Install<AgentInstaller>();
        Container.Install<InventoryInstaller>();
        Container.Install<ItemsInstaller>();
        Container.Install<WeaponInstaller>();
        Container.Install<ToolboxInstaller>();
        Container.Install<FactoryInstaller>();

        if (_sceneConfig.enableUI)
        {
            Container.Install<UIInstaller>();
        }
    }
}
