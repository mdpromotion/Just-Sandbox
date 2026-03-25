using UnityEngine;

public enum ItemType
{
    Object,
    Weapon
}

[CreateAssetMenu(fileName = "Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public int Id;
    public string Name;
    public ItemType Type;
    public string PrefabAddress;
    public string SpriteAddress;
    public Vector3 DisplayOffset;
}