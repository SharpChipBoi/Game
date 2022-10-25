using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance;

	public Transform playerPosition;

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
}
