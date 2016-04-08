using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	public const float baseSpeed = 3.0f;
	public float speed = 3.0f;	
	public float obstacleRange = 5.0f; // Расстояние с которого начинается реакция на препятствие
	private bool _alive; // Следим за состоянием персонажа

	// Use this for initialization
	void Start () {
		_alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(_alive){
		// Непрерывно движемся вперед в каждом кадре, несмотря на повороты
		transform.Translate(0,0,speed * Time.deltaTime);

		// Луч находится в том же положении, и нацеливается в том же направлении, что и персонаж
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		// Бросаем луч, с описанной вокруг него окружностью
		if(Physics.SphereCast(ray,0.75f,out hit)){
				GameObject hitObject = hit.transform.gameObject;
				// Игрок распознается путем наличия компонента PlayerCharacter
				if(hitObject.GetComponent<PlayerCharacter>()){
					if(_fireball == null){
						_fireball = Instantiate(fireballPrefab) as GameObject;
						//Помещаем фаербол перед врагом и нацелим в направлении его движения 
						_fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
						_fireball.transform.rotation = transform.rotation;

					}
				}
			else if(hit.distance < obstacleRange){
				// Поворот с наполовину случайным выбором нового направления
				float angle = Random.Range(-110, 110);
				transform.Rotate(0, angle, 0);
				}
			}
		}
	}

	// Метод, позволяющий внешнему коду воздействовать на "живое" состояние
	public void SetAlive(bool alive){
		_alive = alive;
	}




}
