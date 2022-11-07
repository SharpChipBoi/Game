using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
				instance = GameObject.FindObjectOfType<PlayerManager>(); //находит ачивмент менеджер и  пихает его в instance чьлбы мы могли использовать его класс
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

	public void KillPlayer()
    {
		SceneManager.LoadScene("Customization");
	}

}
