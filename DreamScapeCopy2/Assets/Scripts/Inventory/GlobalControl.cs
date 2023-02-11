using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public PlayerStatistics savedPlayerData = new PlayerStatistics();

    public GameObject Player;

    //TransitionTarget дает нам возможность заспавнить персонажа в выбранном нами месте
    public Transform TransitionTarget;

    private void Update()
    {
        GameObject pl = GameObject.Find("ThirdPersonPlayer");
        Player = pl;
    }

    void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);//для того чтобы у нас не было дублеровынных GlobalControl проверяем присутсвует ли в сцене если да то удаляем и выгружаем из старов сцены в новую со всеми данными
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        if (TransitionTarget == null)
            TransitionTarget = gameObject.transform;

    }

    public PlayerStatistics LocalCopyOfData;
    public bool IsSceneBeingLoaded = false;

    public void SaveData() // сохраняем дату о персонаже в отдельный файл, но сначала дату переводим в двоичную систему чтобы нельзя было его открыть с внешнего источника и изменить
    {
        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = new FileStream("Saves/save.binary", FileMode.Create);

        LocalCopyOfData = PlayerState.Instance.localPlayerData;

        formatter.Serialize(saveFile, LocalCopyOfData);

        saveFile.Close();
    }

    public void LoadData() // выгружаем дату
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);

        LocalCopyOfData = (PlayerStatistics)formatter.Deserialize(saveFile);

        saveFile.Close();
    }
}