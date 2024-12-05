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
    }

    private void clickMenu()
    {
        Time.timeScale = 0;
        settings.transform.DOScale(1f, 0.5f).SetUpdate(true);
    }

    private void clickExit()
    {
        Time.timeScale = 1;
        settings.transform.DOScale(0f, 0.5f);
    }
}
