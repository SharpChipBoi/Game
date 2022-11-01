using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField]
    private string loadRoom;
    public GameObject interactButton;
    public bool nextDoor;
    //To which level are we going to?
    //public int TargetedSceneIndex;

    //TargetPlayerLocation will be saved in Global, and then set to the player
    //after the scene transition, so the player is in correct spot in the new scene.
    //public Transform TargetPlayerLocation;

    void Start()
    {
        interactButton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nextDoor)
        {
            PlayerManager.Instance.SavePlayer();
            SceneManager.LoadScene(loadRoom);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
        if (other.CompareTag("Player"))
        {
            nextDoor = true;
            interactButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
        if (other.CompareTag("Player"))
        {
            nextDoor = false;
            interactButton.SetActive(false);
        }
    }
}
