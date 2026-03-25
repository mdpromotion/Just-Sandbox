using UnityEngine;

[CreateAssetMenu(fileName = "SceneConfig", menuName = "Configs/SceneConfig")]
public class SceneConfig : ScriptableObject
{
    public bool enableCamera = true;
    public bool enableUI = true;
}
