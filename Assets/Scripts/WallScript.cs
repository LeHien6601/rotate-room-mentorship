using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    [SerializeField] private Collider2D coll;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSystem particle1;
    [SerializeField] private ParticleSystem particle2;
    private bool isExploded = false;
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        particle.Stop();
        particle1.Stop();
        particle2.Stop();
    }
    void Update()
    {
        if (player.directions[2] == Vector2.down)
        {
            particle.transform.rotation = Quaternion.Euler(0, 0, 0);
            particle1.transform.rotation = Quaternion.Euler(0, 0, 0);
            particle2.transform.rotation = Quaternion.Euler(0, 0, 0);
        }   
        else if (player.directions[2] == Vector2.up)
        {
            particle.transform.rotation = Quaternion.Euler(0, 0, 180);
            particle1.transform.rotation = Quaternion.Euler(0, 0, 180);
            particle2.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (player.directions[2] == Vector2.right)
        {
            particle.transform.rotation = Quaternion.Euler(0, 0, 90);
            particle1.transform.rotation = Quaternion.Euler(0, 0, 90);
            particle2.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (player.directions[2] == Vector2.left)
        {
            particle.transform.rotation = Quaternion.Euler(0, 0, -90);
            particle1.transform.rotation = Quaternion.Euler(0, 0, -90);
            particle2.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if (isExploded)
        {
            if (particle.isStopped)
            {
                Destroy(gameObject);
            }
            coll.enabled = false;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a / 1.1f);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            isExploded = true;
            particle.Play();
            particle1.Play();
            particle2.Play();
        }
    }
}
