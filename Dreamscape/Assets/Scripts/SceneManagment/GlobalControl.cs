using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl instance;

    public static GlobalControl GetInstance()
    {
        return instance;
    }


    public PlayerStats savedPlayerData = new PlayerStats();

	//Copy or our player, if we ever need it game-wide
	public GameObject Player;

    void Awake()
    {
        //Application.targetFrameRate = 144;

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

		//if (TransitionTarget == null)
		//	TransitionTarget = gameObject.transform;
      
    }
}
