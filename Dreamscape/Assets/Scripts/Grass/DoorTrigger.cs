using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    public GameObject pressurePlate;
    public bool isOpen = false;
    public bool moveBack = false;
    public bool isPressed = false;
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
        }
        if (moveBack)
        {
            door.transform.Translate(Vector3.down * Time.deltaTime * 5);
        }
        if(door.transform.position.y < 4 && moveBack)
        {
            moveBack = false;
            isOpen = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            isOpen = true;

            if (!isPressed)
            {
                pressurePlate.transform.position -= new Vector3(0, 0.05f, 0);
                isPressed = true;
            }

        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (isPressed && other.tag != "Player")
        {
            moveBack = true;
            isOpen = false;
            pressurePlate.transform.position += new Vector3(0, 0.05f, 0);
            isPressed = false;
        }
    }
}
