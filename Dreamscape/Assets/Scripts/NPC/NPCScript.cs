using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cinemachine;


public class NPCScript : MonoBehaviour
{
    public NPCData data;
    public ObjectDialogue dialogue;

    public bool npcIsTalking;


    //public void TurnToPlayer(Vector3 playerPos)
    //{
    //    transform.DOLookAt(playerPos, Vector3.Distance(transform.position, playerPos) / 5);
    //    string turnMotion = isRightSide(transform.forward, playerPos, Vector3.up) ? "rturn" : "lturn";
    //    animator.SetTrigger(turnMotion);
    //}

    //public bool isRightSide(Vector3 fwd, Vector3 targetDir, Vector3 up)
    //{
    //    Vector3 right = Vector3.Cross(up.normalized, fwd.normalized);        // right vector
    //    float dir = Vector3.Dot(right, targetDir.normalized);
    //    return dir > 0f;
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
