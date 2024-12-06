using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject exit;
    public GameObject Menu{
        get{return menu;} set{}
    }
    public GameObject Settings{
        get{return settings;} set{}
    }
    public GameObject Exit{
        get{return exit;} set{}
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
