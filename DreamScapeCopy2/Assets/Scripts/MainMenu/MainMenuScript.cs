using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); //загрузка сцены с индексом 1 
    }

    public void QuitGame()
    {
        Application.Quit(); //выход из игры
    }
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll(); //удаление всех сохраненных данных 
    }
}
