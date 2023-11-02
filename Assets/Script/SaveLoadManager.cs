using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    AudioSource audioSource;
    Toggle audioToggle;
    Slider audioSlider;
    Slider mouseSlider;
    Dropdown resolutionDropdown;
    Toggle offsetX_toggle;
    Toggle offsetY_toggle;

    float bgmVolume;
    bool bgmMute;
    int dropdownIndex;
    float mouseSliderFloat = 1;

    [HideInInspector] public float offsetX = 1;
    [HideInInspector] public float offsetY = 1;
    [HideInInspector] public float rotatingSpeed = 10;

    public static SaveLoadManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("MainCamera").GetComponent<AudioSource>();
        audioToggle = GameObject.Find("Audio").GetComponentInChildren<Toggle>();
        audioSlider = GameObject.Find("Audio").GetComponentInChildren<Slider>();
        resolutionDropdown = GameObject.Find("Resolution").GetComponentInChildren<Dropdown>();
        mouseSlider = GameObject.Find("MouseSlider").GetComponentInChildren<Slider>();
        offsetX_toggle = GameObject.Find("OffsetX").GetComponentInChildren<Toggle>();
        offsetY_toggle = GameObject.Find("OffsetY").GetComponentInChildren<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveSetting()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create("./SavedSetting.dat");
        SaveData saveData = new SaveData();

        //Save in component
        bgmVolume = audioSource.volume;
        bgmMute = audioSource.mute;

        saveData.bgmVolume = bgmVolume;
        saveData.bgmMute = bgmMute;

        //Save in script
        saveData.offsetX = offsetX;
        saveData.offsetY = offsetY;
        saveData.dropdownIndex = dropdownIndex;
        saveData.mouseSliderFloat = mouseSliderFloat;

        binaryFormatter.Serialize(file, saveData);
        file.Close();

        PlayerPrefs.Save();

        Debug.Log("Setting saved!");
    }

    public void LoadSetting()
    {
        if (File.Exists("./SavedSetting.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open("./SavedSetting.dat", FileMode.Open);
            SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);

            //Load to script
            bgmVolume = saveData.bgmVolume;
            bgmMute = saveData.bgmMute;
            offsetX = saveData.offsetX;
            offsetY = saveData.offsetY;
            dropdownIndex = saveData.dropdownIndex;
            mouseSliderFloat = saveData.mouseSliderFloat;

            file.Close();
            CancelSetting();

            Debug.Log("Setting loaded!");
        }
        else
        {
            SaveSetting();
            ResolutionDropdown(dropdownIndex);
            Debug.Log("No setting! New setting Created");
        }
    }

    public void CancelSetting()
    {
        audioSlider.value = bgmVolume;
        audioToggle.isOn = bgmMute;
        resolutionDropdown.value = dropdownIndex;
        mouseSlider.value = mouseSliderFloat;
        offsetX_toggle.isOn = offsetX > 0 ? false : true;
        offsetY_toggle.isOn = offsetY > 0 ? false : true;
    }

    public void CheckOrNotOffsetY(bool isOn)
    {
        offsetY = isOn ? -1 : 1;
    }

    public void CheckOrNotOffsetX(bool isOn)
    {
        offsetX = isOn ? -1 : 1;
    }

    public void MouseSensitivity(float value)
    {
        mouseSliderFloat = value;
        rotatingSpeed = 10 * mouseSliderFloat;
    }

    public void DropdownIndex(int option)
    {
        dropdownIndex = option;

        ResolutionDropdown(option);
    }

    public void ResolutionDropdown(int option)
    {
        int screenWidth;
        int screenHeight;

        switch (option)
        {
            case 0:
                screenWidth = 1920;
                screenHeight = 1080;
                break;
            case 1:
                screenWidth = 2560;
                screenHeight = 1440;
                break;
            case 2:
                screenWidth = 1280;
                screenHeight = 720;
                break;
            default:
                screenWidth = 1920;
                screenHeight = 1080;
                break;
        }
        
        Screen.SetResolution(screenWidth, screenHeight, false);
    }
}

[Serializable]
class SaveData
{
    public float bgmVolume;
    public bool bgmMute;
    public int dropdownIndex;
    public float offsetX;
    public float offsetY;
    public float mouseSliderFloat;
}
