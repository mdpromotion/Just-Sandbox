using System.Threading.Tasks;
using UnityEngine;

namespace Feature.Toolbox.Application
{
    public interface ISpawnUseCase
    {
        Task TrySpawn(int id);
    }

}