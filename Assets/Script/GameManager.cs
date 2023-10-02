using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SceneCheck()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SaveLoadManager.instance.SaveSetting();
        }
    }
}
