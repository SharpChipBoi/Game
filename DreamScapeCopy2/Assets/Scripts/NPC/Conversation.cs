using UnityEngine;
using System;
using UnityEngine.UI;

public enum Mood
{
    Neutral,
    Angry,
    Sad,
    Happy,
    Cocky,
    Conf
}

[Serializable]
public struct Line
{
    public Character character;

    [TextArea(2, 5)]
    public string text;
    public Mood mood;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject // дает возможность создать свой разговор
{
    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;
    public Question question;
    public Conversation nextConversation;
}
