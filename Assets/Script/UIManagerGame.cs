using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour
{
    public GameObject black;
    public GameObject escapeDialogue;
    public RectTransform settingDialogueRT;

    Animator blackAnimator;

    bool dialogueOnOff = false;

    void Start()
    {
        blackAnimator = black.GetComponent<Animator>();

        StartCoroutine(BlackIn());
    }

    IEnumerator BlackIn()
    {
        black.SetActive(true);
        blackAnimator.Play("FadeIn");

        yield return new WaitForSeconds(1);

        black.SetActive(false);
    }

    //ESC dialog
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !dialogueOnOff)
        {
            if (escapeDialogue.activeSelf)
            {
                Time.timeScale = 1;

                escapeDialogue.SetActive(false);
                black.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;

                escapeDialogue.SetActive(true);
                black.SetActive(true);
                blackAnimator.Play("FadeOut", -1, 0.5f);
            }
        }
    }

    public void LoadGame()
    {
        Time.timeScale = 1;
        black.SetActive(true);
        black.GetComponent<Animator>().Play("FadeOut");
        SaveLoadSettingManager.instance.loadGame = true;

        StartCoroutine(LoadScene());
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);

        if (SaveLoadSettingManager.instance.loadGame)
        {
            SaveLoadSettingManager.instance.SaveSetting();
            SceneManager.LoadScene("Game");
        }
        else
        {
            SaveLoadSettingManager.instance.SaveSetting();
            SceneManager.LoadScene("Start");
        }
    }

        //Open close setting menu
        public void OpenDialog()
    {
        dialogueOnOff = true;

        settingDialogueRT.pivot = new Vector2(0.5f, 0.5f);
        settingDialogueRT.anchorMax = new Vector2(0.5f, settingDialogueRT.anchorMax.y);
        settingDialogueRT.anchorMin = new Vector2(0.5f, settingDialogueRT.anchorMin.y);
        settingDialogueRT.anchoredPosition = new Vector2(0, 0);
    }

    public void CloseDialog()
    {
        dialogueOnOff = false;

        settingDialogueRT.pivot = new Vector2(0, 0.5f);
        settingDialogueRT.anchorMax = new Vector2(1, settingDialogueRT.anchorMax.y);
        settingDialogueRT.anchorMin = new Vector2(1, settingDialogueRT.anchorMin.y);
        settingDialogueRT.anchoredPosition = new Vector2(0, 0);
    }

    //Quit button
    public void Quit()
    {
        Application.Quit();
    }
}
