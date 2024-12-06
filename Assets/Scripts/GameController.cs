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
        settings.transform.localScale = Vector3.zero;
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
