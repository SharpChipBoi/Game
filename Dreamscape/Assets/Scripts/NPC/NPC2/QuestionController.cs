using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionController : MonoBehaviour
{
    public Question question;
    //public TextMeshProUGUI questionText;
    public Button choiceButton;

    private List<ChoiceController> choiceControllers = new List<ChoiceController>();

    public void Change(Question _question)//меняем диалог и запускаем его
    {
        RemoveChoices();
        question = _question;
        gameObject.SetActive(true);
        Initialize();
    }

    public void Hide(Conversation conversation)//прячем выбор
    {
        RemoveChoices();
        gameObject.SetActive(false);
    }

    private void RemoveChoices()//удаляем выбор кнопок
    {
        foreach (ChoiceController c in choiceControllers)
            Destroy(c.gameObject);

        choiceControllers.Clear();
    }

    private void Start() { }

    private void Initialize()//создаем вопрос и кнопки
    {
        //questionText.text = question.text;

        for (int index = 0; index < question.choices.Length; index++)
        {
            ChoiceController c = ChoiceController.AddChoiceButton(choiceButton, question.choices[index], index);
            choiceControllers.Add(c);
        }

        choiceButton.gameObject.SetActive(false);
    }
}
