using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadGameManager : MonoBehaviour
{
    Transform playerTransform;
    CharacterController characterController;
    Transform modelRotate;
    Transform cameraRotateY;
    Transform cameraRotateZ;

    public static SaveLoadGameManager instance;
    private void Awake()
    {
        instance = this;
        GetAllComponent();
    }

    void GetAllComponent()
    {
        playerTransform = GameObject.Find("Player2").GetComponent<Transform>();
        characterController = playerTransform.GetComponent<CharacterController>();
        modelRotate = playerTransform.Find("ModelBox").Find("midOutput01").GetComponent<Transform>();
        cameraRotateY = playerTransform.Find("CameraRotateY").GetComponent<Transform>();
        cameraRotateZ = cameraRotateY.Find("CameraRotateZ").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveGame()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create("./GameData.dat");
        GameData saveData = new GameData();

        //Position & Rotation
        saveData.playerPositionX = playerTransform.position.x;
        saveData.playerPositionY = playerTransform.position.y;
        saveData.playerPositionZ = playerTransform.position.z;
        saveData.cameraRotationY = cameraRotateY.localEulerAngles.y;
        saveData.cameraRotationZ = cameraRotateZ.localEulerAngles.z;
        saveData.modelRotation = modelRotate.localEulerAngles.y;

        //Player data
        saveData.attack = PlayerData.instance.attack;
        saveData.defense = PlayerData.instance.defense;
        saveData.curHealth = PlayerData.instance.curHealth;
        saveData.maxHealth = PlayerData.instance.maxHealth;
        saveData.exp = PlayerData.instance.exp;
        saveData.maxExp = PlayerData.instance.maxExp;
        saveData.levels = PlayerData.instance.levels;

        binaryFormatter.Serialize(file, saveData);
        file.Close();

        Debug.Log("Game saved!");
    }

    public void LoadGame()
    {
        if (File.Exists("./GameData.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open("./GameData.dat", FileMode.Open);
            GameData saveData = (GameData)binaryFormatter.Deserialize(file);

            //Position & Rotation
            characterController.enabled = false;
            playerTransform.position = new Vector3(saveData.playerPositionX, saveData.playerPositionY, saveData.playerPositionZ);
            characterController.enabled = true;
            cameraRotateY.localEulerAngles = new Vector3(cameraRotateY.localEulerAngles.x, saveData.cameraRotationY, cameraRotateY.localEulerAngles.z);
            cameraRotateZ.localEulerAngles = new Vector3(cameraRotateZ.localEulerAngles.x, cameraRotateZ.localEulerAngles.y, saveData.cameraRotationZ);
            modelRotate.localEulerAngles = new Vector3(modelRotate.localEulerAngles.x, saveData.modelRotation, modelRotate.localEulerAngles.z);

            //Player data
            PlayerData.instance.attack = saveData.attack ;
            PlayerData.instance.defense = saveData.defense;
            PlayerData.instance.curHealth = saveData.curHealth;
            PlayerData.instance.maxHealth = saveData.maxHealth;
            PlayerData.instance.exp = saveData.exp;
            PlayerData.instance.maxExp = saveData.maxExp;
            PlayerData.instance.levels = saveData.levels;

            file.Close();

            Debug.Log("Game loaded!");
        }
        else
        {
            SaveGame();

            Debug.Log("No game save! New save Created");
        }
    }
}

[Serializable]
class GameData
{
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
    public float cameraRotationY;
    public float cameraRotationZ;
    public float modelRotation;

    public float attack;
    public float defense;
    public float curHealth;
    public float maxHealth;
    public float exp;
    public float maxExp;
    public float levels;
}
