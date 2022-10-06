using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAppear : MonoBehaviour
{
    private PlayerMovement movement;

    private bool isShowing;
    public GameObject inventoryUi;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            movement.active = false;
            isShowing = !isShowing;
            inventoryUi.SetActive(isShowing);
        }
    }
}
