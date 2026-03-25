using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TextureEntry
{
    public int Id;
    public string MaterialAddress;
}


[CreateAssetMenu(fileName = "TextureDatabase", menuName = "Database/TextureDatabase")]
public class TextureDatabase : ScriptableObject
{
    [SerializeField] private List<TextureEntry> _textures;

    public Result<string> GetMaterialById(int id)
    {
        var item = _textures.FirstOrDefault(s => s.Id == id);
        if (item != null)
        {
            return Result<string>.Success(item.MaterialAddress);
        }
        else
        {
            return Result<string>.Failure($"Item with ID {id} not found.");
        }
    }
}
