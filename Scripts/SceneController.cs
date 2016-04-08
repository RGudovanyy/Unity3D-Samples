using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	// Сериализованная переменная для связи с объектом-шаблоном
	[SerializeField] private GameObject enemyPrefab;

	// Закрытая переменная для слежения за экземпляром врага в сцене
	private GameObject _enemy;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_enemy == null){
			// Метод, копирующий объект-шаблон
			_enemy = Instantiate(enemyPrefab) as GameObject; //возвращаем объект GameObject
			_enemy.transform.position = new Vector3(0,1,0);
			float angle = Random.Range(0,360);
			_enemy.transform.Rotate(0,angle,0);
		}
	}
}
