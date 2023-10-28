using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMG : MonoBehaviour
{
    public GameObject black;
    public GameObject escapeDialogue;
    public GameObject settingDialogue;

    Animator blackAnimator;
    RectTransform settingDialogueRectTransform;

    bool settingDialogueOnOff = false;
    bool timerOnOff = false;
    float timer;

    void Start()
    {
        blackAnimator = black.GetComponent<Animator>();
        settingDialogueRectTransform = settingDialogue.GetComponent<RectTransform>();

        StartCoroutine(BlackIn());
    }

    IEnumerator BlackIn()
    {
        black.SetActive(true);

        blackAnimator.SetBool("black", true);

        yield return new WaitForSeconds(1);

        black.SetActive(false);
    }

    //ESC dialog
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !settingDialogueOnOff)
        {
            if (escapeDialogue.activeSelf)
            {
                Time.timeScale = 1;
                escapeDialogue.SetActive(false);
                black.SetActive(false);
                timerOnOff = false;
                timer = 0;
            }
            else
            {
                Time.timeScale = 0;
                escapeDialogue.SetActive(true);
                black.SetActive(true);
                timerOnOff = true;
            }
        }

        if (timerOnOff)
        {
            BlackFadeOut();
        }
    }

    void BlackFadeOut()
    {
        timer += 0.001f;
        blackAnimator.Play("FadeOut", -1, timer);
    }

    //Restart button
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    //Open close setting menu
    public void OpenDialog()
    {
        settingDialogueOnOff = true;
        settingDialogueRectTransform.pivot = new Vector2(0.5f, 0.5f);
        settingDialogueRectTransform.anchorMax = new Vector2(0.5f, settingDialogueRectTransform.anchorMax.y);
        settingDialogueRectTransform.anchorMin = new Vector2(0.5f, settingDialogueRectTransform.anchorMin.y);
        settingDialogueRectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void CloseDialog()
    {
        settingDialogueOnOff = false;
        settingDialogueRectTransform.pivot = new Vector2(0, 0.5f);
        settingDialogueRectTransform.anchorMax = new Vector2(1, settingDialogueRectTransform.anchorMax.y);
        settingDialogueRectTransform.anchorMin = new Vector2(1, settingDialogueRectTransform.anchorMin.y);
        settingDialogueRectTransform.anchoredPosition = new Vector2(0, 0);
    }

    //Quit button
    public void Quit()
    {
        Application.Quit();
    }
}
