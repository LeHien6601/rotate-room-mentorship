using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Callbacks;
using UnityEngine;

public class FinishGate : MonoBehaviour
{
    [SerializeField] private float attraction = 1f;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float duration = 1f;
    [SerializeField] int levelToLoad;
    private bool expand = true;
    private Player player;
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
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision == null) return;
    //     if(collision.tag != "Player") return;

    //     Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
    //     rb.bodyType = RigidbodyType2D.Kinematic;
    //     rb.velocity = Vector3.zero;
    //     player = collision.gameObject.GetComponent<Player>();
    //     player.GetAnimator().SetTrigger("Win");
    //     StartCoroutine(finish());
    // }
    // IEnumerator finish()
    // {
    //     float speed = 15f;
    //     Vector2 direction = transform.position - player.transform.position;
    //     for(int i = 0; i < 15; ++i)
    //     {
    //         player.gameObject.GetComponent<Rigidbody>().position += Vector3.ClampMagnitude(direction, speed);
    //         yield return new WaitForSeconds(0.1f);
    //     }
    // }
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
            rb.velocity = Vector2.zero;
            rb.position += Vector2.ClampMagnitude(direction, attraction);
            Time.timeScale = 1f;
        }
    }
}
