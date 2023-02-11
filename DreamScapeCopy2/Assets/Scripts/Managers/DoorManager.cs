using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DoorManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //string str = "sad";
        //File.WriteAllText(Application.dataPath + "/save.txt", str);
        //Door.instance.LoadDoorState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Door.instance.opened || !Door.instance.needsKey) // если дверь открыта или не нуждается в ключе, сохраняем о ней данные
        {
            Door.instance.SaveDoorState(true);
        }
        else
        {
            Door.instance.SaveDoorState(false);
        }
    }
}
