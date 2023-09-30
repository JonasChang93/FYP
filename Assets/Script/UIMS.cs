using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMS : MonoBehaviour
{
    public GameObject black;
    public GameObject btnGroup;
    //public Transform startBtn;
    //public Transform settingBtn;
    public GameObject dialog;

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
        //startBtn.GetComponent<Animator>().SetBool("isOnclick", true);
        //settingBtn.GetComponent<Animator>().SetBool("isOnclick", true);
        dialog.GetComponent<Animator>().SetBool("isOnclick", true);
    }
    public void CloseDialog()
    {
        btnGroup.GetComponent<Animator>().SetBool("isOnclick", false);
        //startBtn.GetComponent<Animator>().SetBool("isOnclick", false);
        //settingBtn.GetComponent<Animator>().SetBool("isOnclick", false);
        dialog.GetComponent<Animator>().SetBool("isOnclick", false);
    }
}
