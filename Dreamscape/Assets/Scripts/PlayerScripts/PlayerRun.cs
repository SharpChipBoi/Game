using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    PlayerMovement moveScript;

    public float runSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shift"))
        {
            StartCoroutine(run());
        }
    }
    IEnumerator run()
    {
        moveScript.controller.Move(moveScript.moveDir * runSpeed * Time.deltaTime);
        yield return null;
    }
}
