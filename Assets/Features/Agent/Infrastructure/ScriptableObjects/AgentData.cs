using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "AgentData", menuName = "Data/Agent")]
public class AgentData : ScriptableObject
{
    public int Id;
    public string Name;
    public float MaxHealth;
    public float Speed;
    public float Damage;
    public float AttackSpeed;
    public float VisionRange;
    public string PrefabAddress;
}
