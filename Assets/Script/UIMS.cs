using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMS : MonoBehaviour
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

        black.GetComponent<Animator>().SetBool("black", true);

        yield return new WaitForSeconds(1);

        black.SetActive(false);
    }

    public void StartGame()
    {
        black.SetActive(true);

        black.GetComponent<Animator>().SetBool("black", false);

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Game");
    }

    public void OpenDialog()
    {
        btnGroup.GetComponent<Animator>().SetBool("isOnclick", true);
        settingDialogue.GetComponent<Animator>().SetBool("isOnclick", true);
    }
    public void CloseDialog()
    {
        btnGroup.GetComponent<Animator>().SetBool("isOnclick", false);
        settingDialogue.GetComponent<Animator>().SetBool("isOnclick", false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
