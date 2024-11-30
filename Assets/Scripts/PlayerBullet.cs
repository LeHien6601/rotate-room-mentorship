using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Collider2D coll;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected ParticleSystem particle;
    protected bool isExploded = false;
    // public Vector3 direction;
    // public float speed;
    private void Start()
    {
        particle.Stop();
    }
    void Update()
    {
        if (isExploded)
        {
            if (particle.isStopped)
            {
                Destroy(gameObject);
            }
            coll.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a / 1.1f);
        }
    }

    private void FixedUpdate()
    {
        // transform.position += direction * speed * Time.deltaTime; // replaced by addforce, Dang
    }
    //Hits and explodes }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet")) return;
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {        
            if(collision != null && !isExploded)
            {
                // speed = 0;
                isExploded = true;
                particle.Play();
            }
        }
    }

}
