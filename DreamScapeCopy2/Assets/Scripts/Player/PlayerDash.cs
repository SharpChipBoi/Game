using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public static PlayerDash instance;
    public static PlayerDash Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerDash>(); //находит PlayerDash и кладет его в instance, чтобы мы могли использовать его класс
            }
            return PlayerDash.instance;
        }
    }
    PlayerMovement moveScript;

    public float dashSpeed;
    public float dashTime;
    public float dashCD;
    public bool dashing;
    public AudioSource dashAudio;

    public CharacterController controller;

    public float height;
    Vector3 centerContr = new Vector3();
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        height = controller.height;
        centerContr = controller.center;
        moveScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(dashCD <= 0)//если кулдаун прошел мы можен занаво ускориться 
        { 

            if (Input.GetButtonDown("Dash")) //если нажата кнопка С запускаем анимацию и проигрываем звук кувырка
            {
                dashAudio.Play();
                StartCoroutine(Dash());
            }
        }
        else
        {
            controller.center = centerContr;
            controller.height = height;
            animator.SetBool("isSprinting", false);
            dashCD -= Time.deltaTime;
        }
    }

    public IEnumerator Dash()//Функция корутины, проверка на кулдаун и анимация кувырка, так же уменьшение высоты коллайдера 
    {
        float startTime = Time.time;
        while (Time.time < dashTime + startTime)
        {
            dashing = true;
            controller.height = 1.38f;
            controller.center = new Vector3(0, -0.19f, 0);
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);
            
            animator.SetBool("isSprinting", true);
            dashCD = 0.8f;
            yield return null;
        }
        dashing = false;
    }
}