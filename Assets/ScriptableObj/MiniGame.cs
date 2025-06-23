using UnityEngine;

[CreateAssetMenu(fileName = "MiniGame", menuName = "Scriptable Objects/MiniGame")]
public class MiniGame : ScriptableObject
{
    public string SceneName;
    public int coolDown;
    public int currentDownTime;
}
