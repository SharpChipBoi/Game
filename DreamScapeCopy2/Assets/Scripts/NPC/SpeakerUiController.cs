using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeakerUiController : MonoBehaviour
{
    public static SpeakerUiController instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    //вся нужная информация о спикере
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

    public string Dialog//можно получить доступ к диалогу из других скриптов
    {
        get { return dialog.text; }
        set { dialog.text = value; }
    }

    public Mood Mood//показ эмоций
    {
        set
        {
            Sprite sprite;//если злой, то злой, если обычный, то обычный и тд 
            if (value == Mood.Angry)
            {
                sprite = speaker.portraitAngry;
            }
            else if (value == Mood.Sad)
            {
                sprite = speaker.portraitSad;
            }
            else if (value == Mood.Happy)
            {
                sprite = speaker.portraitHappy;
            }
            else if (value == Mood.Cocky)
            {
                sprite = speaker.portraitCocky;
            }
            else if (value == Mood.Conf)
            {
                sprite = speaker.portraitConf;
            }
            else
            {
                sprite = speaker.portrait;
            }

            portrait.sprite = sprite;
        }
    }

    public bool HasSpeaker()//проверка на наличие говорящего
    {
        return speaker != null;
    }

    public bool SpeakerIs(Character character)//ставим в говорящего переданного персонажа
    {
        return speaker == character;
    }

    public void Show()//подключаем UI спикера
    {
        gameObject.SetActive(true);
    }

    public void Hide()//отключаем UI спикера
    {
        gameObject.SetActive(false);
    }
}
