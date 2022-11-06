using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class ConversationTrigger : MonoBehaviour
{
    private ConversationController ui;
    public GameObject interactButton;
    public CinemachineTargetGroup targetGroup;
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
        if(canTalk && !ui.inDialogue) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                movement.active = false;
                ui.CameraChange(true);
                ui.inDialogue = true;
                interactButton.SetActive(false);
                targetGroup.m_Targets[1].target = npc.transform;
                ui.AdvanceLine();
                //currentNpc.TurnToPlayer(transform.position);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
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

