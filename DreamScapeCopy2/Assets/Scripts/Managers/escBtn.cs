using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escBtn : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //при нажатии Esc вызываем функцию, которая запускает сцену с главным меню
        {
            GoToMainMenu();
        }   
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
