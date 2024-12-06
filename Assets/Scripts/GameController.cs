using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject exit;
    [SerializeField] UI ui_script;
    [SerializeField] private GameObject player;
    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        menu.GetComponent<Button>().onClick.AddListener(clickMenu);
        exit.GetComponent<Button>().onClick.AddListener(clickExit);
        settings.transform.localScale = Vector3.zero;
        // Debug.Log("Add listener");
        ui_script = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();

        menu = ui_script.Menu;
        settings = ui_script.Settings;
        exit = ui_script.Exit;
    }
    public void clickMenu()
    {
        // Debug.Log("Menu");
        Time.timeScale = 0;
        settings.transform.DOScale(1f, 0.5f).SetUpdate(true);
    }
    public void clickExit()
    {
        // Debug.Log("Exit");
        Time.timeScale = 1;
        settings.transform.DOScale(0f, 0.5f);
    }
    public void LoadLevel(int i)
    {
        // Debug.Log("Access level " + i);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Demo " + i);
    }
    public void FinishGame(int levelToLoad)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(!player) return;

        SpriteRenderer spr = player.GetComponent<SpriteRenderer>();
        spr.DOFade(0, 0.1f);  
        StartCoroutine(LoadAfterWait(0.1f, levelToLoad));
    }
    IEnumerator LoadAfterWait(float seconds, int level)
    {
        yield return new WaitForSeconds(seconds);
        LoadLevel(level);
        yield return null;
    }
}
