using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public Text timeUI;
    public Transform car;
    float time;
    float lastSaveTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SaveGame()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create("./SaveDate.dat");
        SaveData saveData = new SaveData();
        saveData.time = time;
        saveData.carX = car.position.x;
        saveData.carX = car.position.y;
        saveData.carX = car.position.z;
        binaryFormatter.Serialize(file, saveData);
        file.Close();

        PlayerPrefs.Save();

        Debug.Log("Game saved!");
    }

    void LoadGame()
    {
        if (File.Exists("./SaveDate.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open("./SaveDate.dat", FileMode.Open);
            SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);
            time = saveData.time;
            lastSaveTime = time;

            car.position = new Vector3(0f, 0.66f, 0f);

            Debug.Log("No saved game! New game started");
        }
    }
}

    [Serializable]
    class SaveData
    {
        public float time;
        public float carX;
        public float carY;
        public float carZ;
    }
