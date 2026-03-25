using System.Threading.Tasks;
using UnityEngine;

namespace Feature.Factory.Infrastructure
{
    public interface ISpriteFactory
    {
        Task<Sprite> GetSprite(string address, string spriteName = null);
    }
}