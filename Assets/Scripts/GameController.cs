using System.Collections;
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
    [SerializeField] private GameObject mainMenu;
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
        if (PlayerPrefs.HasKey("Color") && PlayerPrefs.HasKey("Alpha"))
        {
            Debug.Log(currentLevel + " " + maxLevel);
            string colorHex = PlayerPrefs.GetString("Color");
            Color color;
            ColorUtility.TryParseHtmlString("#"+colorHex, out color);
            Debug.Log(color);
            player.GetComponentInChildren<SpriteRenderer>().color = new Color(color.r, color.g, color.b, PlayerPrefs.GetFloat("Alpha"));
        }
        else
        {
            player.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        //player.GetComponentInChildren<SpriteRenderer>().color = Resources.Load<Colors>("CustomCharacter/Colors/" + PlayerPrefs.GetString("Color")).mainColor;

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
            Debug.Log("Set unactive");
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
        mainMenu.GetComponent<Button>().onClick.AddListener(clickMainMenu);
        restart.GetComponent<Button>().onClick.AddListener(clickRestart);
        settings.transform.localScale = Vector3.zero;
        lose.transform.localScale = Vector3.zero;
        winscreen.transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        //if(currentLevel == 3) KeysNeededToContinue = 5;
        if (currentLevel == 0) return;
        //CheckGate();
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
        audioSource.ignoreListenerPause = true;
        audioSource.PlayOneShot(loseSoundClip);
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
    private void clickRestart()
    {
        Time.timeScale = 1f;
        lose.transform.DOScale(0f, 0.5f);
        ReloadCurrentLevel();
    }
    private void clickMainMenu()
    {
        Time.timeScale = 1f;
        settings.transform.DOScale(0f, 0.5f);
        LoadLevel(0);
    }
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        winscreen.transform.DOScale(0f, 0.5f);
        currentLevel = (currentLevel + 1) % maxLevel;
        SFXVolumeSetting.instance.ClearAudioSource();
        SceneManager.LoadScene(currentLevel);
        timeElapsed = 0f;
    }
    void onNewSceneLoad(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.HasKey("Color") && PlayerPrefs.HasKey("Alpha"))
        {
            Debug.Log(currentLevel + " " + maxLevel);
            string colorHex = PlayerPrefs.GetString("Color");
            Debug.Log(colorHex);
            Color color;
            if (ColorUtility.TryParseHtmlString("#"+colorHex, out color))
            {
                Debug.Log(color);
                if (currentLevel != maxLevel)
                {
                    player.GetComponentInChildren<SpriteRenderer>().color = new Color(color.r, color.g, color.b, PlayerPrefs.GetFloat("Alpha"));
                }
                else
                {
                    player.GetComponent<Image>().color = new Color(color.r, color.g, color.b, PlayerPrefs.GetFloat("Alpha"));
                }
            }
        }
        else
        {
            Debug.Log("else " + currentLevel + " " + maxLevel);
            if (currentLevel != maxLevel)
            {
                player.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            }
            else
            {
                player.GetComponent<Image>().color = Color.gray;
            }
        }
        //player.GetComponentInChildren<SpriteRenderer>().color = Resources.Load<Colors>("CustomCharacter/Colors/" + PlayerPrefs.GetString("Color")).mainColor;
        //getOjects();
        timeElapsed = 0f;
        KeysCollected = 0;
        isPaused = false;
        if(KeysNeededToContinue != 0) finishGate.SetActive(false);
        //SubscribeToUI();
        if(SceneManager.GetActiveScene().buildIndex == 4) KeysNeededToContinue = 5;
        else KeysNeededToContinue = 0;
    }
    public void LoadLevel(int level)
    {
        Time.timeScale = 1f;
        currentLevel = level;
        SceneManager.LoadScene(level);
        KeysCollected = 0;
    }
    public void LoadCustom()
    {
        LoadLevel(maxLevel);
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
        SubscribeToUI();
        //CheckGate();
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
                //string ans = "Stars: " + KeysCollected;
                //ans += '\n';
                string ans = "Time: " + (int)timeElapsed;
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
