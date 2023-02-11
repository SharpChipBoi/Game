using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureBoxManager : MonoBehaviour //при правильном ответе сохраняем позицию упавшего куба и загружаем ее
{
    public GameObject cubePosition;
    public GameObject cubeNewPosition;
    bool moved;
    // Start is called before the first frame update
    void Start()
    {
        cubePosition = GameObject.Find("PressureBox");
        cubeNewPosition = GameObject.Find("NewCubePos"); 
        if (DoorTrigger.Instance.isPressed)
        {
            SavePosition(true);
        }
        else
        {
            SavePosition(false);
        }
    }

    public void SavePosition(bool value)
    {
        moved = value;
        PlayerPrefs.SetInt(cubePosition.name, moved ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void LoadPosition()
    {
        moved = PlayerPrefs.GetInt(cubePosition.name) == 1 ? true : false;
        if (moved)
        {
            cubePosition.transform.position = cubeNewPosition.transform.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
