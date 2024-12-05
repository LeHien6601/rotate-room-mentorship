using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject exit;
    // Start is called before the first frame update
    void Start()
    {
        menu.GetComponent<Button>().onClick.AddListener(clickMenu);
        exit.GetComponent<Button>().onClick.AddListener(clickExit);
        settings.transform.localScale = Vector3.zero;
        Debug.Log("Add listener");
    }

    public void clickMenu()
    {
        Debug.Log("Menu");
        Time.timeScale = 0;
        settings.transform.DOScale(1f, 0.5f).SetUpdate(true);
    }

    public void clickExit()
    {
        Debug.Log("Exit");
        Time.timeScale = 1;
        settings.transform.DOScale(0f, 0.5f);
    }
}
