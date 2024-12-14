using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptables/Levels")]
public class Levels : ScriptableObject
{
    public Sprite image;
    public string text;
    public int sceneIndex;
}
