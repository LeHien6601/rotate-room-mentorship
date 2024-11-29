using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGate : MonoBehaviour
{
    [SerializeField] private float attraction = 1f;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private Transform whiteGate;
    private Vector2 initialVelocity;
    private float initialAlpha;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        //if (collision.tag != "Player") return;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        initialVelocity = rb.velocity;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null) return;
        //if (collision.tag != "Player") return;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        SpriteRenderer sprite = collision.GetComponent<SpriteRenderer>();
        Vector3 direction = transform.position - collision.transform.position;

        
        //Teleports to white gate!!!!!!
        if (collision.tag == "Player")
        {
            collision.transform.position = whiteGate.position;
            rb.bodyType = RigidbodyType2D.Dynamic;
            float angle = Vector2.SignedAngle(transform.right, whiteGate.right) * Mathf.Deg2Rad;
            rb.velocity = new Vector2(initialVelocity.x * Mathf.Cos(angle) - initialVelocity.y * Mathf.Sin(angle),
                                        initialVelocity.x * Mathf.Sin(angle) + initialVelocity.y * Mathf.Cos(angle));
        }
        
        else
        {

            {
                //Attracts to black gate!!!!!
                rb.velocity /= 2;
                collision.transform.position += Vector3.ClampMagnitude(direction, attraction);
                collision.transform.position = whiteGate.position;
            }    
        }
    }
}
