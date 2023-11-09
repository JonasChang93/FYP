using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ManagerStart : MonoBehaviour
{
    public GameObject black;
    public GameObject btnGroup;
    public GameObject settingDialogue;

    void Start()
    {
        StartCoroutine(BlackIn());
    }

    IEnumerator BlackIn()
    {
        black.SetActive(true);
        black.GetComponent<Animator>().Play("FadeIn");

        yield return new WaitForSeconds(1);

        black.SetActive(false);
    }

    public void NewGame()
    {
        black.SetActive(true);
        black.GetComponent<Animator>().Play("FadeOut");

        StartCoroutine(LoadScene());
    }

    public void LoadGame()
    {
        SaveLoadSettingManager.instance.loadGame = true;
        SaveLoadSettingManager.instance.SaveSetting();
        NewGame();
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Game");
    }

    public void OpenDialog()
    {
        btnGroup.GetComponent<Animator>().Play("MoveOut");
        settingDialogue.GetComponent<Animator>().Play("MoveIn");
    }
    public void CloseDialog()
    {
        btnGroup.GetComponent<Animator>().Play("MoveIn");
        settingDialogue.GetComponent<Animator>().Play("MoveOut");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
