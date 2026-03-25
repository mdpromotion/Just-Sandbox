using System.Threading.Tasks;
using UnityEngine;

namespace Feature.Factory.Infrastructure
{
    public interface IMaterialFactory
    {
        Task<Material> GetMaterial(string address);
    }
}