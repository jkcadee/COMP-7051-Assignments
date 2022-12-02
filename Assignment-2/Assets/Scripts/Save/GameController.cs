using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static int highScore = 0;
    public static Vector3 playerPos;
    public static Vector3 enemyPos;
    public GameObject player;
    public GameObject enemy;
    public Volume volume;
    const string fileName = "/gameData.dat";
    InputActions ia;

    public static GameController gCtrl;

    public void Awake()
    {
        if (gCtrl == null)
        {
            DontDestroyOnLoad(gameObject);
            gCtrl = this;
        }

        volume.enabled = false;
        ia = new();
    }

    private void OnEnable()
    {
        ia.Player.Timeshift.Enable();
        ia.Player.Timeshift.performed += TimeShift;
    }

    private void OnDisable()
    {
        ia.Player.Timeshift.Disable();
        ia.Player.Timeshift.performed -= TimeShift;
    }

    void TimeShift(InputAction.CallbackContext _)
    {
        volume.enabled = !volume.enabled;
        MusicController.Instance.StopMusic();
        MusicController.Instance.SelectMusic(volume.enabled ? 1 : 0);
        MusicController.Instance.PlayMusic();
    }

    private void OnApplicationQuit()
    {
        SavePlayerPos();
        SaveEnemyPos();
        Debug.Log(highScore);
        SaveScore(highScore);
    }

    public void LoadScore()
    {
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + fileName, FileMode.Open, FileAccess.Read);
            GameData data = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            Debug.Log(data.score);
            highScore = data.score;
        }
    }

    public void LoadPlayerPos()
    {
        playerPos = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
    }

    public void LoadEnemyPos()
    {
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + fileName, FileMode.Open, FileAccess.Read);
            GameData data = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            enemyPos = new Vector3(data.enemyPosX, data.enemyPosY, data.enemyPosZ);
        }
    }

    public void SaveScore(int score)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(Application.persistentDataPath + fileName, FileMode.OpenOrCreate);
        GameData data = new GameData();
        data.score = score;
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
        
    }

    public void SavePlayerPos()
    {
        GameObject currentPlayer = GameObject.FindGameObjectWithTag("Player");
        PlayerPrefs.SetFloat("PlayerX", currentPlayer.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", currentPlayer.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", currentPlayer.transform.position.z);
    }

    public void SaveEnemyPos()
    {
        GameObject currentEnemy = GameObject.FindGameObjectWithTag("Enemy");
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(Application.persistentDataPath + fileName, FileMode.OpenOrCreate);
        GameData data = new GameData();
        data.enemyPosX = currentEnemy.transform.position.x;
        data.enemyPosX = currentEnemy.transform.position.y;
        data.enemyPosX = currentEnemy.transform.position.z;
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    //PlayerPrefs save/load
    public int GetCurrentScore()
    {
        return PlayerPrefs.GetInt("CurrentScore");
    }

    public static void SetCurrentScore(int num)
    {
        PlayerPrefs.SetInt("CurrentScore", num);
    }
}

[Serializable]
class GameData
{
    public int score;
    public float enemyPosX;
    public float enemyPosY;
    public float enemyPosZ;
}