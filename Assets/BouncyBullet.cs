using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class BouncyBullet : PlayerBullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        if(collision.gameObject.CompareTag("Bullet")) return;

        // transform.rotation = Quaternion.AngleAxis(180, collision.transform.position) * transform.forward * -1;\
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // rb.AddForce(Quaternion.AngleAxis(180, collision.transform.position) * rb.velocity * -1 * rb.mass);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rb.velocity);
        Debug.DrawLine(Vector3.zero, hit.normal);
        // Debug.DrawRay(transform.position, rb.velocity);
        // Vector2 damn = new Vector3(rb.velocity.x, rb.velocity.y) - transform.position;
        // rb.AddForce(rb.velocity * - 1 * 50);
        // rb.AddForce(Quaternion.AngleAxis(180, collision.transform.position) * rb.velocity.normalized * -1 * 100);
        rb.velocity = Quaternion.AngleAxis(180, Vector3.forward) * rb.velocity * -1;
        // if(collision != null && !isExploded)
        // {
        //     // speed = 0;
        //     isExploded = true;
        //     particle.Play();
        // }
        // if (Input.GetMouseButton(0))
        // {
        //     RaycastHit hit;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         // Find the line from the gun to the point that was clicked.
        //         Vector3 incomingVec = hit.point - gunObj.position;

        //         // Use the point's normal to calculate the reflection vector.
        //         Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

        //         // Draw lines to show the incoming "beam" and the reflection.
        //         Debug.DrawLine(gunObj.position, hit.point, Color.red);
        //         Debug.DrawRay(hit.point, reflectVec, Color.green);
        //     }
        // }
    }
}
