using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that is used by objects for exiting a level.
/// Object using this script must be on Transition layer!
/// </summary>
public class Transition : MonoBehaviour
{
	//To which level are we going to?
	public int TargetedSceneIndex;

	//TargetPlayerLocation will be saved in Global, and then set to the player
	//after the scene transition, so the player is in correct spot in the new scene.
	public Transform TargetPlayerLocation;


	public void Interact()
	{

		//Assign the transition target location.
		GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;

		//Do important stuff :D 
		SceneManager.LoadScene(TargetedSceneIndex);
	}
}
