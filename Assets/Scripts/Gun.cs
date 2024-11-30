using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float power;
    public string GunStyle {get; protected set;}
    public abstract void Shoot(Vector3 direction);
    void Update()
    {
        if(transform.parent != null)
        {
            return;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player")) return;

        // if(collision.gameObject.GetComponent<Player>().hasgun) return;

        transform.position = collision.gameObject.transform.position;
        transform.position += Vector3.up * 0.5f;
        transform.parent = collision.gameObject.transform;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        collision.GetComponent<Player>().setGun(this);
    }
}
