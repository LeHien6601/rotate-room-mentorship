using System.Collections;
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
    [SerializeField] private UI ui_script;
    [SerializeField] private GameObject player;
    [SerializeField] private uint KeysNeededToContinue = 0;
    [SerializeField] private GameObject finishGate;
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
        ui_script = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        finishGate = GameObject.FindGameObjectWithTag("Finish");
        menu = ui_script.Menu;
        settings = ui_script.Settings;
        exit = ui_script.Exit;
        // Debug.Log("Add listener");
        SubscribeToUI();
        CheckGate();
    }
    void CheckGate()
    {
        if(KeysNeededToContinue != 0) 
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
    public void FinishGame(int levelToLoad)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(!player) return;

        SpriteRenderer spr = player.GetComponent<SpriteRenderer>();
        spr.DOFade(0, 0.1f);  
        StartCoroutine(LoadAfterWait(0.1f, levelToLoad));
    }
    public void ReduceKeys()
    {
        KeysNeededToContinue--;     
        CheckGate();   
    }
    IEnumerator LoadAfterWait(float seconds, int level)
    {
        yield return new WaitForSeconds(seconds);
        LoadLevel(level);
        yield return null;
    }
}
