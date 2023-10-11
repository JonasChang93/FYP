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

    float bgmVolume;
    bool bgmMute;

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

        saveData.bgmVolume = bgmVolume;
        saveData.bgmMute = bgmMute;

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

            audioSource.volume = bgmVolume;
            audioSource.mute = bgmMute;

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
    }
}

    [Serializable]
    class SaveData
    {
        public float bgmVolume;
        public bool bgmMute;
    }
