using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status {get; private set;}

	public int health {get; private set;}
	public int maxHealth {get; private set;}

	public void Startup(){
		Debug.Log ("Player manager starting...");
		//UpdateData(50,100);
		health = 100;
		maxHealth = 100;
		status = ManagerStatus.Started;
		Debug.Log("Health: "+health+ "/" + maxHealth);
	}

	public void UpdateData(int health, int maxHealth){
		this.health = health;
		this.maxHealth = maxHealth;
	}

	// метод для изменения переменной health
	public void ChangeHealth(int value){
		health += value;
		if(health > maxHealth){
			health = maxHealth;
		}else if (health < 0){
			health = 0;
		}
		if(health == 0){
			Messenger.Broadcast(GameEvent.GAME_FAILED);
		}

		Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
		Debug.Log("Health: "+health+ "/" + maxHealth);
	}

}

