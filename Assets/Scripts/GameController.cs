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
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject restart;
    [SerializeField] private int maxLevel;
    private int currentLevel = 0;
    private bool isLose = false;
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
        restart.GetComponent<Button>().onClick.AddListener(clickRestart);
        settings.transform.localScale = Vector3.zero;
        lose.transform.localScale = Vector3.zero;
        Debug.Log("Add listener");
    }
    private void Update()
    {
        if (isLose)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                clickRestart();
            }
        }
    }
    public void loseGame()
    {
        isLose = true;
        Debug.Log("lose");
        lose.transform.DOScale(1f, 0.5f).SetUpdate(true);
    }
    private void clickMenu()
    {
        Debug.Log("Menu");
        Time.timeScale = 0;
        settings.transform.DOScale(1f, 0.5f).SetUpdate(true);
    }

    private void clickExit()
    {
        Debug.Log("Exit");
        Time.timeScale = 1;
        settings.transform.DOScale(0f, 0.5f);
    }
    private void clickRestart()
    {
        Time.timeScale = 1f;
        lose.transform.DOScale(0f, 0.5f);
        ReloadCurrentLevel();
    }
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        currentLevel = (currentLevel + 1) % maxLevel;
        SFXVolumeSetting.instance.ClearAudioSource();
        SceneManager.LoadScene(currentLevel);
    }
    private void ReloadCurrentLevel()
    {
        Time.timeScale = 1f;
        isLose = false;
        SFXVolumeSetting.instance.ClearAudioSource();
        SceneManager.LoadScene(currentLevel);
    }
}
