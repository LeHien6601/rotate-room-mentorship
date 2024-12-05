using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollowCamera : MonoBehaviour
{
    [SerializeField] public Camera cam;
    private float initialCamXPos;
    private float initialXPos;
    [SerializeField] float speed;
    private void Start()
    {
        initialCamXPos = cam.transform.position.x;
        initialXPos = transform.position.x;
    }
    private void Update()
    {
        transform.position = new Vector2(initialXPos + speed * (cam.transform.position.x - initialCamXPos), transform.position.y);
    }
}
