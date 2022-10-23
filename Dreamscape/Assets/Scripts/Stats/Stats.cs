using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour
{
    [SerializeField]
    public int baseVal;
    public int GetVal()
    {
        return baseVal;
    }
}
