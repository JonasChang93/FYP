using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMG : MonoBehaviour
{
    public GameObject black;
    public GameObject escapeDialogue;
    public Transform settingDialoggue;
    public Animator menuPanel;
    public Animator gear;
    public RectTransform inventoryPanel;

    void Start()
    {
        black.SetActive(true);
        black.GetComponent<Animator>().SetBool("blackIn", true);

        //Wait 1s to load scene
        StartCoroutine(TranS2());
    }

    IEnumerator TranS2()
    {
        yield return new WaitForSeconds(1);

        //It will turn back to animator start
        black.SetActive(false);
    }

    //ESC dialog
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeDialogue.SetActive(!escapeDialogue.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryPanel.pivot == new Vector2(0.5f, 0f))
            {
                inventoryPanel.pivot = new Vector2(0.5f, 0.5f);
                inventoryPanel.anchorMax = new Vector2(0.5f, 0.5f);
                inventoryPanel.anchorMin = new Vector2(0.5f, 0.5f);
            }
            else
            {
                inventoryPanel.pivot = new Vector2(0.5f, 0f);
                inventoryPanel.anchorMax = new Vector2(0.5f, 1f);
                inventoryPanel.anchorMin = new Vector2(0.5f, 1f);
            }
        }
    }

    //Restart button
    public void RestartGame()
    {
        black.SetActive(true);
        black.GetComponent<Animator>().SetBool("blackOut", true);

        StartCoroutine(TranS());
    }

    IEnumerator TranS()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Start");
    }

    //Open close setting menu
    public void OpenS()
    {
        settingDialoggue.GetComponent<Animator>().SetBool("isOnclick", true);
    }

    public void CloseS()
    {
        settingDialoggue.GetComponent<Animator>().SetBool("isOnclick", false);
    }

    //Quit button
    public void Quit()
    {
        Application.Quit();
    }

    //Silde up down panel 
    public void OpenCloseMenu()
    {
        //Use inverse value
        menuPanel.SetBool(("slideUp"), !menuPanel.GetBool("slideUp"));
        gear.SetBool(("slideUp"), !gear.GetBool("slideUp"));
    }
}
