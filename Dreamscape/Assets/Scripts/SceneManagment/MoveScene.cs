using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField]
    private string loadRoom;
    //To which level are we going to?
    //public int TargetedSceneIndex;

    //TargetPlayerLocation will be saved in Global, and then set to the player
    //after the scene transition, so the player is in correct spot in the new scene.
    //public Transform TargetPlayerLocation;



    private void OnTriggerEnter(Collider other)
    {

        //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.SavePlayer();
            SceneManager.LoadScene(loadRoom);
        }
    }
}
