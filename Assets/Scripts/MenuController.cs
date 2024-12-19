using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject background;
    [SerializeField] GameObject title;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject lvlselector;
    public void LoadLevel(int index)
    {
        GameController.instance.LoadLevel(index);
    }
    public void LoadCustom()
    {
        GameController.instance.LoadCustom();
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
