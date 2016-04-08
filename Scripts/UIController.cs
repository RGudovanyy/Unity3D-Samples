using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	[SerializeField] private  Text healthLabel;
	[SerializeField] private Text gameEnding;
	[SerializeField] private SettingsPopup settingsPopup;
	[SerializeField] private InventoryPopup inverntoryPopup;


	void Start(){
		OnHealthUpdated();
		inverntoryPopup.gameObject.SetActive(false);
		settingsPopup.gameObject.SetActive(false);
		gameEnding.gameObject.SetActive(false);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.M)){
			bool isShowing = settingsPopup.gameObject.activeSelf;
			settingsPopup.gameObject.SetActive(!isShowing);

			if(isShowing){
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}else{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;

			}
		}

		if(Input.GetKeyDown(KeyCode.I)){
			bool isShowing = inverntoryPopup.gameObject.activeSelf;
			inverntoryPopup.gameObject.SetActive(!isShowing);
			inverntoryPopup.Refresh ();

			if(isShowing){
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}else{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}	
			inverntoryPopup.Refresh();
		}
	}

	public void OnPointerDown(){
		Debug.Log("pointer down");
	}

	void Awake(){ // Активация объекта
		Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated); // Добавление подписчика
		Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
		Messenger.AddListener(GameEvent.GAME_FAILED, OnGameFailed);
	}

	void OnDestroy(){ // Удаление объекта
		Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated); // Удаление подписчика
		Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
		Messenger.RemoveListener(GameEvent.GAME_FAILED, OnGameFailed);
	}

	private void OnHealthUpdated(){
		string message = "Health:" + Managers.Player.health + "/" + Managers.Player.maxHealth;
		healthLabel.text = message;
	}

	public void OnGameComplete(){
		StartCoroutine(GameComplete());
	}

	private IEnumerator GameComplete(){
		gameEnding.gameObject.SetActive(true);
		gameEnding.text = "Game Complete!";
		yield return new WaitForSeconds(2);
		Managers.Level.ExitGame();
	}

	public void OnGameFailed(){
		StartCoroutine(GameFail());
	}

	private IEnumerator GameFail(){
		gameEnding.gameObject.SetActive(true);
		gameEnding.text = "Game Failed!";
		yield return new WaitForSeconds(3);
		Managers.Level.RestartGame();
	}

}
