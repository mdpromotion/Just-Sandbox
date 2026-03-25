using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GameMonoInstaller : MonoInstaller
{
    [Header("Scene References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform handParent;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private AgentDatabase agentDatabase;
    [SerializeField] private TextureDatabase textureDatabase;


    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();

        Container.Bind<Transform>().WithId("Player").FromInstance(playerTransform);
        Container.Bind<Transform>().WithId("HandParent").FromInstance(handParent);
        Container.Bind<Transform>().WithId("InventoryParent").FromInstance(inventoryParent);

        Container.Bind<Rigidbody>().FromInstance(playerRigidbody).AsSingle();
        Container.Bind<CapsuleCollider>().FromInstance(playerCollider).AsSingle();

        Container.Bind<ItemDatabase>().FromInstance(itemDatabase).AsSingle();
        Container.Bind<AgentDatabase>().FromInstance(agentDatabase).AsSingle();
        Container.Bind<TextureDatabase>().FromInstance(textureDatabase).AsSingle();
    }
}