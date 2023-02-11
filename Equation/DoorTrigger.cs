using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    public GameObject pressurePlate;
    public bool isOpen = false;
    public bool isPressed = false;
    public bool lucyDoor = false;
    public AudioSource openGate;
    public AudioSource fallOnMetal;
    // Start is called before the first frame update

    private static DoorTrigger instance;
    public static DoorTrigger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DoorTrigger>(); //находит DoorTrigger и кладет его в instance чтобы мы могли использовать его класс
            }
            return DoorTrigger.instance;
        }
    }

    private void Update()//если у нас нажата кнопка определенным объектом, то открываем дверь
    {
        if (isOpen && isPressed)
        {
            openGate.Play();
            door.transform.Translate(Vector3.up * Time.deltaTime * 5);

        }
        if (door.transform.position.y > 4 && isPressed)//проверка на то чтобы дверь не улетела
        {
            isOpen = false;
            lucyDoor = true;
        }
    }

    private void OnTriggerEnter(Collider other) //проверка на коллизию с предметом, меняем позициу плиты
    {
        if (other.tag != "Player")
        {
            isOpen = true;

            if (!isPressed)
            {
                fallOnMetal.Play();
                pressurePlate.transform.position -= new Vector3(0, 0.245f, 0);
                isPressed = true;
            }

        }

    }
    private void OnTriggerExit(Collider other)//проверка на отсутствии коллизии с предметом, меняем позициу плиты
    {
        if (isPressed && other.tag != "Player")
        {
            isOpen = false;
            pressurePlate.transform.position += new Vector3(0, 0.245f, 0);
            isPressed = false;
        }
    }
}
