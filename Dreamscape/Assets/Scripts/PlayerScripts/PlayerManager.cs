using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance;

	public Transform playerPosition;
	public GameObject player;

	//TUTORIAL
	public CharacterStats localPlayerData = new CharacterStats();

	void Awake()
	{
		if (Instance == null)
			Instance = this;

		if (Instance != this)
			Destroy(gameObject);

		GlobalControl.Instance.Player = gameObject;
	}
	public void SavePlayer()
	{
		GlobalControl.Instance.savedPlayerData = localPlayerData;
	}
	void Start()
	{
		localPlayerData = GlobalControl.Instance.savedPlayerData;
	}

	public void KillPlayer()
    {
		SceneManager.LoadScene("Customization");
	}

}
