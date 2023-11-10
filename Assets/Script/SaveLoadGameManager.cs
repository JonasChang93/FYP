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
        modelRotate = playerTransform.Find("Armature").GetComponent<Transform>();
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

        //Save from component
        saveData.playerPositionX = playerTransform.position.x;
        saveData.playerPositionY = playerTransform.position.y;
        saveData.playerPositionZ = playerTransform.position.z;
        saveData.cameraRotationY = cameraRotateY.localEulerAngles.y;
        saveData.cameraRotationZ = cameraRotateZ.localEulerAngles.z;
        saveData.modelRotation = modelRotate.localEulerAngles.y;
        
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

            //Load to component
            characterController.enabled = false;
            playerTransform.position = new Vector3(saveData.playerPositionX, saveData.playerPositionY, saveData.playerPositionZ);
            characterController.enabled = true;
            cameraRotateY.localEulerAngles = new Vector3(cameraRotateY.localEulerAngles.x, saveData.cameraRotationY, cameraRotateY.localEulerAngles.z);
            cameraRotateZ.localEulerAngles = new Vector3(cameraRotateZ.localEulerAngles.x, cameraRotateZ.localEulerAngles.y, saveData.cameraRotationZ);
            modelRotate.localEulerAngles = new Vector3(modelRotate.localEulerAngles.x, saveData.modelRotation, modelRotate.localEulerAngles.z);
            
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
}
