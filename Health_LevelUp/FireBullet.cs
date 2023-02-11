using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public Quaternion originalRotationValue;
    public GameObject bullet;
    public GameObject tracker;
    public GameObject turret;
    public Transform turretBase;
    public AudioSource shoot;

    public float speed = 12;
    float rotSpeed = 6;

    public float cooldown;
    float lastShot;

    public float gravity;
    public float force;
    public float lookRadius = 8f;

    private void Start()
    {
        originalRotationValue = this.transform.rotation;
    }

    // Start is called before the first frame update
    void CreateBullet() //создаем пулю и проигрываем звук пули, так же проверяем кулдаун
    {
        if (Time.time - lastShot < cooldown)
            return;
        //CalculateAngle(true);
        lastShot = Time.time;
        shoot.Play();
        GameObject puff = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        puff.GetComponent<Rigidbody>().velocity = speed * turretBase.forward;
    }

    void RotateTurret() // менянем вращение врага по отношению к игроку с помощью функции CalculateAngle()
    {
        float? angle = CalculateAngle(true);
        if (angle != null)
        {
            turretBase.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }

    }

    float? CalculateAngle(bool low) //По формуле находим правильный угол, под которым будет создана пуля
    {
        Vector3 targetDir = tracker.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float speedSqr = speed * speed;
        float underSqrt = (speedSqr * speedSqr) - gravity * (gravity * x * x + 2 * y * speedSqr);

        if (underSqrt >= 0f)
        {
            float root = Mathf.Sqrt(underSqrt);
            float highAngle = speedSqr + root;
            float lowAngle = speedSqr - root;
            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        }
        else
            return null;
    }
    private void Update() //проверка на расстояние врага от игрока, если игрок близко, то вращаем врага с помощью функции RotateTurret и создаем пулю с помощью функции CreateBullet
    {
        float distance = Vector3.Distance(tracker.transform.position, transform.position);
        if (distance <= lookRadius)
        {

            Vector3 diraction = (tracker.transform.position - this.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(diraction.x, 0, diraction.z));
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
            RotateTurret();

            //Debug.Log("Interacting with player");
            CreateBullet();
        }
        else
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, originalRotationValue, Time.deltaTime * rotSpeed);

    }

    void OnDrawGizmosSelected()//рисуем гизмос
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}
