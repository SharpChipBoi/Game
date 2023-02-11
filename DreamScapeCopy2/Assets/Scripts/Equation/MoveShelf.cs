using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShelf : MonoBehaviour
{
    void Update()
    {
        if (AnswerManager.Instance.correct) //если правильно ответили на уравнение, то меняем позицию доски, держащей куб
        {
            this.transform.position = new Vector3(-12f, 10.56f, 2.773f);
        }
    }
}
