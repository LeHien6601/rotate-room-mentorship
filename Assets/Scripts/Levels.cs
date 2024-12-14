
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptables/Levels")]
public class Levels : ScriptableObject
{
    public Sprite image;
    public string text;
    public int sceneIndex;
}
