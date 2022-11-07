using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{


    public GameObject interactButton;
    public bool nextDoor;
    private int sceneID;
    // Start is called before the first frame update
    void Start()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; //Запоминаем данные о персонаже 
        if (GlobalControl.Instance.TransitionTarget != null)
            PlayerMovement.Instance.transform.position = GlobalControl.Instance.TransitionTarget.position;


        if (GlobalControl.Instance.IsSceneBeingLoaded) //если мы выгружаем персонажа то передаем стаые значения
        {
            PlayerState.Instance.localPlayerData = GlobalControl.Instance.LocalCopyOfData;

            transform.position = new Vector3(
                            GlobalControl.Instance.LocalCopyOfData.PositionX,
                            GlobalControl.Instance.LocalCopyOfData.PositionY,
                            GlobalControl.Instance.LocalCopyOfData.PositionZ + 0.1f);

            GlobalControl.Instance.IsSceneBeingLoaded = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && nextDoor) // если персонаж находится в зоне двери то при нажатии кнопки Е мы переходим на следующую сцену
        {
            Transition obj = transform.gameObject.GetComponent<Transition>();
            GlobalControl.Instance.LoadData();
            GlobalControl.Instance.IsSceneBeingLoaded = true;

            //int whichScene = sceneID;

            obj.Interact();
            //SceneManager.LoadScene(whichScene);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
        if (other.tag == "Player") //если персонаж рядом с дверью то сохраняем его данные в этой сцене 
        {

            PlayerState.Instance.localPlayerData.SceneID = sceneID;
            PlayerState.Instance.localPlayerData.PositionX = transform.position.x;
            PlayerState.Instance.localPlayerData.PositionY = transform.position.y;
            PlayerState.Instance.localPlayerData.PositionZ = transform.position.z;

            GlobalControl.Instance.SaveData();
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
            interactButton.SetActive(false); // убираем кнопку если вне зоны двери
        }
    }
}
