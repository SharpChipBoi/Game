using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeakerUiController : MonoBehaviour
{
    public Image portrait;
    public TextMeshProUGUI fullName;
    public TextMeshProUGUI dialog;
    public Mood mood;

    private Character speaker;
    public Character Speaker
    {
        get { return speaker; }
        set
        {
            speaker = value;
            // portrait.sprite = speaker.portrait;
            fullName.text = speaker.fullName;
        }
    }

    public string Dialog
    {
        get { return dialog.text; }
        set { dialog.text = value; }
    }

    public Mood Mood
    {
        set
        {
            Sprite sprite;
            if (value == Mood.Angry)
            {
                sprite = speaker.portraitAngry;
            }
            else if (value == Mood.Sad)
            {
                sprite = speaker.portraitSad;
            }
            else if(value == Mood.Happy)
            {
                sprite = speaker.portraitHappy;
            }
            else
            {
                sprite = speaker.portrait;
            }

            portrait.sprite = sprite;
        }
    }

    public bool HasSpeaker()
    {
        return speaker != null;
    }

    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
