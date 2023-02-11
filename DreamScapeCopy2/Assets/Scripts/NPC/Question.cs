using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Choice
{
    [TextArea(2, 5)]
    public string text;
    public Conversation conversation;
}

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Question : ScriptableObject//позволяет создать свой вопрос
{
    //[TextArea(2, 5)]
    //public string text;
    public Choice[] choices;
}