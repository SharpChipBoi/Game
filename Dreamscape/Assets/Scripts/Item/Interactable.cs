using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform player;
    public bool inRadius;
    bool interacted = false;
    public Transform interactionTransform;


    public virtual void Interact()
    {
        //переписываем функции в зависимости с чем интерактируем
    }

    private void Update()
    {
        if (!interacted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius && Input.GetKeyDown(KeyCode.E))
            {
                Interact();
                interacted = true;

            }
        }
    }

    //public void NearObject(Transform playerTransform)
    //{
    //    inRadius = true;
    //    player = playerTransform;
    //}
    //public void AwayFromObject(Transform playerTransform)
    //{
    //    inRadius = false;
    //    player = null;
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);

    }
}
