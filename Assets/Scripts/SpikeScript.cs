using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    //Kills player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Dead();
        }
    }
}
