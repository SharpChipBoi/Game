using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    InventoryPl inventory;
    private void Start()
    {
        inventory = InventoryPl.instance;
        inventory.LoadInventory();
        InventoryPl.instance.dontPlayAudio = false;
        ChangeMaterial.instance.LoadClothes();
    }
    private void Update()
    {
        if (Input.GetKey("escape")) //при нажатии кнопки ESC выход из игры
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") //если проимходит колизия с объектом с тэгом "Player", выходим из игры и очищаем память от сохраненных предметов и ачивок
        {
            PlayerPrefs.DeleteAll();
            Door.instance.SaveDoorState(false);
            SceneManager.LoadScene(0);
            //UnityEditor.EditorApplication.isPlaying = false;

        }
    }
}
