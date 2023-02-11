using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoorTransform : MonoBehaviour
{
    public Transform door;
    public void Update() //сохраняем позицию двери при загрузки новой сцены
    {
        if(this.transform.position != door.position)
        {
            this.transform.position = door.position;
        }
    }
}
