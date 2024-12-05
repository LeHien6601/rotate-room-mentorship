using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCombo : MonoBehaviour
{
    [SerializeField] public Camera cam;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            foreach (BackgroundFollowCamera childOfChild in child.GetComponentsInChildren<BackgroundFollowCamera>())
            {
                childOfChild.cam = cam;
            }
        }
    }
}
