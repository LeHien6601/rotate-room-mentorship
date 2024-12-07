using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCount : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;
    [SerializeField] TMP_Text text;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if(!player) return;
        if(!player.gameObject.GetComponentInChildren<Gun>()) return;

        string str = "Bullets: " + player.gameObject.GetComponentInChildren<Gun>().bulletCount;
        str += "\nGun: ";
        str += player.gameObject.GetComponentInChildren<Gun>().GunStyle;
        text.text = str;
    }
}
