using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerStatistics // данные для сохранения для перехода сцен
{

    public int SceneID; //индекс сцены
    public float PositionX, PositionY, PositionZ; // позиция по XYZ

    public float HP; //кол-во здоровья и опыта
    public float XP;
}
