using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Levels curr_levels;
    [SerializeField] Image insideImg;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadLevel);
        GetComponentInChildren<TMP_Text>().text = curr_levels.text;
        insideImg.sprite = curr_levels.image;
    }
    void LoadLevel()
    {
        SceneManager.LoadScene(curr_levels.sceneIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void setLevel(Levels in_level)
    {
        curr_levels = in_level;
    }
}
