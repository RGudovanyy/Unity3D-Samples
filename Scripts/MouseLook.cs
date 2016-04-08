using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseLook : MonoBehaviour {

	public enum RotationAxes { // Объявляем enum, который будет сопостовлять имена с параметрами
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}

	public RotationAxes axes = RotationAxes.MouseXAndY; // Объявляем глобальную переменную, которая появится в 
														// редакторе

	public float sensivityHor = 9.0f; // переменная для скорости вращения по горизонтали
	public float sensivityVert = 9.0f; // переменная для скорости вращения по вертикали

	// Максимальный угол поворота по вертикали	
	public float minimumVert = -45.0f;
	public float maximumVert = 45.0f;

	private float _rotationX = 0; // Переменная для поворота угла по вертикали


		// Use this for initialization
	void Start () {
		Rigidbody body = GetComponent<Rigidbody> ();
		if (body != null) // Проверяем существует ли этот компонент
			body.freezeRotation = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		
			if (axes == RotationAxes.MouseX) {
				transform.Rotate (0, Input.GetAxis ("Mouse X") * sensivityHor, 0);
			} else if (axes == RotationAxes.MouseY) {
				_rotationX -= Input.GetAxis ("Mouse Y") * sensivityVert; // Увелчиваем угол поворота по вертикали в 
				// соответствии с перемещениями указателя

				_rotationX = Mathf.Clamp (_rotationX, minimumVert, maximumVert); // Фиксация угла поворота по вертикали

				//Сохраняем одинаковый угол поворота вокруг оси Y, т.е. вращение в горизонтальной плоскости отсутствует
				float rotationY = transform.localEulerAngles.y; 

				// Создаем новый вектор из сохраненных значений поворота
				transform.localEulerAngles = new Vector3 (_rotationX, rotationY, 0);
		
		
			} else {
				_rotationX -= Input.GetAxis ("Mouse Y") * sensivityVert;
				_rotationX = Mathf.Clamp (_rotationX, minimumVert, maximumVert);

				// Приращение угла поворота через значение delta - не используем Rotate
				float delta = Input.GetAxis ("Mouse X") * sensivityHor;
				// Значение delta - это величина изменения угла поворота
				float rotationY = transform.localEulerAngles.y + delta;

				transform.localEulerAngles = new Vector3 (_rotationX, rotationY, 0);

		}
	}
}
