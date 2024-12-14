using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : MonoBehaviour
{
    [SerializeField] private Collider2D coll;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioClip canonBulletSoundClip;
    private bool isExploded = false;
    public Vector3 direction;
    public float speed;
    private void Start()
    {
        particle.Stop();
    }
    void Update()
    {
        if(GameController.instance.gameIsPaused()) return;
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
        transform.position += direction * speed * Time.deltaTime;
    }
    //Hits and explodes }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameController.instance.gameIsPaused()) return;
        if (collision.gameObject.layer == 6)
        {
            speed = 0;
            isExploded = true;
            particle.Play();
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(canonBulletSoundClip);
        }
        else if (collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<Player>().Dead();
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(canonBulletSoundClip);
        }
    }

}
