using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

	public float radius = 3f;               // растояние для поднятия
	public Transform interactionTransform;  

	public Transform player;

	//public bool inRange = false;
	public virtual void Interact()//этот метод можно переписать из других методов
	{
		Debug.Log("Interacting with " + transform.name);
	}

	void Update()
	{
	// если мы достаточно близко то можно взаимодействовать с предметом
		float distance = Vector3.Distance(player.position, interactionTransform.position);
		if (distance <= radius && Input.GetKeyDown(KeyCode.E))
		{
			Interact();
		}
	}

	// рисуем радиус гизмосом
	void OnDrawGizmosSelected()
	{
		if (interactionTransform == null)
			interactionTransform = transform;

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}
}