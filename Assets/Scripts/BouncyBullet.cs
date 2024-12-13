using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncyBullet : PlayerBullet
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null && !isExploded)
        {
            // speed = 0;
            Debug.Log("hit cannons");
            if(collision.gameObject.CompareTag("Cannons")) Destroy(collision.gameObject);
            isExploded = true;
            particle.Play();
            audioSource.PlayOneShot(soundEffect);
        }
    }
}
