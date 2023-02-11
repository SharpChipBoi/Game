using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucyReaction : MonoBehaviour
{
    [SerializeField]
    DoorTrigger doorOpen;
    public Conversation conversation1;
    private ConversationController ui;
    private void Start()
    {

        ui = ConversationController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen.lucyDoor && !ui.inDialogue)
        //Проверка если мы открыли дверь, то Люси на нее отреагирует 
        {
            ui.defaultConversation = conversation1;
            ui.conversation = conversation1;
        }
    }
}
