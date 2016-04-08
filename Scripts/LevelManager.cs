using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status {get; private set;}

	public void Startup(){
		Debug.Log ("Level manager starting...");
		status = ManagerStatus.Started;
	}

	public void ReachObjective(){
		Messenger.Broadcast(GameEvent.GAME_COMPLETE);

	}

	public void ExitGame(){
		Application.Quit();
	}

	public void RestartGame(){
		Application.LoadLevel("Demo"); // стартуем сцену заново
	}


}

