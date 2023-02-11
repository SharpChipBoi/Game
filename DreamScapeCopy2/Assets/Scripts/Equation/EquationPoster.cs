using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationPoster : Interactable
{
    public bool nextToPoster;
    public GameObject interactButton;
    public AudioSource pageList;
    bool played = true;

    private void Update()
    {
        if (nextToPoster)
        {
            interactButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) // если персонаж находится в зоне двери то при нажатии кнопки Е мы переходим на следующую сцену
            {
                interactButton.SetActive(false);
                this.Interact();
                pageList.Play();
                AnswerManager.Instance.equation.SetActive(!AnswerManager.Instance.equation.activeSelf);
                if(AnswerManager.Instance.equation.activeSelf == false)
                {
                    played = true;
                }
                else
                {
                    played = false;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
        if (other.tag == "Player") //если персонаж рядом с дверью то сохраняем его данные в этой сцене 
        {
            //GlobalControl.Instance.SaveData();
            nextToPoster = true;
            //interactButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        //GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;
        if (other.tag == "Player")
        {
            if (!played)
            {
                pageList.Play();
            }
            nextToPoster = false;
            interactButton.SetActive(false);
            AnswerManager.Instance.equation.SetActive(false);

        }
    }
}
