using UnityEngine;
using System.Collections;

public class DeviceOperator : MonoBehaviour
{
	public float radius = 1.5f; // Расстояние, с которого персонаж может активировать устройства

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonDown("Fire3")){ // Реакция на кнопку ввода
			// метод возвращает список ближайших объектов, расположенных на определенном расстоянии от текущего местоположения
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius); 
			foreach (Collider hitCollider in hitColliders){
				// получаем направление, куда смотрит игрок, вычитая из координат объекта координаты игрока
				Vector3 direction = hitCollider.transform.position - transform.position; 
				if(Vector3.Dot(transform.forward, direction) > .5f){
					// метод пытается вызвать именованную функцию независимо от типа целевого объекта
					hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver); 
				}
			}
		}
	}
}

