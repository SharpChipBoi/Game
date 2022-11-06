using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl instance;

    public PlayerStats savedPlayerData = new PlayerStats();
    //Copy or our player, if we ever need it game-wide

    private void OnDestroy()
    {
        Debug.Log("GlobalObject destroyed");
    }

    void Awake()
    {
        //Application.targetFrameRate = 144;
        //GameObject.DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        //if (TransitionTarget == null)
        //TransitionTarget = gameObject.transform;

    }
}
