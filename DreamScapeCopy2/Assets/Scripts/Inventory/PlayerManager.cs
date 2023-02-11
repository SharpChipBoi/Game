using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Скрипт не используется
[System.Serializable]
public class PlayerManager : MonoBehaviour
{
	public static PlayerManager instance;
	public static PlayerManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<PlayerManager>(); //находит PlayerManager и кладем его в instance чьлбы мы могли использовать его класс
			}
			return PlayerManager.instance;
		}
	}
	public GameObject player;
	

	//public PlayerStats localPlayerData = new PlayerStats();

	void Start()
	{
		//localPlayerData = GlobalControl.instance.savedPlayerData;
	}


}
