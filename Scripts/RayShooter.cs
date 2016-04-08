using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 	

public class RayShooter : MonoBehaviour {
	// ссылки на звуковые файлы, которые нужно воспроизвести
	[SerializeField] private AudioSource soundSource;
	[SerializeField] private AudioClip hitWallSound;
	[SerializeField] private AudioClip hitEnemySound;

	private Camera _camera;

	// Use this for initialization
	void Start () {
		// Доступ к другим компонентам этого объекта
		_camera = GetComponent<Camera>();
	
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false; // Скрываем указатель мыши в центре экрана
	
	}

	void OnGUI() {
		int size = 12;
		float posX = _camera.pixelWidth/2 - size/4;
		float posY = _camera.pixelHeight/2 - size/2;
		GUI.Label(new Rect(posX,posY,size, size), "*"); // Команда GUI.Label() отображает на экране символ
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) { // Реакция на нажати кнопки мыши


			// Середина экрана - это половина его ширирны и высоты
			Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);

			// Создание в этой точке луча
			Ray ray  = _camera.ScreenPointToRay(point);
			RaycastHit hit;
			// Испущенный луч заполняет информацией переменную, на которую имеется ссылка
			if(Physics.Raycast(ray,out hit)){
				// Получаем объект, в который попал луч
				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
				if(target != null){ // Проверяем у этого объекта компонента ReactiveTarget
					// Вызов метода реакции 
					target.ReactToHit();
					soundSource.PlayOneShot(hitEnemySound); // вызываем метод для воспроизведения звука попадания во врага
					Messenger.Broadcast(GameEvent.ENEMY_HIT); // Рассылка сообщения с информацией о попадании
				}else{
				// Запуск сопрограммы в ответ на попадание
				StartCoroutine(SphereIndicator(hit.point));
				soundSource.PlayOneShot (hitWallSound); // вызываем звук попадания в стену, если не попали во врагу
				}
			}
		
		}
	
	}

	private IEnumerator SphereIndicator(Vector3 pos) { // Сопрограммы пользуются функциями IEnumerator
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = pos;

		//Указываем сопрограмме, когда следует остановиться через ключевое слово yield
		yield return new WaitForSeconds(1);

		// Удаляем этот GameObject и очищаем память
		Destroy(sphere);
	}
}
