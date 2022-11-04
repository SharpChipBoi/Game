using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushForce = 1f;
    private PlayerMovement tpc;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if(body == null || body.isKinematic)
        {
            return;
        }
        if(hit.moveDirection.y < -0.3f)
        {
            return;
        }
        pushForce = tpc.speed;

        Vector3 forceDiraction = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = forceDiraction * pushForce;
    }
    private void Start()
    {
        tpc = gameObject.GetComponent<PlayerMovement>();
    }
}
