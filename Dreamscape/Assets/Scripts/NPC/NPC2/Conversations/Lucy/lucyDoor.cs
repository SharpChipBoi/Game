using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lucyDoor : MonoBehaviour
{
    [SerializeField]
    DoorTrigger doorOpen;
    public Conversation conversation1;
    private ConversationController ui;
    //Смотрим если мы открыли дверь, то Люси на нее отреагирует 
    private void Start()
    {

        ui = ConversationController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen.lucyDoor && !ui.inDialogue)
        {
            ui.defaultConversation = conversation1;
            ui.conversation = conversation1;
        }
    }
}
