using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Feature.Factory.Infrastructure
{
    public interface IMaterialFactory
    {
        UniTask<Material> GetMaterial(string address);
    }
}