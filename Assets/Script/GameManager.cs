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
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SaveLoadManager.instance.SaveSetting();
        }
        else
        {
            SaveLoadManager.instance.LoadSetting();
            SaveLoadManager.instance.CancelSetting();
        }
    }
}
