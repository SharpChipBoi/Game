using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Events;
using System.Collections;

[Serializable]
public class QuestionEvent : UnityEvent<Question> { }

public class ConversationController : MonoBehaviour
{
    public Conversation conversation;
    public Conversation defaultConversation;
    public QuestionEvent questionEvent;
    public static ConversationController instance;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    public PlayerMovement movement;

    private SpeakerUiController speakerUILeft;
    private SpeakerUiController speakerUIRight;

    [Header("Cameras")]
    public GameObject gameCam;
    public GameObject dialogueCam;

    private int activeLineIndex;
    private bool conversationStarted = false;
    public bool inDialogue;

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }
    private void Awake()
    {

        instance = this;
    }
    private void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUiController>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUiController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inDialogue)
        {
            movement.active = false;
            AdvanceLine();
        }
        else if (Input.GetKeyDown("x"))
            EndConversation();
    }
    public void CameraChange(bool dialogue)
    {
        gameCam.SetActive(!dialogue);
        dialogueCam.SetActive(dialogue);
    }
    private void EndConversation()
    {
        conversation = defaultConversation;
        conversationStarted = false;
        inDialogue = false;
        movement.active = true;
        speakerUILeft.Hide();
        speakerUIRight.Hide();
    }

    private void Initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }

    private void AdvanceLine()
    {
        if (conversation == null) return;
        if (!conversationStarted) Initialize();

        if (activeLineIndex < conversation.lines.Length)
            DisplayLine();
        else
            AdvanceConversation();
    }

    private void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialog(speakerUILeft, speakerUIRight, line);
        }
        else
        {
            SetDialog(speakerUIRight, speakerUILeft, line);
        }

        activeLineIndex += 1;
    }

    private void AdvanceConversation()
    {
        // These are really three types of dialog tree node
        // and should be three different objects with a standard interface
        if (conversation.question != null)
        {
            Debug.Log(questionEvent == null);
            Debug.Log(questionEvent);
            questionEvent.Invoke(conversation.question);
        }
        else if (conversation.nextConversation != null)
            ChangeConversation(conversation.nextConversation);
        else
        {
            EndConversation();
        }
    }

    private void SetDialog(
        SpeakerUiController activeSpeakerUI,
        SpeakerUiController inactiveSpeakerUI,
        Line line
    )
    {
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();

        activeSpeakerUI.Dialog = "";
        activeSpeakerUI.Mood = line.mood;

        StopAllCoroutines();
        StartCoroutine(EffectTypewriter(line.text, activeSpeakerUI));
    }

    private IEnumerator EffectTypewriter(string text, SpeakerUiController controller)
    {
        foreach (char character in text.ToCharArray())
        {
            controller.Dialog += character;
            yield return new WaitForSeconds(0.05f);
            // yield return null;
        }
    }
}
