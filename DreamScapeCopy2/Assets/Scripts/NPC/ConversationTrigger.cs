using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class ConversationTrigger : MonoBehaviour
{
    private ConversationController ui;
    public GameObject interactButton;
    public GameObject npc;
    public bool canTalk = false;
    public PlayerMovement movement;


    void Start()
    {
        ui = ConversationController.instance;
        interactButton.SetActive(false);
    }

    void Update()
    {
        if (canTalk && !ui.inDialogue) // если персонаж не нв диалоге и может говорить, то включаем UI кнопки взаимодействия, запускаем диалог при ее нажатии
        {
            interactButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                movement.active = false;
                ui.inDialogue = true;
                interactButton.SetActive(false);
                ui.AdvanceLine();
            }
        }
    }
    private void OnTriggerEnter(Collider other) // проверка на колизию с персонажем
    {
        if (other.CompareTag("Player"))
        {
            canTalk = true;
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;
            interactButton.SetActive(false);
        }
    }
}

