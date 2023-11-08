using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SceneEvent()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                SaveLoadManager.instance.LoadSetting();
                break;
            case 1:
                SaveLoadManager.instance.LoadSetting();
                if (SaveLoadManager.instance.loadGame)
                {

                }
                break;
            default:
                SaveLoadManager.instance.LoadSetting();
                break;
        }
    }
}
