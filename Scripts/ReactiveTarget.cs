using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

	public void ReactToHit(){
		WanderingAI behavior = GetComponent<WanderingAI>();
		if(behavior != null){
			behavior.SetAlive(false);
		}
		// Метод, вызванный сценарием стрельбы
		StartCoroutine(Die());	
	}

	private IEnumerator Die(){
		// Опрокидываем врага
		this.transform.Rotate(75,0,0);
		// Ждем 1,5 секунды
		yield return new WaitForSeconds(1.5f);
		// Уничтожаем объект
		Destroy(this.gameObject);
	}




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
