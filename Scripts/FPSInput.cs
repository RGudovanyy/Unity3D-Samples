using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour {
	// Необязательная переменная для возможности увеличить скорость
	public float speed = 6.0f;
	public float gravity = -9.8f;
	public float jumpSpeed = 15.0f;
	public float minFall = -1.5f;
	public float terminalVelocity = -10.0f;

	private float _vertSpeed;

	// Переменная для ссылки на компонент CharacterController
	private CharacterController _charController;

	// Use this for initialization
	void Start () {
		// Доступ к другим компонентам, присоединенным к этому же объекту
		_charController = GetComponent<CharacterController>();
		_vertSpeed = minFall;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis("Horizontal") * speed;
		float deltaZ = Input.GetAxis("Vertical") * speed;
		Vector3 movement = new Vector3(deltaX, 0, deltaZ);
		//Ограничим движение по диагонали той же скоростью, что и движение параллельно осям
		movement = Vector3.ClampMagnitude(movement,speed);

		// Механизм прыжка
		if(_charController.isGrounded ){ // Проверяем, что персонаж находится на поверхности
			if(Input.GetButtonDown ("Jump")){
				_vertSpeed = jumpSpeed;
			}else{
				_vertSpeed = minFall;
			}
		} else{ // Если персонаж не стоит на поверхности - применяем гравитацию 
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if(_vertSpeed < terminalVelocity){
				_vertSpeed = terminalVelocity;
			}
		}

		// Добавляем силу тяжести, которая тянет объект вниз
		movement.y = _vertSpeed;
		movement *= Time.deltaTime;
		//Преобразуем вектор движения от локальных к глобальным координатам
		movement = transform.TransformDirection(movement);
		// Заставляем вектор перемещать компонент CharacterController
		_charController.Move(movement);

	}


}
