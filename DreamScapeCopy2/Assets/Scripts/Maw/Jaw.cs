using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Скрипт не используется
public class Jaw : MonoBehaviour
{
    public void Shoot()
    {
        if(this.transform.localPosition.y < 0f)
        {
            StartCoroutine(_Shoot());
        }
    }

    IEnumerator _Shoot()
    {
        yield return new WaitForSeconds(.1f);
        this.transform.localPosition += (Vector3.up * 1f);
    }

    public void Retract()
    {
        if (this.transform.localPosition.y > 0f)
        {
            StartCoroutine(_Retract());
        }
    }
    IEnumerator _Retract()
    {
        yield return new WaitForSeconds(.2f);
        this.transform.localPosition -= (Vector3.up * 1f);
    }

   
}
