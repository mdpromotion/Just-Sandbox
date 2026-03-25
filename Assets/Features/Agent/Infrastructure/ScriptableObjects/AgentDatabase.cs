#nullable enable
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentDB", menuName = "Database/Agent")]
public class AgentDatabase : ScriptableObject
{
    [SerializeField] private AgentData[]? _agents;

    public AgentData? GetById(int id)
    {
        return _agents.FirstOrDefault(s => s.Id == id);
    }
}
