using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status {get; private set;}

	public void Startup(){
		Debug.Log ("Game managet starting...");

		status = ManagerStatus.Started;
	}

	public void ReachObjective(){
		Messenger.Broadcast(GameEvent.GAME_COMPLETE);
	}

	public void ExitGame(){
		Application.Quit();
	}

	public void RestartGame(){
		Application.LoadLevel(name); // стартуем сцену заново
	}


}

