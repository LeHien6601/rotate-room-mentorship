using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject background;
    [SerializeField] GameObject title;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject lvlselector;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void SelectLevel()
    {
        background.SetActive(false);
        title.SetActive(false);
        buttons.SetActive(false);
        lvlselector.SetActive(true);
    }
    public void Reset()
    {
        background.SetActive(true);
        title.SetActive(true);
        buttons.SetActive(true);
        lvlselector.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
