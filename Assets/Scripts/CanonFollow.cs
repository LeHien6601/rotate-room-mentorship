using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class CanonFollow : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask obstacle;
    
    void Start()
    {
        gameObject.GetComponent<Canon>().canFire = false;
        player = GameObject.FindGameObjectWithTag("Player");
        SFXVolumeSetting.instance.AddAudioSource(GetComponent<AudioSource>());
        SFXVolumeSetting.instance.UpdateVolume();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCanFire();
    }

    private void CheckIfCanFire()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, player.transform.position - gameObject.transform.position, radius, obstacle);
        if (hit.collider == null)
        {
            gameObject.GetComponent<Canon>().canFire = false;
        }
        else if (hit.collider != null && hit.collider.gameObject.layer == 6)
        {
            if (hit.collider.gameObject.tag != "Bullet")
            {
                gameObject.GetComponent<Canon>().canFire = false;
            }           
        }
        else
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0, 0, angle);
            Vector3 rotateDirection = new Vector3(0, 0, angle);
            transform.DORotate(rotateDirection, 0.1f);
            gameObject.GetComponent<Canon>().canFire = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        DrawCircle(transform.position, radius);
    }

    void DrawCircle(Vector3 center, float radius)
    {
        int segments = 50;
        float angleStep = 360f / segments;
        Vector3 prevPos = center + new Vector3(radius, 0, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = angleStep * i;
            float x = center.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = center.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            Vector3 newPos = new Vector3(x, y, center.z);

            Gizmos.DrawLine(prevPos, newPos);
            prevPos = newPos;
        }
    }
}
