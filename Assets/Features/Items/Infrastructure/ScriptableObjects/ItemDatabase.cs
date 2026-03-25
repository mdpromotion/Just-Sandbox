using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDB", menuName = "Database/Item")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private ItemData[] _items;

    #nullable enable
    public Result<ItemData> GetItemById(int id)
    {
        var item = _items.FirstOrDefault(s => s.Id == id);
        if (item != null)
        {
            return Result<ItemData>.Success(item);
        }
        else
        {
            return Result<ItemData>.Failure($"Item with ID {id} not found.");
        }
    }
    #nullable disable
}
