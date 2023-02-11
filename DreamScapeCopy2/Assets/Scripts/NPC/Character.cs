using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject //позволяет создать свой объект с данными о персонаже
{
    public string fullName;
    public Sprite portrait;
    public Sprite portraitAngry;
    public Sprite portraitSad;
    public Sprite portraitHappy;
    public Sprite portraitCocky;
    public Sprite portraitConf;
}
