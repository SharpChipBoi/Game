using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    PlayerMovement moveScript;

    public float dashSpeed;
    public float dashTime;
    public float dashCD;
    // Start is called before the first frame update
    void Start()
    {

        moveScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(dashCD <= 0)
        { 
            if (Input.GetButtonDown("Dash"))
            {
                StartCoroutine(Dash());
            }
        }
        else 
        { 
            dashCD -= Time.deltaTime;
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < dashTime + startTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);

            dashCD = 0.8f;
            yield return null;
        }
    }
}
