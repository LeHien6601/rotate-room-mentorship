using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FakedBlock : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float duration;
    private float lifeTimer = 0f;
    private float timer = 0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D coll;
    [SerializeField] private Tilemap tilemap;
    private bool isFaded = false;
    private bool isCollapsed = false;
    private float initialAlpha = 0f;
    [SerializeField] private float maxShake = 0.05f;
    private List<float> randomShake;
    private int currentShake = 0;
    private Vector2 verticalDicrection = Vector2.zero;
    private Vector2 horizontalDicrection = Vector2.zero;
    private float initialX = 0f;

    private void Start()
    {
        initialAlpha = tilemap.color.a;
        initialX = transform.position.x;
        float sum = 0;
        randomShake = new List<float>();
        for (int i = 0; i < 10; i++)
        {
            float newElement = Random.Range(-maxShake, maxShake);
            sum += newElement;
            randomShake.Add(newElement);
        }
        randomShake.Add(-sum);
    }
    private void Update()
    {
        if (isCollapsed)
        {
            Shake();
            lifeTimer -= Time.deltaTime;
            if (lifeTimer <= 0)
            {
                Fade();
                lifeTimer = 0;
            }
        }
        else if (isFaded)
        {
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, tilemap.color.a / 1.05f);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Appear();
                timer = 0;
            }
        }
        else if (initialAlpha > tilemap.color.a)
        {
            float newAlpha = Mathf.Min(tilemap.color.a + 10, initialAlpha);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
        }
    }
    //Shaking effect when collapsing
    private void Shake()
    {
        transform.position += (Vector3)horizontalDicrection * randomShake[currentShake];
        transform.position += (Vector3)verticalDicrection * randomShake[currentShake] * 0.1f;
        currentShake = (currentShake + 1) % randomShake.Count;
    }
    //Seting when fading
    private void Fade()
    {
        isCollapsed = false;
        isFaded = true;
        coll.enabled = false;
        timer = duration;
        currentShake = 0;
    }
    //Seting when appearing
    private void Appear()
    {
        transform.position = new Vector3(initialX, transform.position.y);
        rb.bodyType = RigidbodyType2D.Kinematic;
        isFaded = false;
        coll.enabled = true;
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 50);
    }
    //Collapsed trigger
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            verticalDicrection = player.directions[0];
            horizontalDicrection = player.directions[1];
        }
        if (isCollapsed) return;
        isCollapsed = true;
        lifeTimer = lifeTime;
    }
}
