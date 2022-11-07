using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//TUTORIAL
[Serializable]
public class PlayerStatistics // данные для сохранения для перехода сцен
{

    public int SceneID;
    public float PositionX, PositionY, PositionZ;

    public float HP;
    public float XP;
}
