using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FinishGate : MonoBehaviour
{
    [SerializeField] private float attraction = 1f;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float duration = 1f;
    private bool expand = true;

    private void Update()
    {
        if (timer > duration)
        {
            timer = duration;
            expand = false;
        }
        else if (timer < -duration)
        {
            timer = -duration;
            expand = true;
        }
        if (expand)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        transform.localScale = Vector3.one + Vector3.one * (timer + 1f) * 0.5f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.tag != "Player") return;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector3.zero;
        Vector3 direction = transform.position - collision.transform.position;
        if (direction.magnitude < circleCollider.radius / 2)
        {
            rb.MovePosition(transform.position);
            circleCollider.isTrigger = false;
            Time.timeScale = 1f;
            //Finish game!!!!!!
            GameController.instance.LoadNextLevel();
        }
        else
        {
            rb.velocity /= 2;
            rb.position += Vector2.ClampMagnitude(direction, attraction);
            Time.timeScale = 0.2f;
        }
    }
}
