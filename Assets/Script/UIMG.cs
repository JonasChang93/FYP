using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMG : MonoBehaviour
{
    public GameObject black;
    public GameObject escapeDialogue;
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

    //ESC dialog
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeDialogue.SetActive(!escapeDialogue.activeSelf);
        }
    }

    //Restart button
    public void RestartGame()
    {
        black.SetActive(true);

        black.GetComponent<Animator>().SetBool("black", false);

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Start");
    }

    //Open close setting menu
    public void OpenDialog()
    {
        settingDialogue.GetComponent<Animator>().SetBool("isOnclick", true);
    }

    public void CloseDialog()
    {
        settingDialogue.GetComponent<Animator>().SetBool("isOnclick", false);
    }

    //Quit button
    public void Quit()
    {
        Application.Quit();
    }
}
