using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct btnChoice
{
    [TextArea(2, 5)]
    public string text;
    //public Conversation conversation;
}
[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class BtnChoice : ScriptableObject //позволяет создавать свой предмет и хранить больше о нем информации
{
    //[TextArea(2, 5)]
    //public string text;
    public btnChoice[] btnChoices;
}