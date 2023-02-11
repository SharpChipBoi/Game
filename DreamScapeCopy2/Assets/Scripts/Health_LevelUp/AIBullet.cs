using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBullet : MonoBehaviour
{
    //Rigidbody rb;
    public bool selfdestructed;
    //public float destroyCD;

    private void OnCollisionEnter(Collision collision) //при колизии с определенным предметом удаляем пулю
    {
        if (collision.gameObject.tag == "Player" && !Edible.Instance.isEaten)
        {
            CharacterStats.instance.Damage(20); 
            selfdestructed = true;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Ground" && !Edible.Instance.isEaten)
        {
            selfdestructed = true;
            Destroy(this.gameObject);
        }
        if (Edible.Instance.isEaten)
        {
            selfdestructed = true;
            Destroy(this.gameObject);

        }

    }
    private void Start()
    {
        selfdestructed = false;

        StartCoroutine(SelfDestruct());
    }
    public IEnumerator SelfDestruct() //если не произошло колизии, то удаляем пулю через 4 секунды
    {
        selfdestructed = true;
        yield return new WaitForSeconds(4f); 
        Destroy(this.gameObject);
    }
}
