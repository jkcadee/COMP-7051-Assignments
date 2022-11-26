using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public int highScore = 0;
    public static Vector3 playerPos;
    public static Vector3 enemyPos;
    public GameObject player;
    public GameObject enemy;
    const string fileName = "/gameData.dat";

    public static GameController gCtrl;

    public void Awake()
    {
        if (gCtrl == null)
        {
            DontDestroyOnLoad(gameObject);
            gCtrl = this;
            //LoadScore();

        }
    }

    private void OnApplicationQuit()
    {
        SavePlayerPos();
        SaveEnemyPos();
    }

    public void LoadScore()
    {
        SetCurrentScore(0);
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + fileName, FileMode.Open, FileAccess.Read);
            GameData data = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
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
        if (score > gCtrl.highScore)
        {
            gCtrl.highScore = score;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + fileName, FileMode.OpenOrCreate);
            GameData data = new GameData();
            data.score = score;
            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
        }
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

    public void SetCurrentScore(int num)
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