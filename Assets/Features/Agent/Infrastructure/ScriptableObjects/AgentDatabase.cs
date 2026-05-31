#nullable enable
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Agent.Infrastructure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AgentDB", menuName = "Database/Agent")]
    public class AgentDatabase : ScriptableObject
    {
        [FormerlySerializedAs("_agents")] [SerializeField] private AgentData[]? agents;

        public AgentData? GetById(int id)
        {
            foreach (var s in agents!)
            {
                if (s.Id == id) return s;
            }

            return null;
        }
    }
}
