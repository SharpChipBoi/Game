using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour //класс предмета который враг цветок может проглатить
{
    public static Edible instance;
    public static Edible Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Edible>();
            }
            return Edible.instance;
        }
    }
    public bool isEaten;
}
