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
    Dropdown resolutionDropdown;

    float bgmVolume;
    bool bgmMute;
    int dropdownIndex;

    public static SaveLoadManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audioToggle = GameObject.Find("Audio").GetComponentInChildren<Toggle>();
        audioSlider = GameObject.Find("Audio").GetComponentInChildren<Slider>();
        resolutionDropdown = GameObject.Find("Resolution").GetComponentInChildren<Dropdown>();
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

        bgmVolume = audioSource.volume;
        bgmMute = audioSource.mute;
        dropdownIndex = resolutionDropdown.value;

        saveData.bgmVolume = bgmVolume;
        saveData.bgmMute = bgmMute;
        saveData.dropdownIndex = dropdownIndex;

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

            bgmVolume = saveData.bgmVolume;
            bgmMute = saveData.bgmMute;
            dropdownIndex = saveData.dropdownIndex;

            audioSource.volume = bgmVolume;
            audioSource.mute = bgmMute;
            ResolutionDropdown(dropdownIndex);

            Debug.Log("Setting loaded!");
        }
        else
        {
            SaveSetting();

            Debug.Log("No setting! New setting Created");
        }
    }

    public void CancelSetting()
    {
        audioSlider.value = bgmVolume;
        audioToggle.isOn = bgmMute;
        resolutionDropdown.value = dropdownIndex;
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
        Debug.Log(screenWidth);
        Debug.Log(screenHeight);
    }
}

    [Serializable]
    class SaveData
    {
        public float bgmVolume;
        public bool bgmMute;
        public int dropdownIndex;
}
