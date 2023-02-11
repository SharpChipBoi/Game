using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour
{
    public GameObject answerPrefab;

    public Sprite answeredSprite;

    public GameObject equation;
    public bool correct;

    private string answered;
    GameObject opt;
    public Dictionary<string, Answer> options = new Dictionary<string, Answer>();

    private static AnswerManager instance;

    public static AnswerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AnswerManager>();
            }
            return AnswerManager.instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        opt = GameObject.Find("AnswerSheet");
        CreateAnswer("AnswerSheet", "5");
        CreateAnswer("AnswerSheet", "27");
        CreateAnswer("AnswerSheet", "33");
        equation.SetActive(false);
        LoadAnswer();
    }

    // Update is called once per frame
    void Update()
    {
        if (correct)
        {
            Transform[] childSize = opt.GetComponentsInChildren<Transform>();
            for (int i = 0; i < (childSize.Length - 1) / 2; i++)
            {
                Button btn = opt.transform.GetChild(i).GetComponent<Button>();
                btn.interactable = false;
            }
        }
        else
        {
            AnswerManager.Instance.SaveAnswer(false);
        }
    }
    public void CorrectAnswer(string num) //Эта функция для проверки работы уравнения
    {
        if (options[num].CorrectAnswer())
        {
            Debug.Log("Yes");
        }
    }

    public void CreateAnswer(string parent, string num)
    {
        GameObject answer = (GameObject)Instantiate(answerPrefab);

        Answer newAnswer = new Answer(num, answer);

        options.Add(num, newAnswer);

        SetOptionInfo(parent, num, answer);

    }

    public void SetOptionInfo(string parent, string num, GameObject answer)//Возвращает всю информацию об ответе в виде кнопки
    {
        answer.transform.SetParent(GameObject.Find(parent).transform);
        answer.transform.localScale = new Vector3(1, 1, 1);
        //answer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = num;
        answer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = options[num].Option;
        //answer.transform.GetChild(2).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
    }

    public void SaveAnswer(bool value) // сохраняем данные об открытых ответах
    {
        correct = value;
        PlayerPrefs.SetInt(answered, value ? 1 : 0); // проверка на полученный ответ (если загрузим 1, то ответили правильно, если 0 то нет)

        PlayerPrefs.Save();
    }

    public void LoadAnswer() //загружаем ответ
    {
        correct = PlayerPrefs.GetInt(answered) == 1 ? true : false;// проверка на полученный ответ (если загрузим 1, то ответили правильно, если 0 то нет)
        Debug.Log(correct);
    }
}
