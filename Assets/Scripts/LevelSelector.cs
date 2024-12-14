using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject levelobject;
    [SerializeField] List<Levels> levels;
    void Start()
    {
        levels = Resources.LoadAll<Levels>("Levels").ToList();
        foreach(Levels level in levels)
        {
            Debug.Log(level.name);
            Instantiate(levelobject, transform).GetComponent<LevelUI>().setLevel(level);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
