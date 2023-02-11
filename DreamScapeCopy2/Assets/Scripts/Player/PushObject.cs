using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushForce = 1f;
    private PlayerMovement tpc;//передаем скорость игрока
    private PlayerDash dash;//передаем скорость игрока
    private void Start()
    {
        tpc = gameObject.GetComponent<PlayerMovement>(); //передаем класс PlayerMovement и PlayerDash, запоминаем их в переменные
        dash = gameObject.GetComponent<PlayerDash>();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit) //при коллизии с предметом меняем его движение со скоростью и силой персонажа
    {
        Rigidbody body = hit.collider.attachedRigidbody;//находим у предмета риджидбади 
        if (body == null || body.isKinematic) //если тело статичное то выходим
        {
            return;
        }
        if (hit.moveDirection.y < -0.3f) // если персонаж не двигается выходим
        {
            return;
        }

        if (dash.dashing) // проверяем если персонаж в процессе кувырка  
        {
            pushForce = tpc.speed + dash.dashSpeed;
        }
        else
        {
            pushForce = tpc.speed;
        }
        

        Vector3 forceDiraction = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);//двигаем в зависимости от силы
        body.velocity = forceDiraction * pushForce;
    }
}
