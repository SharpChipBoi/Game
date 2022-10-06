using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager ui;
    private NPCScript currentNpc;
    private PlayerMovement movement;
    public CinemachineTargetGroup targetGroup;
    public GameObject interactButton;

    [Space]

    [Header("Post Processing")]
    public Volume dialogueDof;

    void Start()
    {
        ui = DialogueManager.instance;
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !ui.inDialogue && currentNpc != null)
        {
            interactButton.SetActive(false);
            targetGroup.m_Targets[1].target = currentNpc.transform;
            movement.active = false;
            ui.SetCharNameAndColor();
            ui.inDialogue = true;
            ui.CameraChange(true);
            ui.ClearText();
            ui.FadeUI(true, .2f, .65f);
            //currentNpc.TurnToPlayer(transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNpc = other.GetComponent<NPCScript>();
            ui.currentNpc = currentNpc;
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNpc = null;
            ui.currentNpc = currentNpc;
            interactButton.SetActive(false);
        }
    }

    //public Message[] messages;
    //public Actor[] actors;
    //public CinemachineTargetGroup targetGroup;

    //public void StartDialogue()
    //{
    //    FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
    //}
}

//[System.Serializable]
//public class Message
//{
//    public int actorId;
//    public string message;
//}

//[System.Serializable]
//public class Actor
//{
//    public string name;
//    public Sprite sprite;
//}
