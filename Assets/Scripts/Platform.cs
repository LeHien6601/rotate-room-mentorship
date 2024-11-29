using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Collider2D coll;
    [SerializeField] private PlatformEffector2D effector;
    //Player left platform
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag == "Player")
        {
            coll.isTrigger = false;
            collision.gameObject.transform.SetParent(null);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.collider.tag == "Player")
        {
            BoxCollider2D collider = collision.collider.GetComponent<BoxCollider2D>();
            Player player = collision.collider.GetComponent<Player>();
            if (player.directions[0] == Vector2.up)
            {
                //if (collision.transform.position.y + collider.offset.y - collider.size.y / 2f >= transform.position.y)
                if (collision.transform.position.y >= transform.position.y)
                {
                    collision.gameObject.transform.SetParent(transform);
                }
            }
            else if (player.directions[0] == Vector2.down)
            {
                //if (collision.transform.position.y + collider.offset.y + collider.size.y / 2f <= transform.position.y)
                if (collision.transform.position.y <= transform.position.y)
                {
                    collision.gameObject.transform.SetParent(transform);
                }
            }
            else if (player.directions[0] == Vector2.left)
            {
                if (collision.transform.position.x + collider.offset.x + collider.size.x / 2f <= transform.position.x)
                    collision.gameObject.transform.SetParent(transform);
            }
            else if (player.directions[0] == Vector2.right)
            {
                if (collision.transform.position.x + collider.offset.x - collider.size.x / 2f >= transform.position.x)
                    collision.gameObject.transform.SetParent(transform);
            }
        }
    }

}
