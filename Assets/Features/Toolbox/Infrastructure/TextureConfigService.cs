using UnityEngine;

public class TextureConfigService : ITextureConfigService
{
    private readonly TextureDatabase _textureDatabase;

    public TextureConfigService(TextureDatabase textureDatabase)
    {
        _textureDatabase = textureDatabase;
    }

    public Result<string> GetMaterialAddress(int id)
    {
        var material = _textureDatabase.GetMaterialById(id);
        if (!material.IsSuccess) return Result<string>.Failure(material.Error);

        return Result<string>.Success(material.Value);
    }
}
