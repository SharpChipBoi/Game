using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public string correctOption;
    Transform[] childSize;

    public void OpenEquation() // В этой функции идет проверка на правильный ответ в виде нажатый кнопки, если нажали нужную, то все кнопки диактивируются 
    {
        string option = GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text;// текст кнопки

        Debug.Log(option);
        if (option == correctOption)
        {
            GameObject opt = GameObject.Find("AnswerSheet");
            childSize = opt.GetComponentsInChildren<Transform>();
            for (int i = 0; i < (childSize.Length - 1) / 2; i++)
            {
                GameObject answers = GameObject.Find("AnswerSheet");
                Button btn = answers.transform.GetChild(i).GetComponent<Button>();//диактивируем все варианты ответа
                btn.interactable = false;
                Debug.Log("yes");
            }
            AnswerManager.Instance.SaveAnswer(true); //сохраняем ответ
            //Debug.Log("Correct");
            //Debug.Log(AnswerManager.Instance.correct);
        }
        else
        {
            Debug.Log("Wrong");
            AnswerManager.Instance.SaveAnswer(false);
            //AnswerManager.Instance.equation.SetActive(true);
        }

    }
}
