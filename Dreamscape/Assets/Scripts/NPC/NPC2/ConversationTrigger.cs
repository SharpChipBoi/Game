using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class ConversationTrigger : MonoBehaviour
{
    private ConversationController ui;
    public PlayerMovement movement;
    public GameObject interactButton;
    public CinemachineTargetGroup targetGroup;
    public GameObject npc;


    void Start()
    {
        ui = ConversationController.instance;
        interactButton.SetActive(false);
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !ui.inDialogue)
        {
            ui.CameraChange(true);
            interactButton.SetActive(false);
            targetGroup.m_Targets[1].target = npc.transform;
            ui.inDialogue = true;
            movement.active = false;
            //currentNpc.TurnToPlayer(transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(false);
        }
    }
}

