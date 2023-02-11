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

    public AudioSource bop;

    [Header("Cameras")]
    public GameObject gameCam;
    public GameObject dialogueCam;

    private int activeLineIndex;
    public bool conversationStarted = false;
    public bool inDialogue = false;

    public void ChangeConversation(Conversation nextConversation)//если есть следующее предложение меняемся на него
    {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }
    private void Awake()
    {

        instance = this;
    }
    private void Start()//подключаем двух спикеров
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUiController>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUiController>();
    }

    private void Update()//при нажатии enter следующий диалог
    {
        if (Input.GetKeyDown(KeyCode.Return) && inDialogue)
        {
            bop.Stop();
            movement.active = false;
            AdvanceLine();
        }
        else if (Input.GetKeyDown(KeyCode.X) && !inDialogue)
        {
            EndConversation();
        }//если нажмем х мы выйдем из разговора
    }
    public void CameraChange(bool dialogue)//меняем сновную камеру на камеру диалога
    {
        gameCam.SetActive(!dialogue);
        dialogueCam.SetActive(dialogue);
    }
    private void EndConversation()//завершаем диалог и отключаем весь ЮИ
    {
        //CameraChange(false);

        StopAllCoroutines();
        bop.Stop();
        conversation = defaultConversation;
        conversationStarted = false;
        inDialogue = false;
        movement.active = true;
        speakerUILeft.Hide();
        speakerUIRight.Hide();
    }

    public void Initialize()//Начинаем диалог с идексом 0
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }

    public void AdvanceLine()//Запускаем сделующий диалог
    {
        if (conversation == null) return;
        if (!conversationStarted) Initialize();
        if (activeLineIndex < conversation.lines.Length)
            DisplayLine();
        else
            AdvanceConversation();
    }

    private void DisplayLine()//Вывод диалога на экран
    {
        bop.Stop();
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

    private void AdvanceConversation()//после ответа на вопросы мы меняем диалог или выходем из него полностью, если вопроса не существует
    {
        if (conversation.question != null)
        {
            Debug.Log(questionEvent == null);
            Debug.Log(questionEvent);
            questionEvent.Invoke(conversation.question);
        }
        else if (conversation.nextConversation != null)
        {
            bop.Stop();
            ChangeConversation(conversation.nextConversation);
        }
        else
        {
            bop.Stop();
            EndConversation();
        }
    }

    private void SetDialog(SpeakerUiController activeSpeakerUI,SpeakerUiController inactiveSpeakerUI,Line line) //подключаем UI диалога с нужной информацией
    {
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();

        activeSpeakerUI.Dialog = "";
        activeSpeakerUI.Mood = line.mood;

        StopAllCoroutines();
        StartCoroutine(EffectTypewriter(line.text, activeSpeakerUI));
    }

    private IEnumerator EffectTypewriter(string text, SpeakerUiController controller) //Выписываем диалог
    {
        foreach (char character in text.ToCharArray())
        {
            PlayAudioClip();
            controller.Dialog += character;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void PlayAudioClip() //проигрываем звуки НПС
    {
        if (!bop.isPlaying)
        {
            bop.PlayOneShot(bop.clip);
        }
    }
}
