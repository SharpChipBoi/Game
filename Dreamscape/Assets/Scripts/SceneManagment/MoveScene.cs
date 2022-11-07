using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveScene : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;
    private string loadRoom;
    public GameObject interactButton;
    public bool nextDoor;



    void Start()
    {
        player = GetComponent<PlayerMovement>();
        interactButton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nextDoor)
        {
            SavePlayer();
            //PlayerManager.Instance.SavePlayer();
            SceneManager.LoadScene(loadRoom);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
        if (other.tag == "Player")
        {
            nextDoor = true;
            interactButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
        if (other.tag == "Player")
        {
            nextDoor = false;
            interactButton.SetActive(false);
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player); 
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

}
