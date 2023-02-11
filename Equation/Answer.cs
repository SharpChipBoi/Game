using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer
{
    private string option;

    Transform[] childSize;

    public string Option
    {
        get { return option; }
        set { option = value; }
    }

    private bool answered;

    public bool Answered
    {
        get { return answered; }
        set { answered = value; }
    }

    private GameObject answerRef;

    public static Answer instance;

   
    public Answer(string option, GameObject answerRef) // создаем новые варианты ответа
    {
        this.option = option;
    }

    public bool CorrectAnswer() // если правильный ответ то меняем перменную answered на true
    {

        if (!answered)
        {
            answered = true;
            return true;
        }
        return false;
    }
}
