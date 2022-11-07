using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat //Статы которые легко можно модифицировать
{
    [SerializeField]
    public int baseVal;

    private List<int> modifiers = new List<int>();

    public int GetValue() // выдает значения
    {
        int finalValue = baseVal;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int modifier) // Добавить изменение например усиление защиты или урона
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier) // удалить это усиление
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }


}
