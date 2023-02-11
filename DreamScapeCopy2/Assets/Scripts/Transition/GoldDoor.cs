using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldDoor : Interactable
{
    #region Singleton

    public static GoldDoor instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public GameObject interactButton;
    public GameObject keyButton;
    public bool nextDoor;
    private int sceneID;
    InventoryPl inventory;
    ItemInteract itemTmp;
    List<ItemInteract> items;
    public bool usedKey;
    public bool keyFound;
    public bool needsKey;
    public string doorName;
    public bool opened = false;
    public AudioSource openSound;

    // Start is called before the first frame update
    void Start()//загружаем все данные о персонаже, двери и уравнении
    {
        //PlayerPrefs.DeleteAll();
        LoadDoorState();
        //Debug.Log(doorName);
        inventory = InventoryPl.instance;
        //inventory.LoadInventory();
        inventory.dontPlayAudio = false;
        items = inventory.GetItemList();
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
        //InventoryPl.instance.GetItemList();
        //PlayerPrefs.DeleteAll();c
        foreach (ItemInteract item in items) // проверка на наличие золотого ключа в инвентаре
        {
            if (item.name == null)
            {
                break;
            }
            else if (item.name == "GoldKey")
            {
                keyFound = true;
                itemTmp = item;
            }
            else
            {
                keyFound = false;
            }
        }
        if (needsKey)
        {
            if (Input.GetKeyDown(KeyCode.E) && nextDoor && usedKey && opened) // если персонаж находится в зоне двери то при нажатии кнопки Е мы переходим на следующую сцену
            {
                Transition obj = transform.gameObject.GetComponent<Transition>();
                GlobalControl.Instance.LoadData();
                GlobalControl.Instance.IsSceneBeingLoaded = true;

                //int whichScene = sceneID;
                inventory.SaveInventory();
                ChangeMaterial.instance.SaveClothes();
                LevelUp.Instance.SaveXp();
                obj.Interact();
                //SceneManager.LoadScene(whichScene);
            }
            else if (Input.GetKeyDown(KeyCode.E) && nextDoor && !usedKey && keyFound && !opened)
            {
                openSound.Play();
                keyButton.SetActive(false);
                interactButton.SetActive(true);
                keyFound = false;
                inventory.Remove(itemTmp);
                usedKey = true;
                needsKey = false;
                opened = true;
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && nextDoor) // если персонаж находится в зоне двери то при нажатии кнопки Е мы переходим на следующую сцену и сохранем все о нем данные
            {
                Transition obj = transform.gameObject.GetComponent<Transition>();
                GlobalControl.Instance.LoadData();
                GlobalControl.Instance.IsSceneBeingLoaded = true;

                //int whichScene = sceneID;

                ChangeMaterial.instance.SaveClothes();
                inventory.SaveInventory();
                LevelUp.Instance.SaveXp();
                obj.Interact();
                //SceneManager.LoadScene(whichScene);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (needsKey)
        {
            if (other.tag == "Player" && keyFound) //если персонаж рядом с дверью то сохраняем его данные в этой сцене 
            {

                PlayerState.Instance.localPlayerData.SceneID = sceneID;
                PlayerState.Instance.localPlayerData.PositionX = transform.position.x;
                PlayerState.Instance.localPlayerData.PositionY = transform.position.y;
                PlayerState.Instance.localPlayerData.PositionZ = transform.position.z;
                GlobalControl.Instance.SaveData();
                nextDoor = true;
                keyButton.SetActive(true);
                keyFound = false;
            }

            //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
            if (other.tag == "Player" && usedKey) //если персонаж рядом с дверью то сохраняем его данные в этой сцене 
            {

                PlayerState.Instance.localPlayerData.SceneID = sceneID;
                PlayerState.Instance.localPlayerData.PositionX = transform.position.x;
                PlayerState.Instance.localPlayerData.PositionY = transform.position.y;
                PlayerState.Instance.localPlayerData.PositionZ = transform.position.z;

                GlobalControl.Instance.SaveData();
                opened = true;
                nextDoor = true;
                interactButton.SetActive(true);
            }

        }
        else
        {
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


    }
    private void OnTriggerExit(Collider other)
    {

        //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
        if (other.tag == "Player")
        {
            nextDoor = false;
            interactButton.SetActive(false);
            keyButton.SetActive(false); // убираем кнопку если вне зоны двери
        }
    }
    public void SaveDoorState(bool value) // сохраняем данные о двери
    {
        opened = value;
        //Debug.Log("Key needed (save) " + opened);
        PlayerPrefs.SetInt(doorName, value ? 1 : 0);// проверка на открытую дверь (если загрузим 1, то открыли ее, если 0 то нет)
        //Debug.Log(PlayerPrefs.GetInt(doorName));
        PlayerPrefs.Save();
    }
    public void LoadDoorState()
    {
        //Debug.Log(PlayerPrefs.GetInt(doorName));
        opened = PlayerPrefs.GetInt(doorName) == 1 ? true : false;// проверка на открытую дверь (если загрузим 1, то открыли ее, если 0 то нет)
        if (opened)
        {
            needsKey = false;
        }
    }
}
