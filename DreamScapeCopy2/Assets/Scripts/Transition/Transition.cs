using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
	//To which level are we going to?
	public int TargetedSceneIndex;

	//TargetPlayerLocation будет сохранено в Global, а затем установлено для игрока после перехода сцены, 
	
	//чтобы игрок оказался в правильном месте в новой сцене.
	public Transform TargetPlayerLocation;


	public void Interact()
	{

		//Назначаем дальнейшее место перехода.
		GlobalControl.Instance.TransitionTarget.position = TargetPlayerLocation.position;

		//переходим на сцену 
		SceneManager.LoadScene(TargetedSceneIndex);
	}
}
