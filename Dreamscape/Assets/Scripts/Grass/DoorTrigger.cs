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
    // Start is called before the first frame update

    private void Update()
    {
        if (isOpen && isPressed)
        {
            door.transform.Translate(Vector3.up * Time.deltaTime * 5);

        }
        if (door.transform.position.y > 4 && isPressed)
        {
            isOpen = false;
            lucyDoor = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            isOpen = true;

            if (!isPressed)
            {
                pressurePlate.transform.position -= new Vector3(0, 0.245f, 0);
                isPressed = true;
            }

        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (isPressed && other.tag != "Player")
        {
            isOpen = false;
            pressurePlate.transform.position += new Vector3(0, 0.245f, 0);
            isPressed = false;
        }
    }
}
