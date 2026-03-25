using UnityEngine;

public interface ITextureConfigService
{
    Result<string> GetMaterialAddress(int id);
}
