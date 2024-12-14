﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject winscreen;
    [SerializeField] private int maxLevel;
    [SerializeField] private int currentLevel = 0;
    private bool isLose = false;
    [SerializeField] private UI ui_script;
    [SerializeField] private GameObject player;
    public uint KeysNeededToContinue = 5;
    [SerializeField] private uint KeysCollected = 0;
    public GameObject finishGate{get; private set;}
    [SerializeField] float timeElapsed = 0f;
    public static GameController instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip winSoundClip;
    [SerializeField] private AudioClip loseSoundClip;    
    bool isPaused = false;
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
        getOjects();
        timeElapsed = 0;
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponentInChildren<SpriteRenderer>().color = Resources.Load<Colors>("CustomCharacter/Colors/" + PlayerPrefs.GetString("Color")).mainColor;
        
        SceneManager.sceneLoaded += onNewSceneLoad;
        if(SceneManager.GetActiveScene().buildIndex == 4) KeysNeededToContinue = 5;
        else KeysNeededToContinue = 0;
    }
    void FixedUpdate()
    {
        timeElapsed += 0.2f;
    }
    void CheckGate()
    {
        if(!finishGate) getOjects();
        if(KeysCollected != KeysNeededToContinue) 
        {
            finishGate.SetActive(false);
        }
        else
        {
            finishGate.SetActive(true);
        }
    }
    void SubscribeToUI()
    {
        menu.GetComponent<Button>().onClick.AddListener(clickMenu);
        exit.GetComponent<Button>().onClick.AddListener(clickExit);
        restart.GetComponent<Button>().onClick.AddListener(clickRestart);
        settings.transform.localScale = Vector3.zero;
        lose.transform.localScale = Vector3.zero;
        winscreen.transform.localScale = Vector3.zero;
        Debug.Log("Add listener");
    }
    private void Update()
    {
        if(currentLevel == 3) KeysNeededToContinue = 5;
        CheckGate();
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
        if(!lose) getOjects();
        isLose = true;
        Debug.Log("lose");
        lose.transform.DOScale(1f, 0.5f).SetUpdate(true);
        audioSource.ignoreListenerPause = true;
        audioSource.PlayOneShot(loseSoundClip);
        player.GetComponentInChildren<SpriteRenderer>().color = Resources.Load<Colors>("CustomCharacter/Colors/" + PlayerPrefs.GetString("Color")).mainColor;
    }
    public void clickMenu()
    {
        if(!settings) getOjects();
        // Debug.Log("Menu");
        Time.timeScale = 0;
        settings.transform.DOScale(1f, 0.5f).SetUpdate(true);
    }
    public void clickExit()
    {
        if(!settings) getOjects();
        // Debug.Log("Exit");
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
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        currentLevel = (currentLevel + 1) % maxLevel;
        SFXVolumeSetting.instance.ClearAudioSource();
        SceneManager.LoadScene(currentLevel);
        timeElapsed = 0f;
    }
    void onNewSceneLoad(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<SpriteRenderer>().color = Resources.Load<Colors>("CustomCharacter/Colors/" + PlayerPrefs.GetString("Color")).mainColor;
        getOjects();
        timeElapsed = 0f;
        KeysCollected = 0;
        isPaused = false;
        if(KeysNeededToContinue != 0) finishGate.SetActive(false);
        SubscribeToUI();
        if(SceneManager.GetActiveScene().buildIndex == 4) KeysNeededToContinue = 5;
        else KeysNeededToContinue = 0;
    }
    public void LoadLevel(int level)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
        KeysCollected = 0;
    }
    private void ReloadCurrentLevel()
    {
        Time.timeScale = 1f;
        isLose = false;
        SFXVolumeSetting.instance.ClearAudioSource();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
        timeElapsed = 0f;
    }
    public void FinishGame(int levelToLoad)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(!player) return;

        SpriteRenderer spr = player.GetComponent<SpriteRenderer>();
        spr.DOFade(0, 0.1f);  
        StartCoroutine(LoadAfterWait(0.1f, levelToLoad));
    }
    public void AddKeys()
    {
        KeysCollected++;
        CheckGate();   
    }
    IEnumerator LoadAfterWait(float seconds, int level)
    {
        yield return new WaitForSeconds(seconds);
        LoadLevel(level);
        yield return null;
    }
    void getOjects()
    {
        ui_script = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        finishGate = GameObject.FindGameObjectWithTag("Finish");
        menu = ui_script.Menu;
        settings = ui_script.Settings;
        exit = ui_script.Exit;
        winscreen = ui_script.WinScreen;
        // Debug.Log("Add listener");
        SubscribeToUI();
        CheckGate();
    }
    public void ExitToWindows()
    {
        UnityEngine.Application.Quit();
    }
    public void WinScreen()
    {
        isPaused = true;
        audioSource.PlayOneShot(winSoundClip);
        winscreen.transform.DOScale(1f, 0.5f);
        string[] victorytext = {"You win!", "Great Jumps!", "Good shot!"};
        TMP_Text[] arr = winscreen.GetComponentsInChildren<TMP_Text>();
        // TMP_Text win_text;
        foreach(TMP_Text text in arr)
        {
            Debug.Log(text.name);
            if(text.gameObject.name == "You Win")
            {
                text.text = victorytext[Random.Range(0, victorytext.Length)];
            }

            if(text.name == "Text")
            {
                string ans = "Stars: " + KeysCollected;
                ans += '\n';
                ans += "Time: " + (int)timeElapsed;
                text.text = ans;
            }
        }
    }
    public bool gameIsPaused()
    {
        if(isLose || isPaused) return true;
        
        FollowPlayer followPlayer = Camera.main.gameObject.GetComponent<FollowPlayer>();

        return !followPlayer.playing;
    }
}
