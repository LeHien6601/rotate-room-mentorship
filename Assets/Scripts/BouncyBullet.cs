using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncyBullet : PlayerBullet
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null && !isExploded && collision.gameObject.CompareTag("Cannons"))
        {
            // speed = 0;
            Destroy(collision.gameObject);
            isExploded = true;
            particle.Play();
        }
    }
}
