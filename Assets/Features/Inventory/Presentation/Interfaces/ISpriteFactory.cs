using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Feature.Factory.Infrastructure
{
    public interface ISpriteFactory
    {
        UniTask<Sprite> GetSprite(string address, string spriteName = null);
    }
}