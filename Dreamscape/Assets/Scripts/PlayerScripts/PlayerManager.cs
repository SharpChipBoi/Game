using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager instance;

	public Transform playerPosition;
	public GameObject player;

	//TUTORIAL
	public PlayerStats localPlayerData = new PlayerStats();

	void Awake()
	{
		if (instance == null)
			instance = this;

		if (instance != this)
			Destroy(gameObject);

		//GlobalControl.instance.Player = gameObject;
	}
	public void SavePlayer()
	{
		GlobalControl.instance.savedPlayerData = localPlayerData;
	}
	void Start()
	{
		localPlayerData = GlobalControl.instance.savedPlayerData;
	}

	public void KillPlayer()
    {
		SceneManager.LoadScene("Customization");
	}

}
